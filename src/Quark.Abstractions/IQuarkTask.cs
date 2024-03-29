using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quark.Abstractions;

public interface IQuarkTask
{
    public string TaskName { get; init; }
    List<IQuarkTarget> Targets { get; init; }
    Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTargetManager manager, IQuarkTarget target);
}
