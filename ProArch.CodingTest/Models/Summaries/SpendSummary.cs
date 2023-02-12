using System.Collections.Generic;

namespace ProArch.CodingTest.Models.Summaries
{
    public class SpendSummary
    {
        public string Name { get; set; }

        public List<SpendDetail> Years { get; set; }
    }
}