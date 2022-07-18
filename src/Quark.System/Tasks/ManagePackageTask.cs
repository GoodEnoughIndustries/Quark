using Quark.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quark.System.Tasks
{
    public class ManagePackageTask : IQuarkTask
    {
        public IQuarkPackage PackageDescription { get; }
        public bool ShouldExist { get; }

        public ManagePackageTask(IQuarkPackage packageDescription, bool shouldExist)
        {
            this.PackageDescription = packageDescription;
            this.ShouldExist = shouldExist;
        }

        public Task<QuarkResult> ExecuteAsync(IQuarkExecutionContext context, IQuarkTask target)
        {
            return Task.FromResult(new QuarkResult
            {

            });
        }

        public Task<bool> ShouldRunAsync(IQuarkExecutionContext context, IQuarkTarget target)
        {
            return Task.FromResult(true);
        }

        public Task BuildAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
