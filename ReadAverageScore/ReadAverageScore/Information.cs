using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAverageScore
{
    public class Information
    {

        //public int ProvinceId { get; set; }
        public string Province { get; set; } = string.Empty;
        public double Mathematics { get; set; } = 0;
        public double Literature { get; set; } = 0;
        public double Physics { get; set; } = 0;
        public double Chemistry { get; set; } = 0;
        public double Biology { get; set; } = 0;
        public double CombinedNaturalSciences { get; set; } = 0;
        public double History { get; set; } = 0;
        public double Geography { get; set; } = 0;
        public double CivicEducation { get; set; } = 0;
        public double CombinedSocialSciences { get; set; } = 0;
        public double English { get; set; } = 0;

        public Information(int provinceId, string provinceName, double mathematics, double literature, double physics, double chemistry, double biology, double combined_natural_sciences, double history, double geography, double civic_education, double combined_social_sciences, double english)
        {
            this.Province = provinceName;
            this.Mathematics = mathematics;
            this.Literature = literature;
            this.Physics = physics;
            this.Chemistry = chemistry;
            this.Biology = biology;
            this.CombinedNaturalSciences = combined_natural_sciences;
            this.History = history;
            this.Geography = geography;
            this.CivicEducation = civic_education;
            this.CombinedSocialSciences = combined_social_sciences;
            this.English = english;
        }
    }
}
