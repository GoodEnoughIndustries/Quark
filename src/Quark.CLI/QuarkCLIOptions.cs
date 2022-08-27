using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quark.CLI;

public class QuarkCLIOptions
{
    public QuarkCLIOptions()
    {

    }

    public IEnumerable<QuarkTarget> Targets { get; set; } = new List<QuarkTarget>();
}
