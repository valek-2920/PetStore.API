using System.ComponentModel.DataAnnotations;

namespace Project_PetStore.API.Models.DataModels
{
    public class UserRoles
    {
        [Key]
        public int RolesId { get; set; }
        [Required]
        public string RolName { get; set; }
    }
}
