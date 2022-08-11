using System.ComponentModel.DataAnnotations;

namespace Pet_Store.Domains.Models.DataModels
{
    public class UserRoles
    {
        [Key]
        public int RolesId { get; set; }
        [Required]
        public string RolName { get; set; }
    }
}
