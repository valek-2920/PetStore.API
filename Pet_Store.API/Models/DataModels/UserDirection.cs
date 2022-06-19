using System.ComponentModel.DataAnnotations;

namespace Project_PetStore.API.Models.DataModels
{
    public class UserDirection
    {
        [Key]
        public int DirectionId { get; set; }
        [Required]
        public string Directions { get; set; }

    }
}
