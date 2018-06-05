﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentManagerAPI.Controllers.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public IList<Assessment> Assessments { get; set; }

    }
}