using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduX.Resourses.AppLogic
{
    interface IDBHandable
    {
        void Add();
        void Edit();
        void Remove();
    }
    interface StudyPeriodDictionaryDBHandler : IDBHandable
    {

    }
}
