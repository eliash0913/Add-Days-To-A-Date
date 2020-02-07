using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Add_Days_To_A_Date
{
    enum DAY_NAME { MON = 1, TUE, WED, THR, FRI, SAT, SUN };
    class Program
    {
        /// <summary>
        /// struct for DateFormat
        /// </summary>
        struct DateFormat
        {
            public int year;
            public int month;
            public int day;
        }        
        
        /// <summary>
        /// AddDays method to take year, month, day, and days, then add days to the date.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        private string AddDays(DateFormat df, int days)
        {
            days += ToDays(df.year, df.month, df.day); //adding julian days converted by ToDay method and add days to be added
            while (GetDays(df.year) < days) //it will iterate until the days are within the year
            {
                days -= GetDays(df.year);
                df.year++;
            }
            return ToDate(df.year, days); //return julian day format to string
        }
   
        /// <summary>
        /// Take year and return the days in the year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private int GetDays(int year)
        {
            return IsLeapYear(year) ? 366 : 365; //if it is a leap year, return 366 days. otherwise, 365 days
        }

        /// <summary>
        /// GetMonthDay memthod to take year and month to and convert to julian days.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private int ToDays(int year, int month, int day)
        {
            int days = 0;
            for (int m = 1; m < month; m++)
            {
                days += GetMonthDay(year, m); //adding whole month until days are smaller than the month.
            }
            days += day;
            return days;
        }

        /// <summary>
        /// Take year and julian date convert to string format.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        private string ToDate(int year, int jDays)
        {
            int month = 1;
            int days = jDays;
            while (GetMonthDay(year, month) < days)
            {
                days -= GetMonthDay(year, month);
                month++;
                if(month==13) //If month is 13, then it starts over from Jan.
                    month=1;
            }
            string sDate = year.ToString() + "-" + month.ToString() + "-" + days.ToString() + "-" + GetDayName(year, jDays);
            return sDate;
        }

        /// <summary>
        /// Take year and month to return the days in the month.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private int GetMonthDay(int year, int month)
        {
            //If month is less or equal to 7, odd month is 31 and the others are 30 except Feb.
            if( month <= 7)
            {
                if (month == 2)
                {
                    if (year % 4 == 0) //Leap year has 29 days. otherwise, 28 days.
                    {
                        return 29;
                    }
                    else
                    {
                        return 28;
                    }
                }
                else if (month % 2 == 0)
                {
                    return 30;
                }
                return 31;
            }
            else //If month is not less or equal to 7, odd month is 30 and the others are 31.
            {
                if (month % 2 == 0)
                {
                    return 31;
                }
                return 30;
            }
        }

        /// <summary>
        /// isLeapYear method to take year and return true if it is leap year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private bool IsLeapYear(int year)
        {
            if (year % 400 == 0)
                return true;
            else if (year % 100 == 0)
                return false;
            else if (year % 4 == 0)
                return true;
            else 
                return false;
        }

        /// <summary>
        /// take year to get shifted day. this is a helper for get day name.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private int GetDayShift(int year)
        {
            int shift = 0;
            for(int i = 1; i < year; i++)
            {
                shift += GetDays(i) % 7;
            }
            return (shift % 7);
        }

        /// <summary>
        /// Take year and julian day, and return day.
        /// </summary>
        /// <param name="jDay"></param>
        /// <returns></returns>
        private string GetDayName(int year, int jDay)
        {
            int day = GetDayShift(year) + jDay;
            switch ((DAY_NAME)(day % 7))
            {
                case DAY_NAME.MON:
                    return "Monday";

                case DAY_NAME.TUE:
                    return "Tuesday";

                case DAY_NAME.WED:
                    return "Wednesday";

                case DAY_NAME.THR:
                    return "Thursday";

                case DAY_NAME.FRI:
                    return "Friday";

                case DAY_NAME.SAT:
                    return "Satursday";

                case DAY_NAME.SUN:
                    return "Sunday";
            }
            return null;
        }
        
        /// <summary>
        /// GetMonth to convert from integer to string month.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetMonth(int i)
        {
            switch (i)
            {
                case 1:
                    return "January";

                case 2:
                    return "February";

                case 3:
                    return "March";

                case 4:
                    return "April";

                case 5:
                    return "May";

                case 6:
                    return "June";

                case 7:
                    return "July";

                case 8:
                    return "August";

                case 9:
                    return "September";

                case 10:
                    return "October";

                case 11:
                    return "November";

                case 12:
                    return "December";
            }
            return "invalid";
        }
        ///
        /// <summary>
        /// Start method to take and invoke adding method to get the result.
        /// </summary>
        private void Start()
        {
            DateFormat df = new DateFormat();
            int days = 0;
            string date = "";
            try
            {
                Console.WriteLine("Please Enter Date (YYYY-MM-DD):");
                date = Console.ReadLine();
                string[] dateArray = date.Split('-');
                df.year = Int32.Parse(dateArray[0]);
                df.month = Int32.Parse(dateArray[1]);
                if (df.month > 12 || df.month < 0)
                {
                    throw new DateFormatException(df.month + " is not valid month. Please enter 1 - 12 for month.");
                }
                df.day = Int32.Parse(dateArray[2]);
                if (df.day > GetMonthDay(df.year, df.month))
                {
                    throw new DateFormatException(GetMonth(df.month) + " has only " + GetMonthDay(df.year, df.month) + " days.");
                }
                Console.WriteLine("Please Enter Days to Add");
                days = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Date {0} days after {1} : {2}", days, date, AddDays(df, days));
            }
            catch (DateFormatException dfe)
            {
                Console.WriteLine(dfe.Message);
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("Please check date foramt. It must be yyyy-mm-dd");
            }
            catch(OverflowException)
            {
                Console.WriteLine("The number you put is too big to take.");
            }
            catch(FormatException)
            {
                Console.WriteLine("Please enter numeric values");
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Start();
        }
    }
}
