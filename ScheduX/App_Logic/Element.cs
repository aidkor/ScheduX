using System;

namespace ScheduX.App_Logic
{
    public abstract class Element
    {
        public virtual string Name { get; set; }
        public Element(string name)
        {
            Name = name;
        }
    }
    public abstract class PeriodElement : Element
    {       
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public PeriodElement(string name, DateTime startYear, DateTime endYear)
            : base(name)
        {            
            Start = startYear;
            End = endYear;
        }
    }
    public abstract class GroupElement : Element
    {        
        public virtual int StudentQuantity { get; set; }        
        public GroupElement(string name, int studentQuantity)
            : base(name)
        {            
            StudentQuantity = studentQuantity;            
        }
    }
    public abstract class TeacherElement : Element
    {
        public virtual string FullName { get; set; }
        public virtual string Post { get; set; }
        public virtual int Experience { get; set; }
        public virtual string Address { get; set; }
        public virtual string Telephone { get; set; }
        public TeacherElement(string name, string post, int experience, string address, string telephone)
            : base(name)
        {            
            Post = post;
            Experience = experience;
            Address = address;
            Telephone = telephone;
            FullName = name + $"({post})" + " - " + experience + $"{(experience > 1 ? " years exp" : " year exp")}";
        }
    }
    public abstract class AudienceElement : Element
    {
        public virtual string FullName { get; set; }
        public virtual string AudienceType { get; set; }
        public virtual int Capacity { get; set; }
        public AudienceElement(string name, string audienceType, int capacity)
            : base(name)
        {            
            AudienceType = audienceType;
            Capacity = capacity;
            FullName = name + $"({audienceType})" + " - " + capacity + " places";
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
    public abstract class Lesson : Element
    {
        public virtual int LessonsPerWeek { get; set; }
        public virtual SubjectElement Subject { get; set; }
        public virtual TeacherElement Teacher { get; set; }
        public virtual GroupElement Group { get; set; }
        public virtual AudienceElement Audience { get; set; }
        public Lesson(string name, SubjectElement subject, TeacherElement teacher, AudienceElement audience, GroupElement group)
            : base(name)
        {
            Name = name;
            Subject = subject;
            Teacher = teacher;
            Audience = audience;
            Group = group;
        }
        public Lesson(string name, SubjectElement subject, TeacherElement teacher, AudienceElement audience, GroupElement group, int lessonsPerWeek)
            : base(name)
        {
            Name = name;
            Subject = subject;
            Teacher = teacher;
            Audience = audience;
            Group = group;
            LessonsPerWeek = lessonsPerWeek;
        }
    }

    // ================
    public class SchoolPeriod : PeriodElement
    {
        public SchoolPeriod(string name, DateTime startYear, DateTime endYear)
            : base(name, startYear, endYear)
        {

        }
    }
    public class SchoolGroup : GroupElement
    {
        public SchoolGroup(string name, int studentQuantity)
            : base(name, studentQuantity)
        {

        }
    }
    public class SchoolTeacher : TeacherElement
    {
        public SchoolTeacher(string name, string post, int expirience, string address, string telephone)
            : base(name, post, expirience, address, telephone)
        {

        }
    }
    public class SchoolAudience : AudienceElement
    {
        public SchoolAudience(string name, string audienceType, int capacity)
            : base(name, audienceType, capacity)
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
    public class SchoolLesson : Lesson
    {
        public byte Day { get; set; }
        public byte Hour { get; set; }
        public SchoolLesson(string name, SubjectElement subject, TeacherElement teacher, AudienceElement audience, GroupElement group, byte day = 0, byte hour = 0)
            : base(name, subject, teacher, audience, group)
        {
            Day = day;
            Hour = hour;
        }
        public SchoolLesson(string name, SubjectElement subject, TeacherElement teacher, AudienceElement audience, GroupElement group, int lessonsPerWeek, byte day = 0, byte hour = 0)
           : base(name, subject, teacher, audience, group, lessonsPerWeek)
        {
            Day = day;
            Hour = hour;
        }
    }

}
