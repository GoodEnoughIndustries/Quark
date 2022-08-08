using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quark;

public class QuarkExecutor : IQuarkExecutor
{
    public async Task<QuarkResult> RunAsync(QuarkContext context, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(context);

        foreach (var configuration in context.Configurations)
        {
            // TODO: inject executioncontexts
            //IQuarkExecutionContext executionContext = new QuarkExecutionContext(configuration);
            //await executionContext.BuildAllAsync(context, token);
            //await executionContext.ExecuteTasksAsync(context, token).ConfigureAwait(false);
        }

        return new QuarkResult
        {

        };
    }
}
