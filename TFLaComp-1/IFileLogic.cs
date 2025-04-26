using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanfParser
{
    public interface IFileLogic
    {
        void Create(ref string text);
        void Save(string text);
        void SaveAs(string text);
        string Open();
    }
}
