using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunc
{
    class Program
    {
        static void Main(string[] args)
        {
            ProjectTextBox_TextChanged();
            Console.ReadKey();
        }



        private static void ProjectTextBox_TextChanged()
        {
            string scheduxProjectDir = @"C:\Users\Asus\Desktop\Proj";
            ArrayList shxFiles = new ArrayList();
            SearchSystem(scheduxProjectDir, ref shxFiles);
            for (int i = 0; i < shxFiles.Count; i++)
            {
                Console.WriteLine(shxFiles[i]);
            }
        }
        private static void SearchSystem(string dirPath, ref ArrayList files)
        {
            try
            {
                if (Directory.Exists(dirPath))
                {
                    foreach (string item in FindShxFile(Directory.GetFiles(dirPath)))
                    {
                        files.Add(item);
                    }
                    foreach (string item in Directory.GetDirectories(dirPath))
                    {
                        SearchSystem(item, ref files);
                    }
                }
            }
            catch
            {              
                return;                
            }

        }
        private static ArrayList FindShxFile(string[] files)
        {
            ArrayList result = new ArrayList();

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Split('.')[files[i].Split('.').Length - 1] == "shx")
                {
                    result.Add(files[i]);
                }
            }

            return result;
        }
    }
}
