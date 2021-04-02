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
        public Element(string name)
        {
            Name = name;
        }
        // Add Shx Project Type Dependency 
    }
    public abstract class PeriodElement : Element
    {
        public virtual int StartYear { get; set; }
        public virtual int EndYear { get; set; }
        public PeriodElement(string name, int startYear, int endYear)
            : base(name)
        {
            StartYear = startYear;
            EndYear = endYear;
        }
    }
    public abstract class GroupElement : Element
    {
        public virtual int StudentQuantity { get; set; }
        public GroupElement(string name,int studentQuantity)
            : base(name)
        {
            StudentQuantity = studentQuantity;
        }
    }
    public abstract class TeacherElement : Element
    {
        public TeacherElement(string name)
            : base(name)
        {

        }
    }
    public abstract class AudienceElement : Element
    {
        public virtual string AudienceType { get; set; }
        public virtual int Capacity { get; set; }
        public AudienceElement(string name, string audienceType, int capacity)
            : base(name)
        {
            AudienceType = audienceType;
            Capacity = capacity;
        }
    }
    public abstract class SubjectElement : Element
    {
        public virtual int Complexity { get; set; }
        public SubjectElement(string name, int complexity)
            : base(name)
        {
            Complexity = complexity;
        }
    }
    public abstract class TimetableCallsElement : Element
    {
        public virtual Call[] Calls { get; set; }
        public virtual int WorkingDays { get; set; }
        public virtual int LessonsPerDay { get; set; }
        public TimetableCallsElement(string name, int workingDays, int lessonsPerDay)
            : base(name)
        {
            WorkingDays = workingDays;
            LessonsPerDay = lessonsPerDay;            
        }
    }
    public abstract class Call
    {        
        public virtual string FullName { get; set; }
        public virtual string Shortening { get; set; }
        public virtual int Duration { get; set; }       
        public virtual int Break { get; set; }
        public virtual string Starts { get; set; }
        public virtual string Ends { get; set; }
        public Call(string fullName,string shortening, int duration, int @break)
        {

        }

    }

    //=============================================
    //=============================================
    public class SchoolPeriod : PeriodElement
    {                
        public int WorkingWeeks { get; set; }   
        public SchoolPeriod(string name, int workingWeeks, int startYear, int endYear)
            : base(name,startYear,endYear)
        {            
            WorkingWeeks = workingWeeks;
        }
    }    
    public class SchoolTimetableCalls : TimetableCallsElement
    {
        public SchoolTimetableCalls(string name, int workingDays, int lessonsPerDay)
            : base(name,workingDays,lessonsPerDay)
        {
        }
    }
    public class SchoolGroup : GroupElement
    {        
        public int MaxDayLoad { get; set; }
        public int MaxLessonsPerDay { get; set; }
        public SchoolGroup(string name, int studentQuantity, int maxDayLoad, int maxLessonsPerDay)
            : base(name, studentQuantity)
        {            
            MaxDayLoad = maxDayLoad;
            MaxLessonsPerDay = maxLessonsPerDay;
        }
    }
    public class SchoolTeacher : TeacherElement
    {        
        public int MaxLessonsPerDay { get; set; }
        public SchoolTeacher(string name,int maxLessonsPerDay)
            : base(name)
        {
            MaxLessonsPerDay = maxLessonsPerDay;
        }
    }
    public class SchoolAudience : AudienceElement
    {
        public SchoolAudience(string name, string audienceType,int capacity)
            : base(name,audienceType,capacity)
        {
        }
    }
    public class SchoolSubject : SubjectElement
    {
        public SchoolSubject(string name, int complexity)
            : base(name, complexity)
        {

        }
    }

}
