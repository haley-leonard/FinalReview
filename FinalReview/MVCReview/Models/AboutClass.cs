﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCReview.Models
{
    public class AboutClass
    {

        public string BowlRecord { get; set; }
        public string NationalTitles { get; set; }
        public string HeadCoach { get; set; }

        public AboutClass()
        {
            HeadCoach = "Lincoln Riley";
            BowlRecord = "29 - 22 - 1(.567)";
            NationalTitles = "7(1950, 1955, 1956, 1974, 1975, 1985, 2000)";
        }

    }
}