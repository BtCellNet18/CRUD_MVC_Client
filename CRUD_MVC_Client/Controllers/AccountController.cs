using CrudApiClient.Interfaces;
using CrudApiClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUD_MVC_Client.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAuthService _authService;
		public AccountController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpGet]
		public IActionResult Login()
		{
			this.ClearSession();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginUserRequest model)
		{
			var response = await _authService.LoginAsync(model);

			if (response.IsSuccess)
			{
				HttpContext.Session.SetString("Token", response.Data.Token);
				HttpContext.Session.SetString("Username", model.Username);
				return RedirectToAction("Index", "User");
			}

			return View(model);
		}

		public IActionResult Logout()
		{
			this.ClearSession();
			return RedirectToAction("Index", "Home");
		}

		private void ClearSession()
		{
			HttpContext.Session.Clear();
		}
	}
}