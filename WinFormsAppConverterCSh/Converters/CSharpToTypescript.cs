using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection.Metadata;
using WinFormsAppConverterCSharp.Models;

namespace WinFormsAppConverterCSharp.Converters;

public class CSharpToTypescript
{
    public IReadOnlyList<TypescriptInterfaceModel> GetTypescriptInterfaceModels(IEnumerable<BaseNamespaceDeclarationSyntax> namespaces)
    {
        var tsInterfaceModels = new List<TypescriptInterfaceModel>();
        foreach (var currentNamespace in namespaces)
        {
            var classes = currentNamespace.DescendantNodes().OfType<ClassDeclarationSyntax>();
            foreach (var classSyntax in classes)
            {
                var modelTsInterface = GetTypescriptInterfaceModel(classSyntax);
                tsInterfaceModels.Add(modelTsInterface);
            }
        }

        return tsInterfaceModels;
    }

    private static TypescriptInterfaceModel GetTypescriptInterfaceModel(ClassDeclarationSyntax classSyntax)
    {
        string className = classSyntax.Identifier.Text;
        string tsClassName = "I" + className;
        string fileName = ConvertToKebabCase(tsClassName) + ".ts";

        var imports = new List<string>();

        var tsCode = $"export interface {tsClassName} {{";
        foreach (var member in classSyntax.Members)
        {
            if (member is PropertyDeclarationSyntax property)
            {
                var (type, import) = GetTypeToTypeScript(property);
                tsCode += $"\n  {type}";

                if (import != "" && !imports.Contains(import))
                    imports.Add(import);
            }
        }
        tsCode += "\n}";

        if (imports.Count > 0)
        {
            tsCode = $"import {{ {string.Join(", ", imports)} }} from \".\";\n\n{tsCode}";
        }

        return new()
        {
            Name = tsClassName,
            FileName = fileName,
            Code = tsCode
        };
    }

    static (string type, string import) GetTypeToTypeScript(PropertyDeclarationSyntax propertySyntax)
    {
        var propertyName = ConvertToCamelCase(propertySyntax.Identifier.Text);
        string typeName = propertySyntax.Type.ToString();
        var resultTypeName = ("", "");

        // Обработка nullable типов
        if (typeName.EndsWith("?"))
        {
            propertyName += "?";
            typeName = typeName.TrimEnd('?');
        }

        // Обработка IReadOnlyList<T>
        if (typeName.StartsWith("IReadOnlyList<"))
        {
            string genericType = typeName.Substring("IReadOnlyList<".Length, typeName.Length - "IReadOnlyList<".Length - 1);
            resultTypeName = GetTypeName(genericType);
        }
        else
        {
            resultTypeName = GetTypeName(typeName);
        }

        return ($"{propertyName}: {resultTypeName.Item1};", resultTypeName.Item2);


    }

    static (string type, string import) GetTypeName(string cSharTypeName)
    {
        switch (cSharTypeName)
        {
            case "int":
            case "long":
            case "double":
            case "float":
            case "decimal":
                return ("number", "");
            case "bool":
                return ("boolean", "");
            case "string":
            case "DateTimeOffset":
            case "DateOnly":
            case "Guid":
                return ("string", "");
            default:
                return ($"I{cSharTypeName}", $"I{cSharTypeName}"); // TODO 1: 
        }
    }

    static string ConvertToKebabCase(string input)
    {
        return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString().ToLower() : x.ToString().ToLower())).TrimStart('-');
    }

    private static string ConvertToCamelCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;
        return char.ToLower(input[0]) + input[1..];
    }
}
