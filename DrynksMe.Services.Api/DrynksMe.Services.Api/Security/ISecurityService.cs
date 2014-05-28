using System.Net.Http.Headers;
using DrynksMe.Services.Api.Models;

namespace DrynksMe.Services.Api.Security
{
    public interface ISecurityService
    {
        bool Authenticated(DrynksApiHeader header);
        bool Authorized(DrynksApiHeader header);
        void SetCurrentPrincipal(DrynksApiHeader header);
    }
}