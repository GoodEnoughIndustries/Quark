using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quark
{
    public class QuarkExecutionContext : IQuarkExecutionContext
    {
        public QuarkExecutionContext(IQuarkConfiguration configuration) => this.Configuration = configuration;

        public IQuarkConfiguration Configuration { get; }
        public List<IQuarkTarget> Targets { get; } = new();

        public Task BuildResultsAsync(CancellationToken token) => throw new NotImplementedException();

        public Task BuildTargetsAsync()
        {
            return Task.CompletedTask;
        }

        public Task BuildTargetsAsync(CancellationToken token)
            => throw new NotImplementedException();

        public Task BuildTasksAsync()
        {
            return Task.CompletedTask;
        }

        public Task BuildTasksAsync(CancellationToken token)
            => throw new NotImplementedException();

        public IAsyncEnumerable<IQuarkTask> ExecuteTasksAsync(CancellationToken token)
            => throw new NotImplementedException();

        public QuarkResult GetFinalResult() => throw new NotImplementedException();
    }
}
