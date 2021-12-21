using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Models
{
    public class EditUserProfileModel
    {
        public string Id { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public int GenderId { get; set; }
    }
}
