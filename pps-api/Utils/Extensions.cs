using pps_api.Entities;
using pps_api.Models;

namespace pps_api.Utils
{
    public static class Extensions
    {
        public static List<AccessScope> ToAccessScopes(this UserIdentity user)
        {
            return user.UserDepartmentMaps?
                .Select(uas => new AccessScope
                {
                    DepartmentName = uas?.Department.Name,
                    RoleLevel = uas?.RoleLevel ?? 0
                }).ToList() ?? new List<AccessScope>();
        }

        // STOPPED HERE: we need all departments for the department id to map correctly.
        //public static UserIdentity ToUserIdentity(this List<AccessScope> accessScopes)
        //{
        //    var user = new UserIdentity
        //    {
        //        UserDepartments = accessScopes
        //            .Select(scope =>
        //            {
        //                var department = allDepartments.FirstOrDefault(d => d.Name == scope.Department);
        //                if (department == null) throw new Exception($"Department '{scope.Department}' not found.");

        //                return new UserDepartment
        //                {
        //                    DepartmentID = department.,
        //                    Department = department,
        //                    RoleLevel = scope.RoleLevel
        //                };
        //            })
        //            .ToList()
        //    };

        //    return user;
        //}
    }
}
