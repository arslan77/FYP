using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance_System.Utility
{
    //class to manage Session in our Application
    public static class SessionManager
    {
        //Create a new session by taking two parameter.
        //first is the key and the 2nd is value of session
        public static void RegisterSession(string key, object obj)
        {
            System.Web.HttpContext.Current.Session[key] = obj;
        }

        //Delete the session by taking its key as parameter
        public static void FreeSession(string key)
        {
            System.Web.HttpContext.Current.Session[key] = null;
        }


        //Check the session rather it exist or not by taking key as parameter
        public static bool CheckSession(string key)
        {
            if (System.Web.HttpContext.Current.Session[key] != null)
                return true;
            else
                return false;
        }


        //return the value of session input is here is a key of session
        public static object ReturnSessionObject(string key)
        {
            if (CheckSession(key))
                return System.Web.HttpContext.Current.Session[key];
            else
                return null;
        }
    }
}