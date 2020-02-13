using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Webstudent.Models;
using Webstudent.Repository;
//using WebStudent.Models;
namespace Webstudent.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Link()
        {
            return View();
        }

        public JsonResult AllStudent()
        {
            DataSet ds;
            dbRepository st = new dbRepository();

            List<studentModel> items = new List<studentModel>();

            ds = st.getAllStudent();
            int numrow = ds.Tables[0].Rows.Count;

            for (int i = 0; i < numrow; i++)
            {
                items.Add(new studentModel
                {
                    ID = int.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString()),
                    Name = ds.Tables[0].Rows[i].ItemArray[1].ToString(),
                    Birth = ds.Tables[0].Rows[i].ItemArray[2].ToString(),
                    Address = ds.Tables[0].Rows[i].ItemArray[3].ToString(),
                    Tel = ds.Tables[0].Rows[i].ItemArray[4].ToString(),
                    Date_star = ds.Tables[0].Rows[i].ItemArray[5].ToString(),
                    Date_End = ds.Tables[0].Rows[i].ItemArray[6].ToString()
                });
            }
            return Json(items,JsonRequestBehavior.AllowGet);
        }

    

          public JsonResult getEditData(int id)
        {
            DataSet ds;
            dbRepository st = new dbRepository();

            List<studentModel> items = new List<studentModel>();

            ds = st.getEditData(id);
            int numrow = ds.Tables[0].Rows.Count;

            for (int i = 0; i < numrow; i++)
            {
                items.Add(new studentModel
                {
                    ID = int.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString()),
                    Name = ds.Tables[0].Rows[i].ItemArray[1].ToString(),
                    Birth = ds.Tables[0].Rows[i].ItemArray[2].ToString(),
                    Address = ds.Tables[0].Rows[i].ItemArray[3].ToString(),
                    Tel = ds.Tables[0].Rows[i].ItemArray[4].ToString(),
                    Date_star = ds.Tables[0].Rows[i].ItemArray[5].ToString(),
                    Date_End = ds.Tables[0].Rows[i].ItemArray[6].ToString()
                });
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditStudent(int Id,string Name,string Birth,string Address,string Tel,string Date_start,string Date_End)
        {
            
            dbRepository st = new dbRepository();

            st.UpdateStudent(Id,Name,Birth,Address,Tel,  Date_start,  Date_End);
            return Json("Edit Sucess", JsonRequestBehavior.AllowGet);
        }

        public JsonResult inserStudent( string Name, string Birth, string Address, string Tel, string Date_start, string Date_End)
        {

            dbRepository st = new dbRepository();

            st.inserStudent( Name, Birth, Address, Tel, Date_start, Date_End);

            //LineNotification แจ้งเตือน
            string token = "ibgH8dEdnvVFFF42HGin0mPti5HXuKSgMx5VTYM5UgV";

            try
            {
                
                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", "เพิ่มข้อมูล \n " + Name + "\n วันเกิด : " + Birth + " \n ที่อยู่ : " + Address + " \n เบอร์โทรศัพท์ : " + Tel);

                var data = Encoding.UTF8.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer " + token);
                using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return Json("Edit Sucess", JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult deleteStudent(int id)
        {
            dbRepository st = new dbRepository();
            st.deleteStudent(id);
            return Json("Delete Sucess", JsonRequestBehavior.AllowGet);
        }
    }
}