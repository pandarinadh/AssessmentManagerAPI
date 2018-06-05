using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentManagerAPI.Controllers.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public Score Score { get; set; }
    }

  
}