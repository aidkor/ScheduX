using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ScheduX.Resourses.AppLogic;
using ScheduX.Resourses.UILogic;
using ScheduX.UI.Pages;

namespace ScheduX.UI.Lessons
{
    /// <summary>
    /// Interaction logic for NewLessonWindow.xaml
    /// </summary>
    public partial class NewLessonWindow : Window
    {
        public NewLessonWindow()
        {
            InitializeComponent();
        }
        private void ResetControls()
        {
            this.Visibility = Visibility.Hidden;
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                item.Text = null;
            }
        }
        private bool IsWrongTextBoxValue()
        {
            bool flag = false;
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                if (item.Text == "")
                {
                    item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                    flag = true;
                }
                if (!uint.TryParse(item.Text, out uint _) & item.Name != "NameTextBox")
                {
                    item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                    flag = true;
                }
            }
            return flag;
        }
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#595959");
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ResetControls();
        }
        /*   public void Add_Click(object sender, RoutedEventArgs e)
           {
               if (!IsWrongTextBoxValue())
               {
                   var OwnerWindowInstance = (LessonsWindow)this.Owner;
                   Lesson lesson = new SchoolLesson();
                   OwnerWindowInstance.SchoolLessonDictionary.dictionaryList.Add(lesson);
                   OwnerWindowInstance.LessonsList.Items.Add(lesson);
                   ResetControls();
               }
           }*/
        /*  public void Done_Click(object sender, RoutedEventArgs e)
          {
              if (!IsWrongTextBoxValue())
              {
                  var ownerWindowInstance = (LessonsWindow)this.Owner;
                  var lesson = (SchoolLesson)ownerWindowInstance.SchoolLessonDictionary.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.LessonsList.SelectedItem.GetHashCode());

                  lesson.Name = NameTextBox.Text;
                  lesson.LessonsPerWeek = int.Parse(LessonsPerWeekTextBox.Text);
                  lesson.LessonsInGeneral = int.Parse(LessonsInGeneralTextBox.Text);
                  lesson.MaxLessonsPerDay = int.Parse(MaxLessonsPerDayTextBox.Text);


                  int index = ownerWindowInstance.LessonsList.Items.IndexOf(ownerWindowInstance.LessonsList.SelectedItem);
                  ownerWindowInstance.LessonsList.Items.Remove(ownerWindowInstance.LessonsList.SelectedItem);
                  ownerWindowInstance.LessonsList.Items.Insert(index, lesson);

                  ResetControls();
              }
          }*/

        private void SubjectTextBox_DropDownOpened(object sender, EventArgs e)
        {
            var ownerEditorWindowInstance = (EditorWindow)((LessonsWindow)this.Owner).Owner;
            var subjects = ownerEditorWindowInstance.HomePage.SubjectsWindowInstance?.SubjectsList.Items;
            foreach (var item in subjects)
            {
                SubjectTextBox.Items.Add(((SchoolSubject)item).Name);               
            }

        }
    }
}
