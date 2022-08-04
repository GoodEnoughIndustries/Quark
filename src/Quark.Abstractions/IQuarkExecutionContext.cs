using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Quark.Abstractions;

public interface IQuarkExecutionContext
{
    IQuarkConfiguration Configuration { get; }
    List<IQuarkTarget> Targets { get; }
    List<IQuarkResult> Results { get; }
    Task ExecuteTasksAsync(QuarkContext context, CancellationToken token);
    Task BuildAllAsync(QuarkContext context, CancellationToken token);
}
