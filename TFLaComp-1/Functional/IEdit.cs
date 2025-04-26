using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanfParser.Functional
{
    public interface IEdit
    {
        void Undo();
        void Redo();
        void Cut();
        void Copy();
        void Paste();
        void Delete();
        void SelectAll();
        Action StateChanged { get; set; }
    }
}
