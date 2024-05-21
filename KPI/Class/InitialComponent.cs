using KPI.DataContain;
using KPI.Models;
using System;
using System.Windows.Forms;

namespace KPI.Class
{
    public static class InitialComponent
    {
        public static void DateTimePicker(DateTimePicker dp)
        {
            DateTime dt = DateTime.Now;
            int yy = dt.Year;
            int mm = dt.Month;
            dt = new DateTime(yy, mm, 1);
            dp.Value = dt;

        }

        public static void DateTimePickerPrevious(DateTimePicker dp)
        {
            DateTime dt = DateTime.Now;
            int yy = dt.Year;
            int mm = dt.Month;
            dt = new DateTime(yy, mm, 1);
            dp.Value = dt.AddMonths(-1);

        }


        public static void BoxDateTimeInitial(ComboBox Year, ComboBox Month, ComboBox Day, ComboBox HH, ComboBox MM, ComboBox SS)
        {
            DateTime now = DateTime.Now;
            Year.Items.Clear();
            for (int i = 2020; i < 2030; i++)
            {
                Year.Items.Add(i);
            }
            int year = now.Year;
            Year.SelectedItem = year;
            Month.Items.Clear();
            foreach (string m in KPI.DataContain.Name.MonthName)
            {
                Month.Items.Add(m);
            }
            int month = now.Month;
            Month.SelectedIndex = month - 1;
            int dayInmonth = DateTime.DaysInMonth(year, month);
            Day.Items.Clear();
            for (int d = 0; d < dayInmonth; d++)
            {
                Day.Items.Add(d + 1);
            }
            Day.SelectedItem = now.Day;
            HH.Items.Clear();
            MM.Items.Clear();
            SS.Items.Clear();
            for (int i = 0; i < 24; i++)
            {
                HH.Items.Add(i);
            }
            HH.SelectedItem = now.Hour;
            for (int i = 0; i < 60; i++)
            {
                MM.Items.Add(i);
                SS.Items.Add(i);
            }
            MM.SelectedItem = now.Minute;
            SS.SelectedItem = now.Second;

        }

        public static void BoxDateTime(ComboBox Year, ComboBox Month, ComboBox Day)
        {
            int year = Convert.ToInt32(Year.SelectedItem);
            int month = Convert.ToInt32(Month.SelectedIndex + 1);
            int dayInMonth = DateTime.DaysInMonth(year, month);
            Day.Items.Clear();
           
            for (int d = 0; d < dayInMonth; d++)
            {
                Day.Items.Add(d + 1);
            }
            Day.SelectedItem = 1;
           
        }

        public static DateTime BoxDateTimeConvert(ComboBox Year, ComboBox Month, ComboBox Day, ComboBox HH, ComboBox MM, ComboBox SS)
        {

            int year = Convert.ToInt32(Year.SelectedItem);
            int month = Convert.ToInt32(Month.SelectedIndex+1);
            int day= Convert.ToInt32(Day.SelectedItem);
            int hh = Convert.ToInt32(HH.SelectedIndex);
            int mm = Convert.ToInt32(MM.SelectedIndex);
            int ss = Convert.ToInt32(SS.SelectedIndex);
            return new DateTime(year, month, day, hh, mm, ss);

        }

        public static bool BoxDateTimeForce(TracUserInput input , ComboBox CmbYear, ComboBox CmbMonth, ComboBox CmbDay, ComboBox CmbHH, ComboBox CmbMM, ComboBox CmbSS)
        {
            DateTime dtmin = new DateTime(2020, 1, 1, 0, 0, 0);
            DateTime dtmax = new DateTime(2030, 12, 31, 0, 0, 0);
            if (input.dateTime < dtmin || input.dateTime > dtmax)
                return false;

            CmbYear.SelectedItem = input.year;
            CmbMonth.SelectedIndex = input.month-1;
            CmbDay.SelectedItem = input.day;
            CmbHH.SelectedItem = input.hour;
            CmbMM.SelectedItem = input.minute;
            CmbSS.SelectedItem = input.second;

            return true;

        }

        public static void TracUserInputQRmapping(ListBox listbox) 
        {
            listbox.Items.Clear();
            foreach (string l in LegendPlate.LegendQRmapping())
            {
                listbox.Items.Add(l);
            }
            listbox.SelectedIndex = 0;

        }


    }

}
