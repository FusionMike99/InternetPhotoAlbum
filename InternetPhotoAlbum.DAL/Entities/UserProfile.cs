using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetPhotoAlbum.DAL.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(20)]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; } = Gender.NotSelected;

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
