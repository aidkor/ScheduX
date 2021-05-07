using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduX.Resourses.AppLogic
{
    public abstract class PeriodElement
    {
        public virtual string Name { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public PeriodElement(string name, DateTime startYear, DateTime endYear)
        {
            Name = name;
            Start = startYear;
            End = endYear;
        }
    }
    public abstract class GroupElement
    {
        public virtual string Name { get; set; }
        public virtual int StudentQuantity { get; set; }        
        public GroupElement(string name, int studentQuantity)
        {
            Name = name;
            StudentQuantity = studentQuantity;            
        }
    }
    public abstract class TeacherElement
    {
        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Post { get; set; }
        public virtual int Experience { get; set; }
        public virtual string Address { get; set; }
        public virtual string Telephone { get; set; }
        public TeacherElement(string name, string post, int experience, string address, string telephone)
        {

            Name = name;
            Post = post;
            Experience = experience;
            Address = address;
            Telephone = telephone;
            FullName = name + $"({post})" + " - " + experience + $"{(experience > 1 ? " years exp" : " year exp")}";
        }
    }
    public abstract class AudienceElement
    {
        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        public virtual string AudienceType { get; set; }
        public virtual int Capacity { get; set; }
        public AudienceElement(string name, string audienceType, int capacity)
        {

            Name = name;
            AudienceType = audienceType;
            Capacity = capacity;
            FullName = name + $"({audienceType})" + " - " + capacity + " places";
        }
    }
    public abstract class SubjectElement
    {
        public virtual string Name { get; set; }        
        public virtual int Complexity { get; set; }
        public SubjectElement(string name, int complexity)
        {
            Name = name;
            Complexity = complexity;
        }
    }
    public abstract class Lesson
    {

        public virtual string Name { get; set; }
        public virtual SubjectElement Subject { get; set; }
        public virtual TeacherElement Teacher { get; set; }
        public virtual GroupElement Group { get; set; }
        public virtual AudienceElement Audience { get; set; }
        public Lesson(string name, SubjectElement subject, TeacherElement teacher, AudienceElement audience, GroupElement group)
        {
            Name = name;
            Subject = subject;
            Teacher = teacher;
            Audience = audience;
            Group = group;
        }
    }
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
    }

}
