using System.Text;

namespace ScanfParser.Functional
{
    public class Edit : IEdit
    {
        private RichTextBox _richTextBox;

        public Action StateChanged { get; set; }

        public Edit(RichTextBox richTextBox)
        {
            _richTextBox = richTextBox;
        }

        public void Delete()
        {
            int selectionStart = _richTextBox.SelectionStart;
            _richTextBox.Text = _richTextBox.Text.Remove(_richTextBox.SelectionStart, _richTextBox.SelectionLength);
            _richTextBox.SelectionStart = selectionStart;
        }

        public void Cut()
        {
            _richTextBox.Cut();
        }

        public void Copy()
        {
            _richTextBox.Copy();
        }

        public void Paste()
        {
            _richTextBox.Paste(DataFormats.GetFormat("Text"));
        }

        public void Undo()
        {
            _richTextBox.Undo();
            StateChanged?.Invoke();
        }

        public void Redo()
        {
            _richTextBox.Redo();
            StateChanged?.Invoke();
        }

        public void SelectAll()
        {
            _richTextBox.SelectAll();
        }
    }
}
