using Quark.Abstractions;

namespace Quark;

public class QuarkRunbook : IQuarkRunbook
{
    public QuarkRunbook(string runbook)
        => this.Runbook = runbook;

    public string Runbook { get; }
}
