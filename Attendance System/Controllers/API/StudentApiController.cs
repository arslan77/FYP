using Attendance_System.Attributes;
using Attendance_System.Utility;
using Attendance_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Attendance_System.Controllers
{
    [AuthorizeRequest]
    public class StudentApiController : ApiController
    {
        // GET: api/StudentApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/StudentApi/5
       
        public IQueryable Get([FromUri]int id)
        {
            DataManager dm = new DataManager();
            List<StudentInformation> list = dm.getStudentsFromClass(id);
            return list.AsQueryable();
        }

        // POST: api/StudentApi
        public void Post([FromBody]String value)
        {
           
        }

        // PUT: api/StudentApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/StudentApi/5
        public void Delete(int id)
        {
        }
    }
}
