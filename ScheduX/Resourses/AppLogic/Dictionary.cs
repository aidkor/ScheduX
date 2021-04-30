using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduX.Resourses.AppLogic
{
    public abstract class Dictionary<Element>
    {
        public virtual IDBHandable DataHandler { get; set; }
        public virtual List<Element> dictionaryList { get; set; }
        // Add Shx Project Type Dependency 
    }
    public abstract class StudyPeriodDictionary : Dictionary<PeriodElement>
    {                
       
    }
    public abstract class GroupDictionary : Dictionary<GroupElement>
    {

    }
    public abstract class TeacherDictionary : Dictionary<TeacherElement>
    {

    }
    public abstract class AudienceDictionary : Dictionary<AudienceElement>
    {

    }
    public abstract class SubjectDictionary : Dictionary<SubjectElement>
    {

    }
    public abstract class LessonDictionary : Dictionary<Lesson>
    {

    }
    //=============================================
    //=============================================

    public class SchoolStudyPeriodDictionary : StudyPeriodDictionary
    {
      
        public SchoolStudyPeriodDictionary()
        {
            dictionaryList = new List<PeriodElement>();
        }
    }
    public class SchoolGroupDictionary : GroupDictionary
    {
        public SchoolGroupDictionary()
        {
            dictionaryList = new List<GroupElement>();
        }
    }
    public class SchoolTeacherDictionary : TeacherDictionary
    {
        public SchoolTeacherDictionary()
        {
            dictionaryList = new List<TeacherElement>();
        }
    }
    public class SchoolAudienceDictionary : AudienceDictionary
    {
        public SchoolAudienceDictionary()
        {
            dictionaryList = new List<AudienceElement>();
        }
    }
    public class SchoolSubjectDictionary : SubjectDictionary
    {
        public SchoolSubjectDictionary()
        {
            dictionaryList = new List<SubjectElement>();
        }
    }
    public class SchoolLessonDictionary : LessonDictionary
    {
        public SchoolLessonDictionary()
        {
            dictionaryList = new List<Lesson>();
        }
    }


}
