using System;
using Application.Addresses.Core;
using Application.Categories.Core;
using Application.Core;
using Application.Genres.Core;
using Application.Tags.Core;
using Application.ThematicAreas.Core;

namespace Application.Profiles.Core
{
    public class ProfileQryDto
    {

        public Guid Md_ID { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Us_DisplayName { get; set; }
        public DateTime Us_DOB { get; set; }
        public string Us_Gender { get; set; }
        public string Us_language { get; set; }
        public string Us_Country { get; set; }
        public string Us_PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Us_Customer { get; set; }
        public bool Us_Admin { get; set; }
        public bool Us_SuperAdmin { get; set; }
        public DateTime? Us_SubscriptionExpiryDate { get; set; }


        public bool Us_Deleted { get; set; } = false;
        public bool Us_Active { get; set; } = false;
        public DateTime Us_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Us_Creator { get; set; }
        public DateTime? Us_ModifyOn { get; set; }
        public string Us_Modifier { get; set; }

        public virtual ICollection<AddressDto> Addresses { get; set; }
        public virtual ICollection<CategoryDto> Categories { get; set; }
        public virtual ICollection<ItemDto> CategoryItems { get; set; }
        public virtual ICollection<ThematicAreaDto> ThematicAreas { get; set; }
        public virtual ICollection<ItemDto> ThematicAreaItems { get; set; }
        public virtual ICollection<GenreDto> Genres { get; set; }
        public virtual ICollection<ItemDto> GenreItems { get; set; }
        public virtual ICollection<TagDto> Tags { get; set; }
        public virtual ICollection<ItemDto> TagItems { get; set; }

    }
}

