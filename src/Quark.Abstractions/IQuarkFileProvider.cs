using System.IO;
using System.Threading.Tasks;

namespace Quark.Abstractions;

public interface IQuarkFileProvider
{
    Task<FileInfo?> GetFileAsync(string path);
    bool FileExists(string path);
}
