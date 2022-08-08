using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Quark.Abstractions;

public interface IQuarkFileSystem
{
    Task<FileInfo?> GetFileAsync(string path);
    IEnumerable<IQuarkFileProvider> FileProviders { get; init; }

    string GetTemporaryDirectory();
}
