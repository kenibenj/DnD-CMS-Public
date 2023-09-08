using System.ComponentModel.DataAnnotations;


// Class model for Users
namespace DnD_CMS.Models
{
    public class userModel
    {
        [Key]
        public int userID { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]

        public string userPassword { get; set; }

    }
}
