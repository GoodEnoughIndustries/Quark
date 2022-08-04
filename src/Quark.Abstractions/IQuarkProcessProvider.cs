using System.Diagnostics;
using System.Threading.Tasks;

namespace Quark.Abstractions
{
    public interface IQuarkProcessProvider
    {
        Task<Process?> Start(ProcessStartInfo psi);
    }
}
