using System;
using System.IO;

namespace ScheduX.Resourses.AppLogic
{
    class ShxFile
    {
        public string Name { get; set; }
        public string LocationPath { get; set; }

        public ShxFile(string name, string locationPath)
        {
            Name = name;
            LocationPath = locationPath;
        }
        public ShxFile Initialize()
        {
            using (new FileStream($@"{LocationPath}\{Name}.shx", FileMode.CreateNew)) { }
            return this;            
        }
    }
}
