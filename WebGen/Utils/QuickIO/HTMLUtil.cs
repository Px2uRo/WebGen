using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGen.Utils.QuickIO
{
    public static class HTMLUtil
    {
        public static bool TrySave(string targetFile,string content)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile));
                Save(targetFile, content);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        private static void Save(string targetFile, string content)
        {
            File.WriteAllText(targetFile, content);
        }
    }
}
