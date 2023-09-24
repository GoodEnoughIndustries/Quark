using Quark.Abstractions;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Quark.Systems;

public class EnvironmentFactsProvider : IQuarkFactProvider
{
    public Task<IQuarkResult> ExecuteAsync(IQuarkExecutionContext context, IQuarkTarget target)
    {
        foreach (DictionaryEntry kvp in Environment.GetEnvironmentVariables())
        {
            target.Facts.Add($"env.{kvp.Key}", kvp.Value!);
        }

        return Task.FromResult((IQuarkResult)new QuarkResult
        {
            QuarkRunResult = QuarkRunResult.Success,
            QuarkTarget = target,
        });
    }
}
