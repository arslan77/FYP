using Attendance_System.Attributes;
using Attendance_System.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Attendance_System.Controllers.API
{
    [AuthorizeRequest]
    public class ImageController : ApiController
    {
        // GET: api/Image
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        ProjectEntities db = new ProjectEntities();
        // GET: api/Image/5
        public HttpResponseMessage Get(string id)
        {
            string imagePath = db.Userinfoes.Where(o => o.Students.Any(s => s.rollNo == id)).First().imagePath;
            
            if (imagePath == null)
            {
                HttpResponseMessage excep = new HttpResponseMessage(HttpStatusCode.NotFound);
                return excep;

            }
            string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string path = Path.Combine(root, imagePath);
            path = path.Replace("~", "");
            path = path.Replace("/", "\\");
            Image img = Image.FromFile(path);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(ms.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return result;
        }

        // POST: api/Image
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Image/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Image/5
        public void Delete(int id)
        {
        }
    }
}
