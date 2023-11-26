using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dashboard.Core
{
    public class DataDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Value { get; set; }
    }
}