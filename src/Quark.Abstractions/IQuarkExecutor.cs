using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Quark.Abstractions;

public interface IQuarkExecutor
{
    Task<QuarkResult> RunAsync(QuarkContext context, CancellationToken cancellationToken = default);
}
