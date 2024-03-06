using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCSVAndCoonectDB
{
    class Information
    {
        //public string Province { get; set; } = string.Empty;
        //public double Mathematics { get; set; } = 0;
        //public double Literature { get; set; } = 0;
        //public double Physics { get; set; } = 0;
        //public double Chemistry { get; set; } = 0;
        //public double Biology { get; set; } = 0;
        //public double CombinedNaturalSciences { get; set; } = 0;
        //public double History { get; set; } = 0;
        //public double Geography { get; set; } = 0;
        //public double CivicEducation { get; set; } = 0;
        //public double CombinedSocialSciences { get; set; } = 0;
        //public double English { get; set; } = 0;

        //public Information(string provinceName, double mathematics, double literature, double physics, double chemistry, double biology, double combined_natural_sciences, double history, double geography, double civic_education, double combined_social_sciences, double english)
        //{
        //    this.Province = provinceName;
        //    this.Mathematics = mathematics;
        //    this.Literature = literature;
        //    this.Physics = physics;
        //    this.Chemistry = chemistry;
        //    this.Biology = biology;
        //    this.CombinedNaturalSciences = combined_natural_sciences;
        //    this.History = history;
        //    this.Geography = geography;
        //    this.CivicEducation = civic_education;
        //    this.CombinedSocialSciences = combined_social_sciences;
        //    this.English = english;
        //}

        public string StudentID { get; set; }
        public double Mathematics { get; set; }
        public double Literature { get; set; }
        public double Physics { get; set; }
        public double Chemistry { get; set; }
        public double Biology { get; set; }
        public double English { get; set; }
        public string Year { get; set; }
        public double History { get; set; }
        public double Geography { get; set; }
        public double CivicEducation { get; set; }
        public string ProvinceCode { get; set; }

        public Information(string studentID, double mathematics, double literature, double physics, double biology, double english, string year, double chemistry, double history, double geography, double civicEducation, string provinceCode)
        {
            StudentID = studentID;
            Mathematics = mathematics;
            Literature = literature;
            Physics = physics;
            Chemistry = chemistry;
            Biology = biology;
            English = english;
            Year = year;
            History = history;
            Geography = geography;
            CivicEducation = civicEducation;
            ProvinceCode = provinceCode;
        }

    }
}
