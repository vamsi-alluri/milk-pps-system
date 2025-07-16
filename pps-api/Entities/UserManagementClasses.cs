using pps_api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pps_api.Entities
{
    public class UserIdentity
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MaxLength(150)]
        public string PasswordHash { get; set; }

        public bool Active { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public bool IsPasswordStale { get; set; } = false;

        public ICollection<UserDepartmentMapping> UserDepartmentMaps { get; set; } = new List<UserDepartmentMapping>();
    }

    public class Role
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public ICollection<UserIdentity>? Users { get; set; }
    }

    public class Department
    {
        // NOT use [Key] attribute here if using a composite key with UserDepartment.
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public ICollection<UserDepartmentMapping> UserDepartmentMaps { get; set; } = new List<UserDepartmentMapping>();
    }

    public class UserDepartmentMapping
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(UserIdentity))]
        public int UserIdentityID { get; set; }
        public UserIdentity UserIdentity { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public int RoleLevel { get; set; }
    }


}
