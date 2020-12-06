﻿using System;
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
            string dayString = "";
            switch (day)
            {
                case 0:
                    dayString = "Monday";
                    break;
                case 1:
                    dayString = "Tuesday";
                    break;
                case 2:
                    dayString = "Wednesday";
                    break;
                case 3:
                    dayString = "Thursday";
                    break;
                case 4:
                    dayString = "Friday";
                    break;

            }
            return dayString;
        }
        static public JArray ListToJArray(List<Curriculum> curriculumlist, List<Lesson> lessonsList, List<Form> formsList)
        {
            JArray res = new JArray();
            List<List<string>> weekTimeTable = new List<List<string>>();
            for (int i = 0; i < 5; i++)
                weekTimeTable.Add(new List<string>());
            for (int i = 0; i < formsList.Count(); i++)
            {
                JObject curiculumJSONRow = new JObject();
                for (int j = 0; j < curriculumlist.Count(); j++)
                {
                    if(formsList[i].Id == curriculumlist[j].Form.Id)
                    {
                        //if day == 0 it means that it is monday 1 == Tuesday etc. 
                        weekTimeTable[curriculumlist[j].Day].Add(curriculumlist[j].Lesson.Title); 
                    }
                }
                curiculumJSONRow.Add("id", formsList[i].Id);
                curiculumJSONRow.Add("form", formsList[i].FormTitle);

                for (int day = 0; day<weekTimeTable.Count();day++)
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

        static public byte[] GetCurriculumResponse(List<Curriculum> curriculumList, List<Lesson> lessonsList, List<Form> formsList)
        {
            JArray responseBody = ListToJArray(curriculumList, lessonsList, formsList);
            var str = responseBody.ToString();
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }
    }
}
