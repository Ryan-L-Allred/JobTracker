using JobTracker2.Models;

namespace JobTracker2.Repositories
{
    public interface IRoleRepository
    {
        List<Role> GetAllRoles();
        Role GetRoleById(int id);
        void AddRole(Role role);
        void UpdateRole(Role role);
    }
}
