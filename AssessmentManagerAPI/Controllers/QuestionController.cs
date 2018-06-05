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
using AssessmentManagerAPI.Controllers.Model;

namespace AssessmentManagerAPI.Controllers
{
    [RoutePrefix("api/Question")]
    public class QuestionController : ApiController
    {

        public string strFile = Common.Common.APIDataFolder + "questionData.js";
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("UploadQuestions")]
        public string UploadQuestions()
        {
            List<Question> _data = getData();

            if (_data == null) _data = new List<Question>();

            _data.Add(new Question()
            {
                Id = 1,
                Text = "test",
                Description = "A Message"
            });

            string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(strFile, json);

            return "success questions";
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("saveQuestion")]
        public Question saveQuestion(Question question)
        {
            List<Question> _data = getData();

            if (_data == null) _data = new List<Question>();

            
            //  question.Id = "999";


            if (question != null)
            {
                if (question.Id != 0)
                {
                    var tempQ = _data.FirstOrDefault(r => r.Id == question.Id);
                    tempQ.Text = question.Text;
                    tempQ.Description = question.Description;
                }
                else
                {
                    if (_data.Count == 0)
                        question.Id = 1;
                    else
                        question.Id = (Convert.ToInt32(_data.Max(r => r.Id)) + 1);

                    _data.Add(question);
                }

            }
            else
                throw new Exception("error");

            string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(strFile, json);

            return question;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetAllQuestions")]
        public List<Question> GetAllQuestions()
        {
            List<Question> _data = getData();

            return _data;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetQuestionById")]
        public Question GetQuestionById(int questionId)
        {
            List<Question> _data = getData();

            var question = _data.FirstOrDefault(r => r.Id == questionId);
            
            return question;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("DeleteQuestion")]
        public Question DeleteQuestion(int questionId)
        {
            List<Question> _data = getData();

            var question = _data.FirstOrDefault(r => r.Id == questionId);
            var removedQuestion = question;
            _data.Remove(question);

            string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(strFile, json);

            return removedQuestion;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string Get()
        {
         
            return "success";
        }

        private List<Question> getData()
        {
           
            List<Question> _data = new List<Question>();
            using (StreamReader r = new StreamReader(strFile))
            {
                string strJson = r.ReadToEnd();
                if (strJson != "")
                {
                    _data = JsonConvert.DeserializeObject<List<Question>>(strJson);
                }
            }

            return _data;
        }
    }

   
}
