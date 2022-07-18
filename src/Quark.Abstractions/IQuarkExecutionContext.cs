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
        Task BuildResultsAsync(CancellationToken token);
        QuarkResult GetFinalResult();
        Task ExecuteTasksAsync(CancellationToken token);
        Task BuildAllAsync(CancellationToken token);
        Task ValidateAsync(CancellationToken token);
    }
}
