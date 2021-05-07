using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScheduX.Resourses.AppLogic
{
    static class FitnessFunctions
    {
        public static int GroupWindowPenalty = 10;//штраф за окно у группы
        public static int TeacherWindowPenalty = 7;//штраф за окно у преподавателя
        public static int LateLessonPenalty = 1;//штраф за позднюю пару

        public static int LatesetHour = 3;//максимальный час, когда удобно проводить пары

        /// <summary>
        /// Штраф за окна
        /// </summary>
        public static int Windows(Plan plan)
        {
            var res = 0;

            for (byte day = 0; day < Plan.DaysPerWeek; day++)
            {
                var groupHasLessions = new HashSet<GroupElement>();
                var teacherHasLessions = new HashSet<TeacherElement>();

                for (byte hour = 0; hour < Plan.HoursPerDay; hour++)
                {
                    foreach (var pair in plan.HourPlans[day, hour].GroupToTeacher)
                    {
                        var group = pair.Key;
                        var teacher = pair.Value;
                        if (groupHasLessions.Contains(group) && !plan.HourPlans[day, hour - 1].GroupToTeacher.ContainsKey(group))
                            res += GroupWindowPenalty;
                        if (teacherHasLessions.Contains(teacher) && !plan.HourPlans[day, hour - 1].TeacherToGroup.ContainsKey(teacher))
                            res += TeacherWindowPenalty;

                        groupHasLessions.Add(group);
                        teacherHasLessions.Add(teacher);
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Штраф за поздние пары
        /// </summary>
        public static int LateLesson(Plan plan)
        {
            var res = 0;
            foreach (var lesson in plan.GetLessons())
                if (lesson.Hour > LatesetHour)
                    res += LateLessonPenalty;

            return res;
        }
    }
    class Solver // Решатель (генетический алгоритм)
    {
        public int MaxIterations = 4;
        public int PopulationCount = 4;//должно делиться на 4

        public List<Func<Plan, int>> FitnessFunctions = new List<Func<Plan, int>>();
        public int Fitness(Plan plan)
        {
            var res = 0;

            foreach (var f in FitnessFunctions)
                res += f(plan);

            return res;
        }
        public Plan Solve(List<SchoolLesson> lessons)
        {
            var pop = new Population(lessons, PopulationCount);
            if (pop.Count == 0)
                throw new Exception("Can not create any plan"); // HACK: Change to MessageBox 
                                                                //
            var count = MaxIterations;
            while (count-- > 0)
            {
                //считаем фитнесс функцию для всех планов
                pop.ForEach(p => p.FitnessValue = Fitness(p));
                //сортруем популяцию по фитнесс функции
                pop.Sort((p1, p2) => p1.FitnessValue.CompareTo(p2.FitnessValue));
                //найден идеальный план?
                if (pop[0].FitnessValue == 0)
                    return pop[0];
                //отбираем 25% лучших планов
                pop.RemoveRange(pop.Count / 4, pop.Count - pop.Count / 4);
                //от каждого создаем трех потомков с мутациями
                var c = pop.Count;
                for (int i = 0; i < c; i++)
                {
                    pop.AddChildOfParent(pop[i]);
                    pop.AddChildOfParent(pop[i]);
                    pop.AddChildOfParent(pop[i]);
                }
            }

            //считаем фитнесс функцию для всех планов
            pop.ForEach(p => p.FitnessValue = Fitness(p));
            //сортруем популяцию по фитнесс функции
            pop.Sort((p1, p2) => p1.FitnessValue.CompareTo(p2.FitnessValue));

            //возвращаем лучший план
            return pop[0];
        }
    }
    class Population : List<Plan>
    {
        public Population(List<SchoolLesson> lessons, int count)
        {
            var maxIterations = count * 2;

            do
            {
                var plan = new Plan();
                if (plan.Init(lessons))
                {
                    Add(plan);
                }
            } while (maxIterations-- > 0 && Count < count);
        }

        public bool AddChildOfParent(Plan parent)
        {
            int maxIterations = 10;

            do
            {
                var plan = new Plan();
                if (plan.Init(parent))
                {
                    Add(plan);
                    return true;
                }
            } while (maxIterations-- > 0);
            return false;
        }
    }
    class Plan
    {
        public static int DaysPerWeek = 5;
        public static int HoursPerDay = 6;
        Random random = new Random();
        public int FitnessValue { get; internal set; }
        public HourPlan[,] HourPlans = new HourPlan[DaysPerWeek, HoursPerDay];
        public bool Init(List<SchoolLesson> lessons)
        {
            for (int i = 0; i < HoursPerDay; i++)
            {
                for (int j = 0; j < DaysPerWeek; j++)
                {
                    HourPlans[j, i] = new HourPlan();
                }
            }  // Initialization

            foreach (var les in lessons)
            {
                if (!AddToAnyDayAndHour(les))
                {
                    return false;
                }
            } // Random Lessons Insertion

            return true;
        }
        public bool AddToAnyDayAndHour(SchoolLesson lesson)
        {
            int maxIterations = 30;

            while (maxIterations-- > 0)
            {
                var day = (byte)random.Next(DaysPerWeek);
                lesson.Day = day;
                if (AddToAnyHour(lesson))
                {
                    return true;
                }
            }

            return false;//не смогли добавить никуда
        }
        bool AddToAnyHour(SchoolLesson lesson)
        {
            for (byte hour = 0; hour < HoursPerDay; hour++)
            {
                lesson.Hour = hour;
                if (AddLesson(lesson))
                {
                    return true;
                }
            }

            return false;//нет свободных часов в этот день
        }

        public bool AddLesson(SchoolLesson les)
        {
            return HourPlans[les.Day, les.Hour].AddLesson(les);
        }
        public void RemoveLesson(SchoolLesson les)
        {
            HourPlans[les.Day, les.Hour].RemoveLesson(les);
        }


        /// <summary>
        /// Создание наследника с мутацией
        /// </summary>
        public bool Init(Plan parent)
        {
            //копируем предка
            for (int i = 0; i < HoursPerDay; i++)
                for (int j = 0; j < DaysPerWeek; j++)
                    HourPlans[j, i] = parent.HourPlans[j, i].Clone();

            //выбираем два случайных дня
            var day1 = (byte)random.Next(DaysPerWeek);
            var day2 = (byte)random.Next(DaysPerWeek);

            //находим пары в эти дни
            var pairs1 = GetLessonsOfDay(day1);
            var pairs2 = GetLessonsOfDay(day2);

            //выбираем случайные пары
            if (pairs1.Count == 0 || pairs2.Count == 0) return false;
            var pair1 = pairs1[random.Next(pairs1.Count)];
            var pair2 = pairs2[random.Next(pairs2.Count)];

            //создаем мутацию - переставляем случайные пары местами
            RemoveLesson(pair1);//удаляем
            RemoveLesson(pair2);//удаляем
            var res1 = AddToAnyHour(new SchoolLesson(pair1.Name, pair1.Subject, pair1.Teacher, pair1.Audience, pair1.Group, pair2.Day));//вставляем в случайное место
            var res2 = AddToAnyHour(new SchoolLesson(pair2.Name, pair2.Subject, pair2.Teacher, pair2.Audience, pair2.Group, pair1.Day));//вставляем в случайное место
            return res1 && res2;
        }

        public List<SchoolLesson> GetLessonsOfDay(byte day)
        {
            List<SchoolLesson> tmp = new List<SchoolLesson>();
            for (byte hour = 0; hour < HoursPerDay; hour++)
            {
                foreach (var p in HourPlans[day, hour].Data.Values)
                {
                    tmp.Add(new SchoolLesson(p[0] as string, p[1] as SubjectElement, p[2] as TeacherElement, p[3] as AudienceElement, p[4] as GroupElement, day, hour));
                }
            }
            return tmp;
        }
        public List<SchoolLesson> GetLessons()
        {
            List<SchoolLesson> tmp = new List<SchoolLesson>();
            for (byte day = 0; day < DaysPerWeek; day++)
            {
                for (byte hour = 0; hour < HoursPerDay; hour++)
                {
                    foreach (var p in HourPlans[day, hour].Data.Values)
                    {
                        tmp.Add(new SchoolLesson(p[0] as string, p[1] as SubjectElement, p[2] as TeacherElement, p[3] as AudienceElement, p[4] as GroupElement, day, hour));
                    }
                }
            }
            return tmp;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (byte day = 0; day < Plan.DaysPerWeek; day++)
            {
                sb.AppendFormat("Day {0}\r\n", day);
                for (byte hour = 0; hour < Plan.HoursPerDay; hour++)
                {
                    sb.AppendFormat("Hour {0}: ", hour);
                    foreach (var p in HourPlans[day, hour].GroupToTeacher)
                        sb.AppendFormat("Gr-Tch: {0}-{1} ", p.Key.Name, p.Value.Name);
                    sb.AppendLine();
                }
            }

            sb.AppendFormat("Fitness: {0}\r\n", FitnessValue);

            return sb.ToString();
        }
        public static void WriteInFile(string path, string text)
        {
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {                
                byte[] array = System.Text.Encoding.Default.GetBytes(text);                
                fstream.Write(array, 0, array.Length);                
            }
        }
    }
    class HourPlan // UNDONE: Change Logic
    {
        public Dictionary<GroupElement, TeacherElement> GroupToTeacher = new Dictionary<GroupElement, TeacherElement>();
        public Dictionary<TeacherElement, GroupElement> TeacherToGroup = new Dictionary<TeacherElement, GroupElement>();
        public Dictionary<GroupElement, ArrayList> Data = new Dictionary<GroupElement, ArrayList>();

        public bool AddLesson(SchoolLesson lesson)
        {
            if (TeacherToGroup.ContainsKey(lesson.Teacher) || GroupToTeacher.ContainsKey(lesson.Group))
            {
                return false;
            }

            GroupToTeacher[lesson.Group] = lesson.Teacher;
            TeacherToGroup[lesson.Teacher] = lesson.Group;

            Data[lesson.Group] = new ArrayList() { lesson.Name, lesson.Subject, lesson.Teacher, lesson.Audience, lesson.Group, lesson.Day, lesson.Hour };

            return true;
        }
        public void RemoveLesson(SchoolLesson lesson)
        {
            GroupToTeacher.Remove(lesson.Group);
            TeacherToGroup.Remove(lesson.Teacher);
            Data.Remove(lesson.Group);
        }
        public HourPlan Clone()
        {
            var res = new HourPlan();
            res.GroupToTeacher = new Dictionary<GroupElement, TeacherElement>(GroupToTeacher);
            res.TeacherToGroup = new Dictionary<TeacherElement, GroupElement>(TeacherToGroup);
            res.Data = new Dictionary<GroupElement, ArrayList>(Data);

            return res;
        }
    }
}
