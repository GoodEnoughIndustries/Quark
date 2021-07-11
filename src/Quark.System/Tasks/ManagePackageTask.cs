using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            throw new NotImplementedException();
        }

        public Task<bool> ShouldRunAsync(IQuarkExecutionContext context, IQuarkTarget target)
        {
            throw new NotImplementedException();
        }
    }
}
