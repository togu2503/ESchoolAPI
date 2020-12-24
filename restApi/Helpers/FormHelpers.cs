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
    public class FormHelpers
    {
        static public JArray ListToJArray(List<Form> formsList, List<PupilFormJoint> jointList, List<Pupil> pupulList)
        {
            JArray res = new JArray();
            List<List<string>> weekTimeTable = new List<List<string>>();

            for(int i=0;i<formsList.Count();i++)
            {
                JArray formRow = new JArray();
                for(int j=0; j<jointList.Count;j++)
                {
                    if(jointList[j].FormId == formsList[i].Id)
                    {
                        formRow.Add(jointList[j].Pupil.Name + " " + jointList[j].Pupil.Surname + " " + jointList[j].Pupil.Patronymic);
                    }
                }
                JObject formObj = new JObject();
                formObj.Add(formsList[i].FormTitle, formRow);
                res.Add(formObj);
            }
            return res;
        }

        static public byte[] GetAllFormsWithPupils(ApplicationDBContext context)
        {
            var joint = context.PupilFormJoints.ToList();
            var pupils = context.Pupil.ToList();
            var forms = context.Form.ToList();
            JArray responseBody = ListToJArray(forms, joint, pupils);
            var str = responseBody.ToString();
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }

    }
}
