using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentManagerAPI.Controllers.Model
{
    public class StudentAssessment
    {
        public int Id { get; set; }
        public Student Student { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }
    }
} 