using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduX.Resourses.AppLogic
{
    abstract class Element { }
    abstract class PeriodElements : Element
    {
        public abstract string Name { get; set; }
    }
    class SchoolPeriod : PeriodElements
    {
        public override string Name { get; set; }
        public uint WorkingWeeks { get; set; }
        public uint StartYear { get; set; }
        public uint EndYear { get; set; }
        public SchoolPeriod(string name, uint workingWeeks, uint startYear, uint endYear)
        {
            Name = name;
            WorkingWeeks = workingWeeks;
            StartYear = startYear;
            EndYear = endYear;
        }
    }
}
