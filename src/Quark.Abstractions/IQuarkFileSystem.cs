using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quark.Abstractions;

public interface IQuarkFileSystem
{
    Task<FileInfo?> GetFileAsync(string path);
    IEnumerable<IQuarkFileProvider> FileProviders { get; init; }
}
