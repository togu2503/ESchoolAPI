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
    public class UserHelpers
    {
        static public string HashPassword(string login, string password)
        {
         byte[] salt = Encoding.UTF8.GetBytes(login);

         string HashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

        return HashedPassword;
        }

        static public string GenerateUserToken()
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-abcdefghigklmnopqrstuvwxyz";
            var random = new Random();
            var resultToken = new string(
               Enumerable.Repeat(allChar, 32)
               .Select(token => token[random.Next(token.Length)]).ToArray());

            string authToken = resultToken.ToString();
            return authToken;
        }

        static public byte[] DuplicateUserResponse()
        {
            var responseBody = new JObject();
            responseBody.Add("status", "There is such user");
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }

        static public byte[] SuccessfulUserAdding()
        {
            var responseBody = new JObject();
            responseBody.Add("status", "Added");
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }

        static public byte[] SuccessfulLogin(string token)
        {
            var responseBody = new JObject();
            responseBody.Add("auth", token);
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }

        static public byte[] BadLogin()
        {
            var responseBody = new JObject();
            responseBody.Add("auth", "");
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }
    }
}
