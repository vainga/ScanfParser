using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScanfParser.DTO;

namespace ScanfParser.ResultLog
{
    public interface ISaveResult
    {
       void WriteToLog(List<FullCardDTO> cards);
    }
}
