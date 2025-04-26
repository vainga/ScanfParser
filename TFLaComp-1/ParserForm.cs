using System.Windows.Forms;
using ScanfParser.DTO;
using ScanfParser.Functional;
using ScanfParser.ParserHelp;
using ScanfParser.RegExParser;
using ScanfParser.ResultLog;
using TFLaComp_1.ParserLogic;

namespace ScanfParser
{
    public partial class ParserForm : System.Windows.Forms.Form
    {
        private IEdit _edit;

        private IFileLogic _logic;

        private IParserHelpProvider _helpProvider;

        private ISaveResult _saveResult;

        private bool isTextChanged = false;

        private bool isHighlighted = false;

        public ParserForm()
        {
            InitializeComponent();


            this.HelpButton = true;
            _saveResult = new SaveResult();
            InitEdit();
            _logic = new FileLogic();

            _helpProvider = new ParserHelpProvider();
            _helpProvider.SetHelp(richTextBoxInput, HelpHtmDict.TopicDict["Правка"], HelpNavigator.Topic);
            _helpProvider.SetHelp(file, "Создать", HelpNavigator.KeywordIndex);
            _helpProvider.SetHelp(open, "Открыть", HelpNavigator.KeywordIndex);
            _helpProvider.SetHelp(save, "Сохранить", HelpNavigator.KeywordIndex);
            _helpProvider.SetHelp(start, "Пуск", HelpNavigator.Topic);

            _helpProvider.SetHelp(undo, "Отменить", HelpNavigator.KeywordIndex);
            _helpProvider.SetHelp(redo, "Повторить", HelpNavigator.KeywordIndex);
            _helpProvider.SetHelp(copy, "Копировать", HelpNavigator.KeywordIndex);
            _helpProvider.SetHelp(cut, "Вырезать", HelpNavigator.KeywordIndex);
            _helpProvider.SetHelp(paste, "Вставить", HelpNavigator.KeywordIndex);

            _helpProvider.SetHelp(this, "О программе", HelpNavigator.KeywordIndex);

            helpProvider1 = _helpProvider.HelpProvider;

            richTextBoxInput.TextChanged += richTextBoxInput_TextChanged;
        }

        private void makeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfirmSaveChanges())
            {
                string text = richTextBoxInput.Text;
                _logic.Create(ref text);
                richTextBoxInput.Text = text;
                ClearOutput();
            }
        }

        private void file_Click(object sender, EventArgs e)
        {
            if (ConfirmSaveChanges())
            {
                string text = richTextBoxInput.Text;
                _logic.Create(ref text);
                richTextBoxInput.Text = text;
                ClearOutput();
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
            if (ConfirmSaveChanges())
                LoadFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfirmSaveChanges())
                LoadFile();
        }

        private bool ConfirmSaveChanges()
        {
            if (!isTextChanged || richTextBoxInput.Text == "") return true; // Если изменений нет, просто продолжаем.

            DialogResult result = MessageBox.Show(
                "Сохранить изменения перед открытием нового файла?",
                "Подтверждение",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _logic.Save(richTextBoxInput.Text);
                isTextChanged = false;
                return true;
            }
            else if (result == DialogResult.No)
            {
                return true;
            }

            return false; // Отмена операции
        }

        private void LoadFile()
        {
            try
            {
                string? text = _logic.Open() ?? throw new FileLoadException("Ошибка открытия файла!");
                richTextBoxInput.Text = text;
                isTextChanged = false; // Сбрасываем флаг изменений
                InitEdit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            _logic.Save(richTextBoxInput.Text);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logic.Save(richTextBoxInput.Text);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logic.SaveAs(richTextBoxInput.Text);
        }

        private void HighlightResults(List<CardDTO> cards)
        {
            Dehighlight();

            foreach (var card in cards)
            {
                richTextBoxInput.Select(card.IndexStart, card.IndexEnd - card.IndexStart + 1);

                richTextBoxInput.SelectionColor = Color.Green;
                richTextBoxInput.SelectionFont = new Font(richTextBoxInput.Font, FontStyle.Bold);
            }
            isHighlighted = true;
        }

        private void Dehighlight()
        {
            int start = richTextBoxInput.SelectionStart;
            richTextBoxInput.SelectionStart = 0;
            richTextBoxInput.SelectionLength = richTextBoxInput.TextLength;

            richTextBoxInput.SelectionColor = richTextBoxInput.ForeColor;
            richTextBoxInput.SelectionFont = richTextBoxInput.Font;

            richTextBoxInput.SelectionStart = start;
            richTextBoxInput.SelectionLength = 0;
            isHighlighted = false;
        }

        private void ProcessInput()
        {
            try
            {
                List<Lexeme> lexemes = ParserCurs.LexAnalyze(richTextBoxInput.Text); // Лексический анализ
                TFLaComp_1.ParserLogic.Parser parser = new TFLaComp_1.ParserLogic.Parser(lexemes);
                parser.Parse();
                PrintOutput(lexemes, "Синтаксический анализ завершен успешно! 🎉");
            }
            catch (Exception ex)
            {
                PrintOutput(new List<Lexeme>(), $"Ошибка: {ex.Message}");
            }
        }

        private void PrintOutput(List<Lexeme> lexemes, string statusMessage)
        {
            ClearOutput();

            // Добавляем колонки для отображения лексем
            dataGridViewOutput.Columns.Add("LexemeType", "Тип лексемы");
            dataGridViewOutput.Columns.Add("Value", "Значение");

            // Выводим все лексемы
            foreach (var lexeme in lexemes)
            {
                dataGridViewOutput.Rows.Add(lexeme.type.ToString(), lexeme.value);
            }

            // Добавляем строку с результатом проверки
            dataGridViewOutput.Rows.Add("STATUS", statusMessage);

            // Настраиваем внешний вид
            dataGridViewOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOutput.Rows[dataGridViewOutput.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGreen;
            dataGridViewOutput.Rows[dataGridViewOutput.Rows.Count - 1].DefaultCellStyle.Font = new Font(dataGridViewOutput.Font, FontStyle.Bold);
        }

        private void ClearOutput()
        {
            dataGridViewOutput.Rows.Clear();
            dataGridViewOutput.Columns.Clear();
        }

        private void start_Click(object sender, EventArgs e)
        {
            ProcessInput();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessInput();
        }

        private void undo_Click(object sender, EventArgs e)
        {
            _edit.Undo();
        }

        private void redo_Click(object sender, EventArgs e)
        {
            _edit.Redo();
        }

        private void cut_Click(object sender, EventArgs e)
        {
            _edit.Cut();
        }

        private void paste_Click(object sender, EventArgs e)
        {
            _edit.Paste();
        }

        private void copy_Click(object sender, EventArgs e)
        {
            _edit.Copy();
        }

        private void callHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace);
        }

        private void aboutCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.KeywordIndex, "О программе");
        }

        private void expToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // потом должен открываться файл курсовой
            Help.ShowHelp(this, helpProvider1.HelpNamespace,
                HelpNavigator.Topic, HelpHtmDict.TopicDict["Постановка задачи"]);
        }

        private void grammarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // потом должен открываться файл курсовой
            Help.ShowHelp(this, helpProvider1.HelpNamespace,
                HelpNavigator.Topic, HelpHtmDict.TopicDict["Грамматика"]);

        }

        private void classificationgrammarClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // потом должен открываться файл курсовой
            Help.ShowHelp(this, helpProvider1.HelpNamespace,
                HelpNavigator.Topic, HelpHtmDict.TopicDict["Классификация"]);

        }

        private void analysismethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // потом должен открываться файл курсовой
            Help.ShowHelp(this, helpProvider1.HelpNamespace,
                HelpNavigator.Topic, HelpHtmDict.TopicDict["Метод анализа"]);

        }

        private void diagnosticsNeutralizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // потом должен открываться файл курсовой
            Help.ShowHelp(this, helpProvider1.HelpNamespace,
                HelpNavigator.Topic, HelpHtmDict.TopicDict["Диагностика"]);

        }

        private void explToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // потом должен открываться файл курсовой
            Help.ShowHelp(this, helpProvider1.HelpNamespace,
                HelpNavigator.Topic, HelpHtmDict.TopicDict["Тестовый пример"]);

        }

        private void bibliographyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // потом должен открываться файл курсовой
            Help.ShowHelp(this, helpProvider1.HelpNamespace,
                HelpNavigator.Topic, HelpHtmDict.TopicDict["Список литературы"]);

        }

        private void sourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // потом должен открываться файл курсовой
            Help.ShowHelp(this, helpProvider1.HelpNamespace,
                HelpNavigator.Topic, HelpHtmDict.TopicDict["Исходный код"]);

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _edit.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _edit.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _edit.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _edit.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _edit.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _edit.Delete();
        }

        private void pasteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _edit.SelectAll();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы хотите сохранить перед выходом?",
                "Подтверждение",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _logic.Save(richTextBoxInput.Text);
            }
            else if (result == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void richTextBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                _edit.Undo();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                _edit.Redo();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                _edit.SelectAll();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                _edit.Cut();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                _edit.Copy();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                _edit.Paste();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                _logic.Save(richTextBoxInput.Text);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                _logic.Open();
                e.SuppressKeyPress = true;
            }
        }

        private void richTextBoxInput_TextChanged(object sender, EventArgs e)
        {
            OnStateChanged();

            isTextChanged = true;
            if (isHighlighted)
                Dehighlight();
        }

        private void InitEdit()
        {
            _edit = new Edit(richTextBoxInput);
            _edit.StateChanged += OnStateChanged;
            OnStateChanged();
        }

        private void OnStateChanged()
        {
            undo.Enabled = richTextBoxInput.CanUndo;
            redo.Enabled = richTextBoxInput.CanRedo;
        }

        private void курсачToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
