using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AssessmentManagerAPI.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        public string strFile = Common.Common.APIDataFolder + "studentData.js";
        
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("saveStudent")]
        public Student saveStudent(Student student)
        {
            List<Student> _data = getData();

            if (_data == null) _data = new List<Student>();


            //  student.Id = "999";


            if (student != null)
            {
                if (student.Id != 0)
                {
                    var tempQ = _data.FirstOrDefault(r => r.Id == student.Id);
                    tempQ.Name = student.Name;
                    tempQ.Address = student.Address;
                    tempQ.City = student.City;
                    tempQ.State = student.State;
                    tempQ.Zip = student.Zip;
                }
                else
                {
                    if (_data.Count == 0)
                        student.Id = 1;
                    else
                        student.Id = (Convert.ToInt32(_data.Max(r => r.Id)) + 1);

                    _data.Add(student);
                }

            }
            else
                throw new Exception("error");

            string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(strFile, json);

            return student;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetAllStudents")]
        public List<Student> GetAllStudents()
        {
            List<Student> _data = getData();

            return _data;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetStudentById")]
        public Student GetStudentById(int studentId)
        {
            List<Student> _data = getData();

            var student = _data.FirstOrDefault(r => r.Id == studentId);

            return student;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("DeleteStudent")]
        public Student DeleteStudent(int studentId)
        {
            List<Student> _data = getData();

            var student = _data.FirstOrDefault(r => r.Id == studentId);
            var removedStudent = student;
            _data.Remove(student);

            string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(strFile, json);

            return removedStudent;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string Get()
        {

            return "success";
        }

        private List<Student> getData()
        {

            List<Student> _data = new List<Student>();
            using (StreamReader r = new StreamReader(strFile))
            {
                string strJson = r.ReadToEnd();
                if (strJson != "")
                {
                    _data = JsonConvert.DeserializeObject<List<Student>>(strJson);
                }
            }

            return _data;
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State{ get; set; }
        public string Zip { get; set; }

        public IList<Assessment> Assessments { get; set; }

    }
}