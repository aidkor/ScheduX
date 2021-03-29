using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduX.Resourses.AppLogic
{
    public abstract class Dictionary<Element>
    {
        public abstract IDBHandable DataHandler { get; set; }
        public abstract List<Element> dictionaryList { get; set; }
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
    
    //=============================================
    //=============================================
    
    public class SchoolStudyPeriodDictionary : StudyPeriodDictionary
    {
        public override IDBHandable DataHandler { get; set; }
        public override List<PeriodElement> dictionaryList { get; set; }
        public SchoolStudyPeriodDictionary()
        {
            dictionaryList = new List<PeriodElement>();
        }
    }
    public class SchoolGroupDictionary : GroupDictionary
    {
        public override IDBHandable DataHandler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override List<GroupElement> dictionaryList { get; set; }
    }
    public class SchoolTeacherDictionary : TeacherDictionary
    {
        public override IDBHandable DataHandler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override List<TeacherElement> dictionaryList { get; set; }

    }
    public class CallScheduleDictionary : Dictionary<CallScheduleElement>
    {
        public override IDBHandable DataHandler { get; set; }
        public override List<CallScheduleElement> dictionaryList { get; set; }
        public CallScheduleElement CurrentSelected { get; set; }
        public CallScheduleDictionary()
        {            
            dictionaryList = new List<CallScheduleElement>();
        }    
    }
}
