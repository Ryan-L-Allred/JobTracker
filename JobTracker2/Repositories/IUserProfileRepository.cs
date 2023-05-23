using JobTracker2.Models;

namespace JobTracker2.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetById(int id);
        void Add(UserProfile userProfile);
    }
}
