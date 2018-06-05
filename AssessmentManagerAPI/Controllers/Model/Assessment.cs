using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentManagerAPI.Controllers.Model
{
    public class Assessment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public IList<Question> Questions { get; set; }
    }
}