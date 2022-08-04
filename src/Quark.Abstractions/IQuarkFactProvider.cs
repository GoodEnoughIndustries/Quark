using System.Threading.Tasks;

namespace Quark.Abstractions;

public interface IQuarkFactProvider
{
    Task<IQuarkResult> ExecuteAsync(IQuarkExecutionContext context, IQuarkTarget target);
}
