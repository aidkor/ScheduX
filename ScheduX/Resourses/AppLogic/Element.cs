using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduX.Resourses.AppLogic
{
    public abstract class Element
    {        
        public virtual string Name { get; set; }        
        // Add Shx Project Type Dependency 
    }
    public abstract class PeriodElement : Element
    {
    }
    public abstract class CallScheduleElement : Element
    {
    }
    public abstract class GroupElement : Element
    {
        public abstract uint StudentQuantity { get; set; }
    }
    public abstract class TeacherElement : Element
    {        
    }

    //=============================================
    //=============================================
    public class SchoolPeriodElement : PeriodElement
    {                
        public uint WorkingWeeks { get; set; }
        public uint StartYear { get; set; }
        public uint EndYear { get; set; }
        public SchoolPeriodElement(string name, uint workingWeeks, uint startYear, uint endYear)
        {            
            Name = name;
            WorkingWeeks = workingWeeks;
            StartYear = startYear;
            EndYear = endYear;
        }
    }    
    public class SchoolCallScheduleElement : CallScheduleElement
    {
        public Call[] Calls { get; set; }
        public byte LessonsPerDay { get; set; }
        public byte WorkingDays { get; set; }
        public SchoolCallScheduleElement(byte lessonsPerDay, byte workingDays)
        {
            LessonsPerDay = lessonsPerDay;
            WorkingDays = workingDays;

        }
       /* private void DefaultCallsArrayInitialization()
        {
            Calls = new Call[LessonsPerDay];
            byte duration = 45, start = 9;
            for (int i = 0; i < LessonsPerDay; i++)
            {
                Calls[i] = new Call(duration, (i.ToString(), i.ToString()), (9,0), ());
            }
        }*/
    }
    public class Call
    {
        public (string,string) Name { get; set; }
        public uint Duration { get; set; }
        public uint Break { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
       /* public Call((string, string) name, uint duration, (byte,byte) start, (byte, byte) end)
        {
            Duration = duration;
            Name = name;
            Start = start;
            End = end;
        }*/
    }
    public class SchoolClass : GroupElement
    {
        public override uint StudentQuantity { get; set; }
        public uint MaxDayLoad { get; set; }
        public uint MaxLessonsPerDay { get; set; }
        public SchoolClass(string name, uint studentQuantity, uint maxDayLoad, uint maxLessonsPerDay)
        {
            Name = name;
            StudentQuantity = studentQuantity;
            MaxDayLoad = maxDayLoad;
            MaxLessonsPerDay = maxLessonsPerDay;
        }
    }
    public class SchoolTeacher : TeacherElement
    {        
        public uint MaxLessonsPerDay { get; set; }
        public SchoolTeacher(string name,uint maxLessonsPerDay)
        {
            Name = name;
            MaxLessonsPerDay = maxLessonsPerDay;
        }
    }

}
