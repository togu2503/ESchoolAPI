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
        enum Permissions
        {
            Pupil,
            Teacher
        }
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

        static public User GetUser(string token,ApplicationDBContext context)
        {
           int userId = context.ActiveUser.FirstOrDefault(activeUser => activeUser.Token == token).UserId;
            var user = context.User.FirstOrDefault(u => u.Id == userId);
            if (user != null)
                return user;
            return null;
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
            responseBody.Add("token", token);
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }

        static public byte[] SuccessDeleting()
        {
            var responseBody = new JObject();
            responseBody.Add("status", "Deleted");
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }

        static public byte[] WrongPasswordOrLogin()
        {
            var responseBody = new JObject();
            responseBody.Add("status", "wrong password or login");
            byte[] body = Encoding.UTF8.GetBytes(responseBody.ToString());
            return body;
        }
    }
}
