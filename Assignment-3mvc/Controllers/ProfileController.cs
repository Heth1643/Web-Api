using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Assignment_3Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment_3mvc.Controllers
{
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        String Baseurl = "https://localhost:7270";


        public async Task<IActionResult> Profile()
        {
            if (HttpContext.Session.GetString("loginemail") != null)
            {

                var Email = HttpContext.Session.GetString("loginemail");
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    var res = await client.GetAsync("/api/RegisterApi/" + Email);

                    if (res.IsSuccessStatusCode)
                    {
                        var x = await res.Content.ReadFromJsonAsync<Register>();

                        ViewBag.Fname = x.Fname;
                        ViewBag.Lname = x.Lname;
                        ViewBag.Cno = x.Cno;
                        ViewBag.Dob = x.Dob;
                        ViewBag.Email = x.Email;
                        ViewBag.Password = x.Password;
                        ViewBag.Dateadded = x.Dateadded;
                        ViewBag.Lastupdate = x.Lastupdate;
                        ViewBag.Recid = x.Recid;

                        return View();



                    }


                }
            }
            return RedirectToAction("Login", "Home");



        }

        [HttpPost]
        public async Task<IActionResult> update(Register reg)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var res = await client.PutAsJsonAsync("/api/RegisterApi", reg);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Home");



                }

            }

            return View();
        }

    }
}