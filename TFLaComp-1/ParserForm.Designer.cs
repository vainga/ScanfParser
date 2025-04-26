namespace ScanfParser
{
    partial class ParserForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParserForm));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            makeToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAssToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editingToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            pasteAllToolStripMenuItem = new ToolStripMenuItem();
            textToolStripMenuItem = new ToolStripMenuItem();
            expToolStripMenuItem = new ToolStripMenuItem();
            grammarToolStripMenuItem = new ToolStripMenuItem();
            classificationgrammarClassToolStripMenuItem = new ToolStripMenuItem();
            analysismethodToolStripMenuItem = new ToolStripMenuItem();
            diagnosticsNeutralizationToolStripMenuItem = new ToolStripMenuItem();
            explToolStripMenuItem = new ToolStripMenuItem();
            bibliographyToolStripMenuItem = new ToolStripMenuItem();
            sourceCodeToolStripMenuItem = new ToolStripMenuItem();
            StartToolStripMenuItem = new ToolStripMenuItem();
            refToolStripMenuItem = new ToolStripMenuItem();
            callHelpToolStripMenuItem = new ToolStripMenuItem();
            aboutCodeToolStripMenuItem = new ToolStripMenuItem();
            курсачToolStripMenuItem = new ToolStripMenuItem();
            file = new Button();
            open = new Button();
            save = new Button();
            start = new Button();
            undo = new Button();
            redo = new Button();
            cut = new Button();
            paste = new Button();
            richTextBoxInput = new RichTextBox();
            copy = new Button();
            helpProvider1 = new HelpProvider();
            toolTip1 = new ToolTip(components);
            dataGridViewOutput = new DataGridView();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOutput).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editingToolStripMenuItem, textToolStripMenuItem, StartToolStripMenuItem, refToolStripMenuItem, курсачToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(701, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { makeToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, saveAssToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(48, 20);
            fileToolStripMenuItem.Text = "Файл";
            // 
            // makeToolStripMenuItem
            // 
            makeToolStripMenuItem.Name = "makeToolStripMenuItem";
            makeToolStripMenuItem.Size = new Size(153, 22);
            makeToolStripMenuItem.Text = "Создать";
            makeToolStripMenuItem.Click += makeToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(153, 22);
            openToolStripMenuItem.Text = "Открыть";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(153, 22);
            saveToolStripMenuItem.Text = "Сохранить";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAssToolStripMenuItem
            // 
            saveAssToolStripMenuItem.Name = "saveAssToolStripMenuItem";
            saveAssToolStripMenuItem.Size = new Size(153, 22);
            saveAssToolStripMenuItem.Text = "Сохранить как";
            saveAssToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(153, 22);
            exitToolStripMenuItem.Text = "Выход";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // editingToolStripMenuItem
            // 
            editingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, deleteToolStripMenuItem, pasteAllToolStripMenuItem });
            editingToolStripMenuItem.Name = "editingToolStripMenuItem";
            editingToolStripMenuItem.Size = new Size(59, 20);
            editingToolStripMenuItem.Text = "Правка";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(148, 22);
            undoToolStripMenuItem.Text = "Отменить";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(148, 22);
            redoToolStripMenuItem.Text = "Повторить";
            redoToolStripMenuItem.Click += redoToolStripMenuItem_Click;
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new Size(148, 22);
            cutToolStripMenuItem.Text = "Вырезать";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(148, 22);
            copyToolStripMenuItem.Text = "Копировать";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(148, 22);
            pasteToolStripMenuItem.Text = "Вставить";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(148, 22);
            deleteToolStripMenuItem.Text = "Удалить";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // pasteAllToolStripMenuItem
            // 
            pasteAllToolStripMenuItem.Name = "pasteAllToolStripMenuItem";
            pasteAllToolStripMenuItem.Size = new Size(148, 22);
            pasteAllToolStripMenuItem.Text = "Выделить все";
            pasteAllToolStripMenuItem.Click += pasteAllToolStripMenuItem_Click;
            // 
            // textToolStripMenuItem
            // 
            textToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { expToolStripMenuItem, grammarToolStripMenuItem, classificationgrammarClassToolStripMenuItem, analysismethodToolStripMenuItem, diagnosticsNeutralizationToolStripMenuItem, explToolStripMenuItem, bibliographyToolStripMenuItem, sourceCodeToolStripMenuItem });
            textToolStripMenuItem.Name = "textToolStripMenuItem";
            textToolStripMenuItem.Size = new Size(49, 20);
            textToolStripMenuItem.Text = "Текст";
            // 
            // expToolStripMenuItem
            // 
            expToolStripMenuItem.Name = "expToolStripMenuItem";
            expToolStripMenuItem.Size = new Size(288, 22);
            expToolStripMenuItem.Text = "Постановка задачи";
            expToolStripMenuItem.Click += expToolStripMenuItem_Click;
            // 
            // grammarToolStripMenuItem
            // 
            grammarToolStripMenuItem.Name = "grammarToolStripMenuItem";
            grammarToolStripMenuItem.Size = new Size(288, 22);
            grammarToolStripMenuItem.Text = "Грамматика";
            grammarToolStripMenuItem.Click += grammarToolStripMenuItem_Click;
            // 
            // classificationgrammarClassToolStripMenuItem
            // 
            classificationgrammarClassToolStripMenuItem.Name = "classificationgrammarClassToolStripMenuItem";
            classificationgrammarClassToolStripMenuItem.Size = new Size(288, 22);
            classificationgrammarClassToolStripMenuItem.Text = "Классификация грамматики";
            classificationgrammarClassToolStripMenuItem.Click += classificationgrammarClassToolStripMenuItem_Click;
            // 
            // analysismethodToolStripMenuItem
            // 
            analysismethodToolStripMenuItem.Name = "analysismethodToolStripMenuItem";
            analysismethodToolStripMenuItem.Size = new Size(288, 22);
            analysismethodToolStripMenuItem.Text = "Метод анализа";
            analysismethodToolStripMenuItem.Click += analysismethodToolStripMenuItem_Click;
            // 
            // diagnosticsNeutralizationToolStripMenuItem
            // 
            diagnosticsNeutralizationToolStripMenuItem.Name = "diagnosticsNeutralizationToolStripMenuItem";
            diagnosticsNeutralizationToolStripMenuItem.Size = new Size(288, 22);
            diagnosticsNeutralizationToolStripMenuItem.Text = "Диагностика и нейтрализация ошибок";
            diagnosticsNeutralizationToolStripMenuItem.Click += diagnosticsNeutralizationToolStripMenuItem_Click;
            // 
            // explToolStripMenuItem
            // 
            explToolStripMenuItem.Name = "explToolStripMenuItem";
            explToolStripMenuItem.Size = new Size(288, 22);
            explToolStripMenuItem.Text = "Тестовый пример";
            explToolStripMenuItem.Click += explToolStripMenuItem_Click;
            // 
            // bibliographyToolStripMenuItem
            // 
            bibliographyToolStripMenuItem.Name = "bibliographyToolStripMenuItem";
            bibliographyToolStripMenuItem.Size = new Size(288, 22);
            bibliographyToolStripMenuItem.Text = "Список литературы";
            bibliographyToolStripMenuItem.Click += bibliographyToolStripMenuItem_Click;
            // 
            // sourceCodeToolStripMenuItem
            // 
            sourceCodeToolStripMenuItem.Name = "sourceCodeToolStripMenuItem";
            sourceCodeToolStripMenuItem.Size = new Size(288, 22);
            sourceCodeToolStripMenuItem.Text = "Исходный код программы";
            sourceCodeToolStripMenuItem.Click += sourceCodeToolStripMenuItem_Click;
            // 
            // StartToolStripMenuItem
            // 
            StartToolStripMenuItem.Name = "StartToolStripMenuItem";
            StartToolStripMenuItem.Size = new Size(46, 20);
            StartToolStripMenuItem.Text = "Пуск";
            StartToolStripMenuItem.Click += StartToolStripMenuItem_Click;
            // 
            // refToolStripMenuItem
            // 
            refToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { callHelpToolStripMenuItem, aboutCodeToolStripMenuItem });
            refToolStripMenuItem.Name = "refToolStripMenuItem";
            refToolStripMenuItem.Size = new Size(65, 20);
            refToolStripMenuItem.Text = "Справка";
            // 
            // callHelpToolStripMenuItem
            // 
            callHelpToolStripMenuItem.Name = "callHelpToolStripMenuItem";
            callHelpToolStripMenuItem.Size = new Size(156, 22);
            callHelpToolStripMenuItem.Text = "Вызов справки";
            callHelpToolStripMenuItem.Click += callHelpToolStripMenuItem_Click;
            // 
            // aboutCodeToolStripMenuItem
            // 
            aboutCodeToolStripMenuItem.Name = "aboutCodeToolStripMenuItem";
            aboutCodeToolStripMenuItem.Size = new Size(156, 22);
            aboutCodeToolStripMenuItem.Text = "О программе";
            aboutCodeToolStripMenuItem.Click += aboutCodeToolStripMenuItem_Click;
            // 
            // курсачToolStripMenuItem
            // 
            курсачToolStripMenuItem.Name = "курсачToolStripMenuItem";
            курсачToolStripMenuItem.Size = new Size(58, 20);
            курсачToolStripMenuItem.Text = "Курсач";
            курсачToolStripMenuItem.Click += курсачToolStripMenuItem_Click;
            // 
            // file
            // 
            file.BackgroundImage = (Image)resources.GetObject("file.BackgroundImage");
            file.BackgroundImageLayout = ImageLayout.Zoom;
            file.Cursor = Cursors.Hand;
            file.Location = new Point(10, 28);
            file.Margin = new Padding(3, 2, 3, 2);
            file.Name = "file";
            helpProvider1.SetShowHelp(file, false);
            file.Size = new Size(37, 32);
            file.TabIndex = 1;
            file.Tag = "";
            toolTip1.SetToolTip(file, "Создать файл");
            file.UseVisualStyleBackColor = true;
            file.Click += file_Click;
            // 
            // open
            // 
            open.BackgroundImage = (Image)resources.GetObject("open.BackgroundImage");
            open.BackgroundImageLayout = ImageLayout.Zoom;
            open.Cursor = Cursors.Hand;
            open.Location = new Point(48, 28);
            open.Margin = new Padding(3, 2, 3, 2);
            open.Name = "open";
            helpProvider1.SetShowHelp(open, false);
            open.Size = new Size(37, 32);
            open.TabIndex = 2;
            open.Tag = "";
            toolTip1.SetToolTip(open, "Открыть файл");
            open.UseVisualStyleBackColor = true;
            open.Click += open_Click;
            // 
            // save
            // 
            save.BackgroundImage = (Image)resources.GetObject("save.BackgroundImage");
            save.BackgroundImageLayout = ImageLayout.Zoom;
            save.Cursor = Cursors.Hand;
            save.Location = new Point(86, 28);
            save.Margin = new Padding(3, 2, 3, 2);
            save.Name = "save";
            helpProvider1.SetShowHelp(save, false);
            save.Size = new Size(37, 32);
            save.TabIndex = 3;
            save.Tag = "";
            toolTip1.SetToolTip(save, "Сохранить");
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // start
            // 
            start.BackgroundImage = (Image)resources.GetObject("start.BackgroundImage");
            start.BackgroundImageLayout = ImageLayout.Zoom;
            start.Cursor = Cursors.Hand;
            start.Location = new Point(140, 28);
            start.Margin = new Padding(3, 2, 3, 2);
            start.Name = "start";
            helpProvider1.SetShowHelp(start, false);
            start.Size = new Size(37, 32);
            start.TabIndex = 4;
            start.Tag = "";
            toolTip1.SetToolTip(start, "Запуск");
            start.UseVisualStyleBackColor = true;
            start.Click += start_Click;
            // 
            // undo
            // 
            undo.BackgroundImage = (Image)resources.GetObject("undo.BackgroundImage");
            undo.BackgroundImageLayout = ImageLayout.Zoom;
            undo.Cursor = Cursors.Hand;
            undo.Location = new Point(196, 28);
            undo.Margin = new Padding(3, 2, 3, 2);
            undo.Name = "undo";
            helpProvider1.SetShowHelp(undo, false);
            undo.Size = new Size(37, 32);
            undo.TabIndex = 5;
            undo.Tag = "";
            toolTip1.SetToolTip(undo, "Отменить");
            undo.UseVisualStyleBackColor = true;
            undo.Click += undo_Click;
            // 
            // redo
            // 
            redo.BackgroundImage = (Image)resources.GetObject("redo.BackgroundImage");
            redo.BackgroundImageLayout = ImageLayout.Zoom;
            redo.Cursor = Cursors.Hand;
            redo.Location = new Point(234, 28);
            redo.Margin = new Padding(3, 2, 3, 2);
            redo.Name = "redo";
            helpProvider1.SetShowHelp(redo, false);
            redo.Size = new Size(37, 32);
            redo.TabIndex = 6;
            redo.Tag = "";
            toolTip1.SetToolTip(redo, "Вернуть");
            redo.UseVisualStyleBackColor = true;
            redo.Click += redo_Click;
            // 
            // cut
            // 
            cut.BackgroundImage = (Image)resources.GetObject("cut.BackgroundImage");
            cut.BackgroundImageLayout = ImageLayout.Zoom;
            cut.Cursor = Cursors.Hand;
            cut.Location = new Point(291, 28);
            cut.Margin = new Padding(3, 2, 3, 2);
            cut.Name = "cut";
            helpProvider1.SetShowHelp(cut, false);
            cut.Size = new Size(37, 32);
            cut.TabIndex = 7;
            cut.Tag = "";
            toolTip1.SetToolTip(cut, "Вырезать");
            cut.UseVisualStyleBackColor = true;
            cut.Click += cut_Click;
            // 
            // paste
            // 
            paste.BackgroundImage = (Image)resources.GetObject("paste.BackgroundImage");
            paste.BackgroundImageLayout = ImageLayout.Zoom;
            paste.Cursor = Cursors.Hand;
            paste.Location = new Point(329, 28);
            paste.Margin = new Padding(3, 2, 3, 2);
            paste.Name = "paste";
            helpProvider1.SetShowHelp(paste, false);
            paste.Size = new Size(37, 32);
            paste.TabIndex = 8;
            paste.Tag = "";
            toolTip1.SetToolTip(paste, "Вставить");
            paste.UseVisualStyleBackColor = true;
            paste.Click += paste_Click;
            // 
            // richTextBoxInput
            // 
            richTextBoxInput.Location = new Point(12, 64);
            richTextBoxInput.Margin = new Padding(3, 2, 3, 2);
            richTextBoxInput.Name = "richTextBoxInput";
            richTextBoxInput.RightToLeft = RightToLeft.No;
            richTextBoxInput.Size = new Size(679, 216);
            richTextBoxInput.TabIndex = 9;
            richTextBoxInput.Text = "";
            richTextBoxInput.WordWrap = false;
            richTextBoxInput.KeyDown += richTextBoxInput_KeyDown;
            // 
            // copy
            // 
            copy.BackgroundImage = (Image)resources.GetObject("copy.BackgroundImage");
            copy.BackgroundImageLayout = ImageLayout.Zoom;
            copy.Cursor = Cursors.Hand;
            copy.Location = new Point(367, 28);
            copy.Margin = new Padding(3, 2, 3, 2);
            copy.Name = "copy";
            copy.Size = new Size(37, 32);
            copy.TabIndex = 11;
            toolTip1.SetToolTip(copy, "Копировать");
            copy.UseVisualStyleBackColor = true;
            copy.Click += copy_Click;
            // 
            // dataGridViewOutput
            // 
            dataGridViewOutput.AllowUserToDeleteRows = false;
            dataGridViewOutput.AllowUserToResizeColumns = false;
            dataGridViewOutput.AllowUserToResizeRows = false;
            dataGridViewOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOutput.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewOutput.Location = new Point(10, 285);
            dataGridViewOutput.Name = "dataGridViewOutput";
            dataGridViewOutput.ReadOnly = true;
            dataGridViewOutput.RowHeadersWidth = 51;
            dataGridViewOutput.RowTemplate.Height = 25;
            dataGridViewOutput.Size = new Size(681, 301);
            dataGridViewOutput.TabIndex = 12;
            // 
            // ParserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(701, 598);
            Controls.Add(dataGridViewOutput);
            Controls.Add(copy);
            Controls.Add(richTextBoxInput);
            Controls.Add(paste);
            Controls.Add(cut);
            Controls.Add(redo);
            Controls.Add(undo);
            Controls.Add(start);
            Controls.Add(save);
            Controls.Add(open);
            Controls.Add(file);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "ParserForm";
            Text = "Парсер";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOutput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editingToolStripMenuItem;
        private ToolStripMenuItem textToolStripMenuItem;
        private ToolStripMenuItem StartToolStripMenuItem;
        private ToolStripMenuItem refToolStripMenuItem;
        private RichTextBox richTextBoxInput;
        private Button copy;
        private ToolStripMenuItem makeToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAssToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem pasteAllToolStripMenuItem;
        private ToolStripMenuItem expToolStripMenuItem;
        private ToolStripMenuItem grammarToolStripMenuItem;
        private ToolStripMenuItem classificationgrammarClassToolStripMenuItem;
        private ToolStripMenuItem analysismethodToolStripMenuItem;
        private ToolStripMenuItem diagnosticsNeutralizationToolStripMenuItem;
        private ToolStripMenuItem explToolStripMenuItem;
        private ToolStripMenuItem bibliographyToolStripMenuItem;
        private ToolStripMenuItem sourceCodeToolStripMenuItem;
        private ToolStripMenuItem callHelpToolStripMenuItem;
        private ToolStripMenuItem aboutCodeToolStripMenuItem;
        private HelpProvider helpProvider1;
        internal Button file;
        internal Button open;
        internal Button save;
        internal Button start;
        internal Button undo;
        internal Button redo;
        internal Button cut;
        internal Button paste;
        private ToolTip toolTip1;
        private DataGridView dataGridViewOutput;
        private ToolStripMenuItem курсачToolStripMenuItem;
    }
}
