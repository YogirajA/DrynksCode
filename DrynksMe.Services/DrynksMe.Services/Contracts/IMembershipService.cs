using DrynksMe.DataAccess.Models;

namespace DrynksMe.Services.Contracts
{
    public interface IMembershipService
    {
        UserProfile GetUserProfile(string userName);
        int InsertUserProfile(UserProfile userProfile);
        User CreateUserWithUserProfile(User user);
        User GetUserByDeviceId(string deviceId);
        void UpdateUserProfile(UserProfile userProfile);
        User GetUserByAnonymousId(string anonymousId);
        User GetUserByUserId(int id);
        User GetUserByTwitterId(string twitterId);
        void UpdateUser(User user);
        void UpdateShare(int userId, bool share);
    }
}