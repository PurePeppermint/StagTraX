using System;
using System.Threading.Tasks;

namespace stagtrax
{
    public interface IFirebaseAuthenticator
    {
        bool IsLoggedIn();
        bool IsEmailVerified();
        Task<bool> LoginWithEmailPassword(string email, string password);
        Task<bool> RegisterWithEmailPassword(string email, string password);
        string GetCurrentUserDisplayName();
        string GetCurrentUserEmail();
        void Logout();
    }
}
