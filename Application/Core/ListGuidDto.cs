using Application.Categories.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core
{
    public class ListGuidDto
    {
        public virtual ICollection<Guid> Ids { get; set; }
    }
}