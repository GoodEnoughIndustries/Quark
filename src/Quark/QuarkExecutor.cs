using Quark.Abstractions;
using System;
using System.Threading.Tasks;

namespace Quark
{
    public class QuarkExecutor : IQuarkExecutor
    {
        public Task<QuarkResult> RunAsync(IQuarkConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return Task.FromResult(new QuarkResult());
        }
    }
}
