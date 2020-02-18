using CrudApiClient.Interfaces;
using CrudApiClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUD_MVC_Client.Controllers
{
	public class UserController : Controller
	{
		private IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<IActionResult> Index()
		{
			this.AddToken();
			var response = await _userService.GetAllAsync();

			if (response.IsSuccess)
			{
				return View(response.Data);
			}

			return RedirectToAction("Login", "Account");
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(CreateUserRequest model)
		{
			await _userService.CreateAsync(model);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			this.AddToken();
			var response = await _userService.GetByIdAsync(id);

			if (response.IsSuccess)
			{
				var model = new UpdateUserRequest()
				{
					Id = id,
					FirstName = response.Data.FirstName,
					LastName = response.Data.LastName,
					Email = response.Data.Email
				};

				return View(model);
			}

			if (response.StatusCode == 404)
			{
				return View("NotFound", id);
			}

			return RedirectToAction("Login", "Account");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdateUserRequest model)
		{
			this.AddToken();
			var response = await _userService.UpdateAsync(model.Id, model);

			if (response.IsSuccess)
			{
				return RedirectToAction(nameof(Index));
			}

			if (response.StatusCode == 404)
			{
				return View("NotFound", model.Id);
			}

			return RedirectToAction("Login", "Account");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			this.AddToken();
			var response = await _userService.DeleteAsync(id);

			if (response.IsSuccess)
			{
				return RedirectToAction(nameof(Index));
			}

			if (response.StatusCode == 404)
			{
				return View("NotFound", id);
			}

			return RedirectToAction("Login", "Account");
		}

		private void AddToken() 
		{
			if (HttpContext.Session.TryGetValue("Token", out _))
			{
				var token = HttpContext.Session.GetString("Token");
				_userService.SetAuthToken(token);
			}
		}
	}
}