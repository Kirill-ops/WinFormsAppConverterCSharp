namespace WinFormsAppConverterCSharp
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FileDialogOpenSolution = new OpenFileDialog();
            buttonOpenSolution = new Button();
            comboBoxNamespaces = new ComboBox();
            labelNamespaces = new Label();
            comboBoxProjects = new ComboBox();
            labelProjects = new Label();
            buttonConvert = new Button();
            folderBrowserDialogOutputDirectory = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // FileDialogOpenSolution
            // 
            FileDialogOpenSolution.FileName = "openSolutionFileDialog";
            // 
            // buttonOpenSolution
            // 
            buttonOpenSolution.Location = new Point(12, 12);
            buttonOpenSolution.Name = "buttonOpenSolution";
            buttonOpenSolution.Size = new Size(271, 29);
            buttonOpenSolution.TabIndex = 0;
            buttonOpenSolution.Text = "Выбрать решение (файл .sln)";
            buttonOpenSolution.UseVisualStyleBackColor = true;
            buttonOpenSolution.Click += ButtonOpenSolution_Click;
            // 
            // comboBoxNamespaces
            // 
            comboBoxNamespaces.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNamespaces.FormattingEnabled = true;
            comboBoxNamespaces.Location = new Point(12, 167);
            comboBoxNamespaces.Name = "comboBoxNamespaces";
            comboBoxNamespaces.Size = new Size(271, 28);
            comboBoxNamespaces.TabIndex = 1;
            comboBoxNamespaces.SelectedIndexChanged += ComboBoxNamespaces_SelectedIndexChanged;
            // 
            // labelNamespaces
            // 
            labelNamespaces.AutoSize = true;
            labelNamespaces.Location = new Point(12, 144);
            labelNamespaces.Name = "labelNamespaces";
            labelNamespaces.Size = new Size(147, 20);
            labelNamespaces.TabIndex = 2;
            labelNamespaces.Text = "Пространства имён";
            // 
            // comboBoxProjects
            // 
            comboBoxProjects.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProjects.FormattingEnabled = true;
            comboBoxProjects.Location = new Point(12, 89);
            comboBoxProjects.Name = "comboBoxProjects";
            comboBoxProjects.Size = new Size(271, 28);
            comboBoxProjects.TabIndex = 3;
            comboBoxProjects.SelectedIndexChanged += ComboBoxProjects_SelectedIndexChanged;
            // 
            // labelProjects
            // 
            labelProjects.AutoSize = true;
            labelProjects.Location = new Point(12, 66);
            labelProjects.Name = "labelProjects";
            labelProjects.Size = new Size(70, 20);
            labelProjects.TabIndex = 4;
            labelProjects.Text = "Проекты";
            // 
            // buttonConvert
            // 
            buttonConvert.Location = new Point(12, 242);
            buttonConvert.Name = "buttonConvert";
            buttonConvert.Size = new Size(271, 29);
            buttonConvert.TabIndex = 5;
            buttonConvert.Text = "Конвертировать";
            buttonConvert.UseVisualStyleBackColor = true;
            buttonConvert.Click += ButtonConvert_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(294, 450);
            Controls.Add(buttonConvert);
            Controls.Add(labelProjects);
            Controls.Add(comboBoxProjects);
            Controls.Add(labelNamespaces);
            Controls.Add(comboBoxNamespaces);
            Controls.Add(buttonOpenSolution);
            Name = "FormMain";
            Text = "Convecter C#";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog FileDialogOpenSolution;
        private Button buttonOpenSolution;
        private ComboBox comboBoxNamespaces;
        private Label labelNamespaces;
        private ComboBox comboBoxProjects;
        private Label labelProjects;
        private Button buttonConvert;
        private FolderBrowserDialog folderBrowserDialogOutputDirectory;
    }
}
