using System.ComponentModel.DataAnnotations;


// Class model for characters

namespace DnD_CMS.Models
{
    public class characterModel
    {
        [Key]
        public int characterID { get; set; }

        [Required]
        public string characterName { get; set; }

        [Required]
        public string characterRace { get; set; }

        [Required]
        public string characterClassOrType { get; set; }

        public string? characterSubClass { get; set; }

        public string? characterDescription { get; set; }

        [Required]
        public string characterSkills { get; set; }

        public string? characterImage { get; set; }

        [Required]
        public int characterSTR { get; set; }

        [Required]
        public int characterDEX { get; set; }

        [Required]
        public int characterCON { get; set; }

        [Required]
        public int characterINT { get; set; }

        [Required]
        public int characterWIS { get; set; }

        [Required]
        public int characterCHA { get; set; }

        public int? characterLevel { get; set; }

        [Required]
        public int characterInitiative { get; set; }

        [Required]
        public int characterSpeed { get; set; }

        [Required]
        public int characterAC { get; set; }

        [Required]
        public int characterHP { get; set; }

        [Required]
        public int characterIsPublic { get; set; }

        [Required]
        public string characterUser { get; set; }
    }
}
