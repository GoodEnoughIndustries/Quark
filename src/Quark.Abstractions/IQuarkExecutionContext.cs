using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Quark.Abstractions
{
    public interface IQuarkExecutionContext
    {
        IQuarkConfiguration Configuration { get; }
        List<IQuarkTarget> Targets { get; }
        Task BuildTargetsAsync(CancellationToken token);
        Task BuildTasksAsync(CancellationToken token);
        Task BuildResultsAsync(CancellationToken token);
        QuarkResult GetFinalResult();
        IAsyncEnumerable<IQuarkTask> ExecuteTasksAsync(CancellationToken token);
    }
}
