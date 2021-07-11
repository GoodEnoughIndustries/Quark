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
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            IQuarkExecutionContext context = new QuarkExecutionContext(configuration);
            await context.BuildTargetsAsync(token).ConfigureAwait(false);
            await context.BuildTasksAsync(token).ConfigureAwait(false);

            await foreach(var task in context.ExecuteTasksAsync(token).ConfigureAwait(false))
            {

            }

            await context.BuildResultsAsync(token).ConfigureAwait(false);

            return context.GetFinalResult();
        }
    }
}
