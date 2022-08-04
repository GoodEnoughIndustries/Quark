using System;

namespace Quark;

public class QuarkCredentialNotFoundException : Exception
{
    public QuarkCredentialNotFoundException() : base()
    {
    }

    public QuarkCredentialNotFoundException(string? message) : base(message)
    {
    }

    public QuarkCredentialNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
