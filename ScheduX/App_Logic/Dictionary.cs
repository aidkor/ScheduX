using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduX.DB_Logic;

namespace ScheduX.App_Logic
{
    public abstract class Dictionary
    {
        public virtual IDBHandable DataHandler { get; set; }       

    }
    public abstract class StudyPeriodDictionary : Dictionary
    {

    }
    public abstract class GroupDictionary  : Dictionary
    {
        
    }
    public abstract class TeacherDictionary : Dictionary
    {
       
    }
    public abstract class AudienceDictionary : Dictionary
    {
       
    }
    public abstract class SubjectDictionary : Dictionary
    {
       
    }
    public abstract class LessonDictionary : Dictionary
    {
        
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
