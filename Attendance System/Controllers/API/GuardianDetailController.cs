using Attendance_System.Attributes;
using Attendance_System.Models;
using Attendance_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Attendance_System.Controllers.API
{
    [AuthorizeRequest]
    public class GuardianDetailController : ApiController
    {
        ProjectEntities db = new ProjectEntities();
        // GET: api/StudentDetail
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/StudentDetail/5
        public IQueryable Get(int id)
        {
            try
            {
                Guardian guardian = db.Guardians.Find(id);
                GuardianDetailModel obj = new GuardianDetailModel();
                obj.guardianID = guardian.guardianID;
                obj.guardianName = guardian.guardianName;   
                obj.mobileNumber = guardian.mobileNumber;
                obj.relationship = guardian.relationship;
                List<GuardianDetailModel> list = new List<GuardianDetailModel>();
                list.Add(obj);
                return list.AsQueryable();
            
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        // POST: api/StudentDetail
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/StudentDetail/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/StudentDetail/5
        public void Delete(int id)
        {
        }
    }
}
