using Quark.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quark;

public class QuarkCredentialProvider : IQuarkCredentialProvider
{
    private Dictionary<string, IQuarkCredential> credentials = new();

    public IQuarkCredentialProvider AddCredential(string name, IQuarkCredential credential)
    {
        this.credentials.Add(name, credential);

        return this;
    }

    public IQuarkCredentialProvider AddCredentials(IEnumerable<IQuarkCredential> credentials)
    {
        foreach (var credential in credentials)
        {
            this.AddCredential(credential.GetUsername(), credential);
        }

        return this;
    }

    public Task<IQuarkCredential> GetCredential(string credentialName)
    {
        if (this.credentials.TryGetValue(credentialName, out var credential))
        {
            return Task.FromResult(credential);
        }

        throw new QuarkCredentialNotFoundException(nameof(credentialName));
    }
}
