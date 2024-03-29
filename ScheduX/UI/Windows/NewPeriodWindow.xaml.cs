﻿using System;
using System.Windows;
using System.Windows.Controls;
using ScheduX.App_Logic;

namespace ScheduX.UI.PeriodOfStudy
{
    /// <summary>
    /// Interaction logic for NewPeriodWindow.xaml
    /// </summary>
    public partial class NewPeriodWindow : Window
    {
        public NewPeriodWindow()
        {
            InitializeComponent();
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongValue())
            {
                var OwnerWindowInstance = (PeriodOfStudyWindow)this.Owner;
                var period = new SchoolPeriod(NameTextBox.Text, DatePicker_1.SelectedDate.Value, DatePicker_2.SelectedDate.Value);
                OwnerWindowInstance.StudyPeriodDict.dictionaryList.Add(period);
                OwnerWindowInstance.PeriodsList.Items.Add(period);
                
                ElementsModification.ResetControlText<TextBox>(this);
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongValue())
            {
                var ownerWindowInstance = (PeriodOfStudyWindow)Owner;
                var period = (SchoolPeriod)ownerWindowInstance.StudyPeriodDict.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.PeriodsList.SelectedItem.GetHashCode());

                period.Name = NameTextBox.Text;
                period.Start = DatePicker_1.SelectedDate.Value;
                period.End = DatePicker_2.SelectedDate.Value;

                int index = ownerWindowInstance.PeriodsList.Items.IndexOf(ownerWindowInstance.PeriodsList.SelectedItem);
                ownerWindowInstance.PeriodsList.Items.Remove(ownerWindowInstance.PeriodsList.SelectedItem);
                ownerWindowInstance.PeriodsList.Items.Insert(index, period);

                ElementsModification.ResetControlText<TextBox>(this);
            }
        }
        private bool IsWrongValue()
        {
            bool flag = false;
            if (NameTextBox.Text == "")
            {
                NameTextBox.BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.Blood);
                flag = true;
            }
            if (DatePicker_1.Text == "")
            {
                DatePicker_1.BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.Blood);
                flag = true;
            }
            if (DatePicker_2.Text == "")
            {
                DatePicker_2.BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.Blood);
                flag = true;
            }          
            if ((DatePicker_2.SelectedDate ?? new DateTime()) < (DatePicker_1.SelectedDate ?? new DateTime()))
            {
                InfoLabel.Content = "* Start date is more than end date";
                flag = true;
            }
            return flag;
        }       
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ElementsModification.ResetControlText<TextBox>(this);
        }       
        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.DarkGrey);
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as DatePicker).BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.DarkGrey);
        }
    }
}
