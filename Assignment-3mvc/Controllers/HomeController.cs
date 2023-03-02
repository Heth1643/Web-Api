using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment_3mvc.Models;
using Assignment_3Model;

namespace Assignment_3mvc.Controllers;

public class HomeController : Controller
{

    string Baseurl = "https://localhost:7270";
    public async Task<IActionResult> Login()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Register reg)
    {
        String Email = reg.Email;
        String Password = reg.Password;

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(Baseurl);

            var res = await client.PostAsJsonAsync<Register>("/api/RegisterApi", reg);

            if (res.IsSuccessStatusCode)
            {

                var res1 = await res.Content.ReadFromJsonAsync<Register>();
                HttpContext.Session.SetString("loginemail", reg.Email);
                return RedirectToAction("Profile", "Profile");

            }

        }

        return View();
    }


    public async Task<IActionResult> Register1(Register reg)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(Baseurl);

            var res = await client.PostAsJsonAsync<Register>("/api/RegisterApi/PostData/", reg);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
        }

        return View();
    }


    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");

    }

}
