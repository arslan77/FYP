using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Attendance_System.Models;
using Attendance_System.ViewModels;

namespace Attendance_System.Utility
{
    public class AccountManager
    {
        //Database reference
        private static ProjectEntities db = new ProjectEntities();

        //Enum
        public enum SigninResult
        {
            WrongPassword, WrongUsername, OK
        };
        public static string getId()
        {
            String id = "";
            if(SessionManager.CheckSession("id"))
            {
                id = SessionManager.ReturnSessionObject("id").ToString();
            }
            else
            {
                id = "-1";
            }
            return id;
        }
        //Return true if user is signin?
        public static bool IsAuthentic()
        {
            bool c = SessionManager.CheckSession("login");
            if (c)
            {
                var a = SessionManager.ReturnSessionObject("login");
                bool status = bool.Parse(a.ToString());

                if (status)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
        //end
        //Take two parameter username and password and check if user exist and password
        //is correct then create the session for login.
        internal static SigninResult Signin(string username, string password)
        {
            Userinfo user = db.Userinfoes.Find(username);
            if (user != null)
            {
                if (Cryptography.matchHash(user.passwordHash, password))
                {

                    HttpContext.Current.Session["login"] = "True";
                    HttpContext.Current.Session["role"] = user.role;
                    if (user.role == "student")
                    {
                        Student std = db.Students.Where(s => s.username == user.username).Single();
                        HttpContext.Current.Session["name"] = std.fullName;
                        HttpContext.Current.Session["id"] = std.rollNo;
                        
                    }
                    else
                    {
                        Teacher tchr = db.Teachers.Where(t => t.username == user.username).Single();
                        HttpContext.Current.Session["name"] = tchr.name;
                        HttpContext.Current.Session["id"] = tchr.teacherID;

                    }
                    return SigninResult.OK;
                }
                else
                {
                    return SigninResult.WrongPassword;
                }
            }
            else
            {
                return SigninResult.WrongUsername;
            }

        }
        //end

        public static bool IsUserInRole(string role)
        {
            bool check = SessionManager.CheckSession("role");
            if (check)
            {
                if (SessionManager.ReturnSessionObject("role").ToString() == role)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        //delete the session for siging out
        internal static void Signout()
        {
            SessionManager.FreeSession("login");
            SessionManager.FreeSession("name");
            SessionManager.FreeSession("role");


        }

        internal static bool validateToken(string token)
        {
           
            if (db.Userinfoes.Any(o => o.token == token))
                return true;
            else
                return false;
        }
        internal static Userinfo getUserFromToken(String token)
        {
            Userinfo obj = db.Userinfoes.Where(o=> o.token==token).First();
            return obj;
        }

        internal static String getUsernameFromToken(String token)
        {
            Userinfo obj = getUserFromToken(token);
            return obj.username;
        }

        //get role from token
        internal static String getRoleFromToken(String token)
        {
            Userinfo obj = getUserFromToken(token);
            return obj.role;
        }
        //get token on successfull login
        internal static UserInformation returnUserInformation(string username, string password)
        {
            Userinfo user = db.Userinfoes.Find(username);
            UserInformation obj = new UserInformation();
            if (user != null)
            {
                if (user.passwordHash==password)
                {
                    obj.status = "OK";
                    obj.token = user.token;
                    obj.role = user.role;
                    if (obj.role == "admin" || obj.role == "teacher")
                        obj.name = db.Teachers.Where(o => o.username == user.username).First().name;
                    else
                        obj.name = db.Students.Where(o => o.username == user.username).First().fullName;
                }
                else
                {
                    obj.status = "wrong pw";
                }
            }
            else
            {
                obj.status = "wrong un";
            }
            return obj;

        }

    }
}