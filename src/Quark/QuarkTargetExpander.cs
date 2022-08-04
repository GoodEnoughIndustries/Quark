using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Quark;

public class QuarkTargetExpander : IQuarkTargetExpander
{
    private const string RangePattern = @".*(?<range>\[(?<begin>\d*):(?<end>\d*)\]).*";
    private Regex rangeRegex;

    public QuarkTargetExpander()
    {
        this.rangeRegex = new Regex(RangePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }

    public bool TryExpand(IQuarkTargetGroup targetGroup, out IEnumerable<IQuarkTarget> targets)
    {
        var expandedTargets = new List<IQuarkTarget>();

        var matches = this.rangeRegex.Match(targetGroup.Pattern);
        if (matches.Success
            && int.TryParse(matches.Groups["begin"].Value, out var begin)
            && int.TryParse(matches.Groups["end"].Value, out var end))
        {
            var range = matches.Groups["range"];
            foreach (var num in Enumerable.Range(begin, end - begin + 1))
            {
                var t = targetGroup.Pattern.Replace(range.Value, num.ToString(CultureInfo.InvariantCulture));
                expandedTargets.Add(new QuarkTarget
                {
                    Name = t,
                    Type = targetGroup.Type,
                });
            }

            targets = expandedTargets;
            return true;
        }

        targets = Enumerable.Empty<IQuarkTarget>();
        return false;
    }

}
