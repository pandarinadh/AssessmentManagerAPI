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
    [RoutePrefix("api/assessment")]
    public class AssessmentController : ApiController
    {
        public string strFile = Common.Common.APIDataFolder + "assessmentData.js";

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("saveAssessment")]
        public Assessment saveAssessment(Assessment assessment)
        {
            List<Assessment> _data = getData();

            if (_data == null) _data = new List<Assessment>();


            //  assessment.Id = "999";


            if (assessment != null)
            {
                if (assessment.Id != 0)
                {
                    var tempQ = _data.FirstOrDefault(r => r.Id == assessment.Id);
                    tempQ.Text = assessment.Text;
                    tempQ.Description = assessment.Description;
                    tempQ.Questions = assessment.Questions;
                }
                else
                {
                    if (_data.Count == 0)
                        assessment.Id = 1;
                    else
                        assessment.Id = (Convert.ToInt32(_data.Max(r => r.Id)) + 1);

                    _data.Add(assessment);
                }

            }
            else
                throw new Exception("error");

            string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(strFile, json);

            return assessment;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetAllAssessments")]
        public List<Assessment> GetAllAssessments()
        {
            List<Assessment> _data = getData();

            return _data;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetAssessmentById")]
        public Assessment GetAssessmentById(int assessmentId)
        {
            List<Assessment> _data = getData();

            var assessment = _data.FirstOrDefault(r => r.Id == assessmentId);

            return assessment;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("DeleteAssessment")]
        public Assessment DeleteAssessment(int assessmentId)
        {
            List<Assessment> _data = getData();

            var assessment = _data.FirstOrDefault(r => r.Id == assessmentId);
            var removedAssessment = assessment;
            _data.Remove(assessment);

            string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(strFile, json);

            return removedAssessment;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string Get()
        {

            return "success";
        }

        private List<Assessment> getData()
        {

            List<Assessment> _data = new List<Assessment>();
            using (StreamReader r = new StreamReader(strFile))
            {
                string strJson = r.ReadToEnd();
                if (strJson != "")
                {
                    _data = JsonConvert.DeserializeObject<List<Assessment>>(strJson);
                }
            }

            return _data;
        }
    }

    public class Assessment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public IList<Question> Questions { get; set; }
    }
}