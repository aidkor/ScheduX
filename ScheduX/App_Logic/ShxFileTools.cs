using System.IO;

namespace ScheduX.App_Logic
{
    static class ShxFileTools
    {
       public static void CreateShxFile(string path, string name)
       {
            try
            {
                if (Directory.Exists(path))
                {
                    using (new FileStream($"{path}\\{name}.shx", FileMode.OpenOrCreate)) { }
                }
            }
            catch (System.Exception)
            {                
                throw;
            }
       }
    }
}
