using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EM.InsurePlus.Helpers
{
    internal static class UriBuilderExtensions
    {
        internal static void AppendToPath(this UriBuilder builder, string pathToAdd)
        {
            var completePath = Path.Combine(builder.Path, pathToAdd);
            builder.Path = completePath;
        }
    }
}
