using System.Threading;
using System.Threading.Tasks;

namespace Quark.Abstractions
{
    public interface IQuarkTask
    {
        public string TaskName => this.GetType().Name;
        Task<bool> ShouldRunAsync(IQuarkExecutionContext context, IQuarkTarget target);
        Task<QuarkResult> ExecuteAsync(IQuarkExecutionContext context, IQuarkTask target);
        Task BuildAsync(CancellationToken token);
    }
}
