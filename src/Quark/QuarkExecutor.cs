using Quark.Abstractions;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Quark
{
    public class QuarkExecutor : IQuarkExecutor
    {
        public async static Task<QuarkResult> RunAsync(IQuarkConfiguration configuration, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            IQuarkExecutionContext context = new QuarkExecutionContext(configuration);
            await context.BuildAllAsync(token);
            await context.ValidateAsync(token);
            await context.ExecuteTasksAsync(token).ConfigureAwait(false);

            await context.BuildResultsAsync(token).ConfigureAwait(false);

            return context.GetFinalResult();
        }
    }
}
