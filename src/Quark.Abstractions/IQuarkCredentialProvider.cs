using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quark.Abstractions;

public interface IQuarkCredentialProvider
{
    IQuarkCredentialProvider AddCredentials(IEnumerable<IQuarkCredential> credentials);
    IQuarkCredentialProvider AddCredential(string name, IQuarkCredential credential);
    Task<IQuarkCredential> GetCredential(string credentialName);
}
