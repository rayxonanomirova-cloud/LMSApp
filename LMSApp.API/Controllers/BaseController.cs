using LMSApp.Application.Helpers;
using LMSApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSApp.API.Controllers
{
    [ApiController,Authorize, Route("api/LMSApp/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        protected UserProfile userProfile => HelperUserProfile.GetUserProfile(User);
    }
}
