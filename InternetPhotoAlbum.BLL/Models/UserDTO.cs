using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Models
{
    public class UserDTO
    {
        public string Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
