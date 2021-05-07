using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduX.Resourses.AppLogic
{
    public abstract class StudyPeriodDictionary 
    {
        public virtual IDBHandable DataHandler { get; set; }       
    }
    public abstract class GroupDictionary 
    {
        public virtual IDBHandable DataHandler { get; set; }        
    }
    public abstract class TeacherDictionary 
    {
        public virtual IDBHandable DataHandler { get; set; }      
    }
    public abstract class AudienceDictionary 
    {
        public virtual IDBHandable DataHandler { get; set; }     
    }
    public abstract class SubjectDictionary 
    {
        public virtual IDBHandable DataHandler { get; set; }     
    }
    public abstract class LessonDictionary 
    {
        public virtual IDBHandable DataHandler { get; set; }      
    }
    //=============================================
    //=============================================

    public class SchoolStudyPeriodDictionary : StudyPeriodDictionary
    {
        public virtual List<SchoolPeriod> dictionaryList { get; set; }
        public SchoolStudyPeriodDictionary()
        {
            dictionaryList = new List<SchoolPeriod>();
        }
    }
    public class SchoolGroupDictionary : GroupDictionary
    {
        public virtual List<SchoolGroup> dictionaryList { get; set; }
        public SchoolGroupDictionary()
        {
            dictionaryList = new List<SchoolGroup>();
        }
    }
    public class SchoolTeacherDictionary : TeacherDictionary
    {
        public virtual List<SchoolTeacher> dictionaryList { get; set; }
        public SchoolTeacherDictionary()
        {
            dictionaryList = new List<SchoolTeacher>();
        }
    }
    public class SchoolAudienceDictionary : AudienceDictionary
    {
        public virtual List<SchoolAudience> dictionaryList { get; set; }
        public SchoolAudienceDictionary()
        {
            dictionaryList = new List<SchoolAudience>();
        }
    }
    public class SchoolSubjectDictionary : SubjectDictionary
    {
        public virtual List<SchoolSubject> dictionaryList { get; set; }
        public SchoolSubjectDictionary()
        {
            dictionaryList = new List<SchoolSubject>();
        }
    }
    public class SchoolLessonDictionary : LessonDictionary
    {
        public virtual List<SchoolLesson> dictionaryList { get; set; }
        public SchoolLessonDictionary()
        {
            dictionaryList = new List<SchoolLesson>();
        }
    }


}
