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
using AssessmentManagerAPI.Common;
using AssessmentManagerAPI.Controllers.Model;

namespace AssessmentManagerAPI.Controllers
{
    [RoutePrefix("api/AssesssmentCenter")]
    public class AssessmentCenterController : ApiController
    {
        public string strFile = Common.Common.APIDataFolder +  "studentAssessmentData.js";

       

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetAllStudentAssessments")]
        public List<StudentAssessment> GetAllStudentAssessments()
        {
            List<StudentAssessment> _data = getData();

            return _data;
        }

        private List<StudentAssessment> getData()
        {

            List<StudentAssessment> _data = new List<StudentAssessment>();
            using (StreamReader r = new StreamReader(strFile))
            {
                string strJson = r.ReadToEnd();
                if (strJson != "")
                {
                    _data = JsonConvert.DeserializeObject<List<StudentAssessment>>(strJson);
                }
            }

            return _data;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("save")]
        public StudentAssessment save(StudentAssessment studentAssessment)
        {
            List<StudentAssessment> _data = getData();

            if (_data == null) _data = new List<StudentAssessment>();


            //  question.Id = "999";


            if (studentAssessment != null)
            {
                if (studentAssessment.Id != 0)
                {
                    var tempQ = _data.FirstOrDefault(r => r.Id == studentAssessment.Id);
                    tempQ.Student.Assessments = studentAssessment.Student.Assessments;
                }
                else
                {
                    if (_data.Count == 0)
                        studentAssessment.Id = 1;
                    else
                        studentAssessment.Id = (Convert.ToInt32(_data.Max(r => r.Id)) + 1);

                    _data.Add(studentAssessment);
                }

            }
            else
                throw new Exception("error");

            string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(strFile, json);

            return studentAssessment;
        }
    }

    
}
