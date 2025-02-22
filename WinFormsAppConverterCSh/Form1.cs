using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System.Collections.Generic;
using WinFormsAppConverterCSh.Converters;
using WinFormsAppConverterCSh.Models;

namespace WinFormsAppConverterCSh;

public partial class FormMain : Form
{
    private readonly MSBuildWorkspace _workspace;
    private Solution? _solution;
    private IReadOnlyList<Project> _projects = [];
    private IReadOnlyList<BaseNamespaceDeclarationSyntax> _namespaces = [];
    private readonly CSharpToTypescript _cSharpToTypescript;
    private string _nameNamespace = "";

    public FormMain()
    {
        InitializeComponent();

        _workspace = MSBuildWorkspace.Create();
        _cSharpToTypescript = new();
    }

    private void ButtonOpenSolution_Click(object sender, EventArgs e)
    {
        if (FileDialogOpenSolution.ShowDialog() == DialogResult.Cancel)
            return;

        var solutionPath = FileDialogOpenSolution.FileName;
        try
        {
            _solution = _workspace.OpenSolutionAsync(solutionPath).Result;
            _projects = _solution.Projects.ToList();
            comboBoxProjects.Items.Clear();
            comboBoxNamespaces.Items.Clear();
            comboBoxProjects.Items.AddRange(_projects.Select(x => x.Name).ToArray());
        }
        catch { MessageBox.Show("Ошибка во время открытия решения."); }
    }


    private void ComboBoxProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedProject = comboBoxProjects.SelectedItem?.ToString();
        if (selectedProject is not null)
        {
            var project = _projects.FirstOrDefault(x => x.Name == selectedProject);
            if (project is not null)
            {
                var compilation = project.GetCompilationAsync().Result;
                if (compilation == null)
                {
                    MessageBox.Show("Failed to get compilation.");
                    return;
                }

                _namespaces = compilation.SyntaxTrees
                    .SelectMany(tree
                        => tree
                            .GetRoot()
                            .DescendantNodes()
                            .OfType<BaseNamespaceDeclarationSyntax>()
                            .ToList())
                    .ToList();
                comboBoxNamespaces.Items.Clear();
                comboBoxNamespaces.Items.AddRange(_namespaces.Select(x => x.Name.ToString()).Distinct().ToArray());
            }

        }
    }

    private void ComboBoxNamespaces_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = comboBoxNamespaces.SelectedItem?.ToString();
        if (selected is not null)
        {
            _nameNamespace = selected;
        }
    }

    private void ButtonConvert_Click(object sender, EventArgs e)
    {
        if (folderBrowserDialogOutputDirectory.ShowDialog() == DialogResult.Cancel)
            return;

        var outputDirectories = folderBrowserDialogOutputDirectory.SelectedPaths;

        if (outputDirectories.Length < 1 || outputDirectories[0] == string.Empty)
        {
            MessageBox.Show("Ошибка при выборе папки");
            return;
        }

        var outputDirectory = outputDirectories[0];

        var namespaces = _namespaces
            .Where(x => x.Name.ToString() == _nameNamespace);

        if (namespaces == null)
        {
            Console.WriteLine($"Namespace '{_nameNamespace}' not found.");
            return;
        }

        var modelTsInterfaces = _cSharpToTypescript.GetTypescriptInterfaceModels(namespaces);

        if (modelTsInterfaces.Count > 0)
        {
            foreach (var modelTsInterface in modelTsInterfaces)
            {
                string filePath = Path.Combine(outputDirectory, modelTsInterface.FileName);
                using var writer = new StreamWriter(filePath);
                writer.Write(modelTsInterface.Code);
            }

            string indexFilePath = Path.Combine(outputDirectory, "index.ts");
            using var indexWriter = new StreamWriter(indexFilePath);
            var indexTsCode = string.Join("\n", modelTsInterfaces.Select(x => $"export type {{ {x.Name} }} from './{x.FileName}';").ToList());
            indexWriter.Write(indexTsCode);
        }

        MessageBox.Show("Модели созданы!");
    }
}
