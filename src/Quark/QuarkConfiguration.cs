using Quark.Abstractions;
using System;

namespace Quark
{
    public class QuarkConfiguration : IQuarkConfiguration
    {
        public QuarkConfiguration(QuarkConfigurationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
        }
    }
}
