using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dashboard.Core
{
    public class ChartDto
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Value4 { get; set; }

        public string Name{ get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int[] Data{ get; set; }
        public string[] Categories { get; set; }
    }
}