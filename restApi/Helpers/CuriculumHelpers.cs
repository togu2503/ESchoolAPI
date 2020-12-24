using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using restApi.Helpers;
using restApi.DAL;
using restApi.Models;

namespace restApi.Helpers
{
    public class CuriculumHelpers
    {
        static public string GetDayByNum(int day)
        {
            return Enum.GetName(typeof(DayOfWeek), day);
        }
        static public JArray ListToJArray(List<Curiculum> curriculumlist, List<Lesson> lessonsList, List<Form> formsList)
        {
            JArray res = new JArray();
            List<List<string>> weekTimeTable = new List<List<string>>();
            for (int i = 0; i < 7; i++)
                weekTimeTable.Add(new List<string>());
            for (int i = 0; i < formsList.Count(); i++)
            {
                JObject curiculumJSONRow = new JObject();
                for (int j = 0; j < curriculumlist.Count(); j++)
                {
                    if(formsList[i].Id == curriculumlist[j].Form.Id)
                    {
                        //if day == 0 it means that it is Sunday 1 == Monday etc. 
                        weekTimeTable[curriculumlist[j].Day].Add(curriculumlist[j].Lesson.Title); 
                    }
                }
                curiculumJSONRow.Add("id", formsList[i].Id);
                curiculumJSONRow.Add("form", formsList[i].FormTitle);

                for (int day = (int)DayOfWeek.Monday; day<(int)DayOfWeek.Saturday;day++)
                {
                    JArray dayJSON = new JArray();
                    for (int lesson = 0; lesson < weekTimeTable[day].Count; lesson ++)
                    {
                        dayJSON.Add(weekTimeTable[day][lesson]);
                    }
                    curiculumJSONRow.Add(GetDayByNum(day), dayJSON);
                    dayJSON = new JArray();
                }
                res.Add(curiculumJSONRow);
            }

            return res; 
        }

        static public byte[] GetTimeTableWithoutHomeworkResponse(ApplicationDBContext context)
        {
            var curiculum = context.Curriculum.ToList();
            var lessons = context.Lesson.ToList();
            var forms = context.Form.ToList();
            JArray responseBody = ListToJArray(curiculum, lessons, forms);
            var str = responseBody.ToString();
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }

        static public int IsHomeworkInList(List<Curiculum> curiculums, int id)
        {
            for(int i=0;i<curiculums.Count();i++)
            {
                if (curiculums[i].Id == id)
                    return i; 
            }
            return -1;
        }

        static public byte[] GetHomeWork(ApplicationDBContext context, int form, DateTime date)
        {
            List<Curiculum> curiculums = context.Curriculum.Where(row => row.FormId == form).ToList();
            int offset = (int)date.DayOfWeek;
            if (offset < 6)
                offset = -offset;
            else
                offset = 7 - offset;
            DateTime start = date.AddDays(offset);
            DateTime finish = start.AddDays(6);
            //List<Homework> homeworks =  context.Homework.ToList();
            List<Homework> homeworks = context.Homework.ToList();
            for(int i=0;i< homeworks.Count();i++)
            {
                if(homeworks[i].Date < start.Date || homeworks[i].Date > finish.Date)
                {
                    homeworks.Remove(homeworks[i]);
                    i--;
                }
            }
            List<List<JObject>> weekTimeTable = new List<List<JObject>>();
            for (int i = 0; i < 7; i++)
                weekTimeTable.Add(new List<JObject>());
            JObject res = new JObject();
            for (int i = 0; i < homeworks.Count(); i++)
            {

                int curiculumId = IsHomeworkInList(curiculums, homeworks[i].LessonId);
                if (curiculumId != -1)
                {
                    //if day == 0 it means that it is Sunday 1 == Monday etc.
                    JObject lesson = new JObject();
                    lesson.Add("lesson", context.Lesson.Find(curiculums[curiculumId].LessonId).Title);
                    lesson.Add("homework", homeworks[i].Tasks);
                    weekTimeTable[curiculums[curiculumId].Day].Add(lesson);
                }
            }
                for (int day = (int)DayOfWeek.Monday; day < (int)DayOfWeek.Saturday; day++)
                {
                    JArray dayJSON = new JArray();
                    for (int lesson = 0; lesson < weekTimeTable[day].Count; lesson++)
                    {
                        dayJSON.Add(weekTimeTable[day][lesson]);
                    }
                    res.Add(GetDayByNum(day), dayJSON);
                    dayJSON = new JArray();
                }

            res.Add("form", context.Form.Find(form).FormTitle);

            byte[] body = Encoding.UTF8.GetBytes(res.ToString());
            return body;
        }
    }
}
