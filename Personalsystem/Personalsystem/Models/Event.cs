﻿using Personalsystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }

        public int cId { get; set; }
        [ForeignKey("cId")]
        public virtual Company company { get; set; }

        PersonalSystemContext db = new PersonalSystemContext();

        public void CreateCompanyEvent(Company company, string description, DateTime time)
        {
            var target = db.company.Find(company.Id);


            var companyEvent = new Event { Content = description, cId = target.Id, Time = time };
            db.companyEvent.Add(companyEvent);
            db.SaveChanges();

        }

        public void RemoveCompanyEvent(Company company, Event e)
        {
            if (e.cId == company.Id)
            {
                db.companyEvent.Remove(e);
            }
        }

        public string WeekdayToString(DateTime time)
        {
            var result = time.ToString("ddd", new CultureInfo("en-US"));
            return result;
        }

        public int DayFinder(DateTime query)
        {
            switch (WeekdayToString(query))
            {
                case "Mon":
                    return 1;
                case "Tue":
                    return 2;
                case "Wed":
                    return 3;
                case "Thu":
                    return 4;
                case "Fri":
                    return 5;
                case "Sat":
                    return 6;
                case "Sun":
                    return 7;
                default:
                    return 0;
            }
        }

            // Need a calendar.  Culture’s irrelevent since we specify start day of week
            private static Calendar cal = CultureInfo.InvariantCulture.Calendar;

            // This presumes that weeks start with Monday.
            // Week 1 is the 1st week of the year with a Thursday in it.
            public int GetIso8601WeekOfYear(DateTime time)
            {
                // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it’ll 
                // be the same week# as whatever Thursday, Friday or Saturday are,
                // and we always get those right
                DayOfWeek day = cal.GetDayOfWeek(time);
                if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
                {
                    time = time.AddDays(3);
                }

                // Return the week of our adjusted day
                return cal.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            }

            public List<Event> ListCompanyEvents(Company company)
            {
                List<Event> eventList = db.companyEvent.Where(e => e.cId == company.Id).ToList();
                return eventList;
            }
        }
    }
