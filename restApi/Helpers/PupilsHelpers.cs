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
using restApi.DAL;
using restApi.Models;

namespace restApi.Helpers
{
    public class PupilsHelpers
    {
        static public JArray ListToJArray(List<Pupil> pupilList, ApplicationDBContext context)
        {
            JArray res = new JArray();
            List<List<string>> weekTimeTable = new List<List<string>>();

            for (int i = 0; i < pupilList.Count(); i++)
            {
                JObject pupil = new JObject();

                pupil.Add("id",pupilList[i].Id);
                pupil.Add("name", pupilList[i].Name);
                pupil.Add("surname", pupilList[i].Surname);
                pupil.Add("patronymic", pupilList[i].Patronymic);
                pupil.Add("phone", pupilList[i].Phone);
                pupil.Add("account_id", pupilList[i].AccountId);
                PupilFormJoint joint = context.PupilFormJoints.FirstOrDefault(row => row.PupilId == pupilList[i].Id);
                string form = "undefined";
                if (joint != null)
                    form = context.Form.FirstOrDefault(row => row.Id == joint.FormId).FormTitle;
                pupil.Add("form", form); ;
                res.Add(pupil);
            }
            return res;
        }

        static public byte[] GetAllPupils(ApplicationDBContext context)
        {
            var pupils = context.Pupil.ToList();
            JArray responseBody = ListToJArray(pupils, context);
            var str = responseBody.ToString();
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }

        static public byte[] GetPupil(ApplicationDBContext context , int id)
        {
            var pupils = context.Pupil.Where(row => row.Id == id).ToList();
            JArray responseBody = ListToJArray(pupils, context);
            var str = responseBody.ToString();
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }
    }
}
