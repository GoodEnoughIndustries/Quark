using Quark.Abstractions;

namespace Quark;

public class QuarkUserNamePasswordCredential : IQuarkCredential
{
    private readonly string username;
    private readonly string password;

    public QuarkUserNamePasswordCredential(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

    public string GetPassword() => this.password;

    public string GetUsername() => this.username;
}
