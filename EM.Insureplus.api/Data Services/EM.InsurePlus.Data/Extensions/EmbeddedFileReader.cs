using System.Reflection;

namespace EM.InsurePlus.Data.Extensions
{
    public class EmbeddedFileReader
    {
        public string ReadEmbeddedResource(string fileNamespace, string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var asemblyStream = assembly.GetManifestResourceStream($"{fileNamespace}.{filename}");

            if (asemblyStream != null)
            {
                using (Stream stream = asemblyStream)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        return result;
                    }
                }
            }
            else
                return string.Empty;
        }
    }
}