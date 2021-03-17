using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduX.Resourses.AppLogic
{
    abstract class Dictionary<T>
    {
        public static IDBHandable DataHandler { get; }
        public virtual List<T> dictionaryList { get; set; }
    }
    class StudyPeriodDictionary : Dictionary<PeriodElements>
    {        
        public StudyPeriodDictionary()
        {
            dictionaryList = new List<PeriodElements>();
        }
               
    }
}
