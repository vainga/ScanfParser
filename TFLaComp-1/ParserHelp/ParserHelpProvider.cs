using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanfParser.ParserHelp
{
    public class ParserHelpProvider : IParserHelpProvider
    {
        public const string HelpFileName = $"ParserHelp\\res\\parserHelpProvider.chm";

        public HelpProvider HelpProvider { get; }

        public ParserHelpProvider()
        {
            HelpProvider = new();
            HelpProvider.HelpNamespace = HelpFileName;
        }

        public ParserHelpProvider(string helpChm)
        {
            HelpProvider = new();
            HelpProvider.HelpNamespace = helpChm;
        }

        public void SetHelp(Control control, string keyword, HelpNavigator helpNavigator = HelpNavigator.TableOfContents)
        {
            HelpProvider.SetShowHelp(control, true);
            HelpProvider.SetHelpKeyword(control, keyword);
            HelpProvider.SetHelpNavigator(control, helpNavigator);
        }
    }
}
