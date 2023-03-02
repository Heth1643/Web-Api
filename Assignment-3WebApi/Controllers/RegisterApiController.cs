using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_3Model;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterApiController : ControllerBase
    {
        private readonly Traineedb17Context _db;

        public RegisterApiController(Traineedb17Context db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {


            return Ok(_db.Registers.ToList());
        }

        [HttpGet("{Email}")]
        public async Task<IActionResult> Get(string Email)

        {
            if (Email != null)
            {
                string eml = EnryptString(Email);

                var x = _db.Registers.Where(x => x.Email == eml).FirstOrDefault();
                if (x != null)
                {
                    Register reg = new Register();
                    reg.Fname = DecryptString(x.Fname);
                    reg.Lname = DecryptString(x.Lname);
                    reg.Email = DecryptString(x.Email);
                    reg.Cno = DecryptString(x.Cno);
                    reg.Password = DecryptString(x.Password);
                    reg.Dob = x.Dob;
                    reg.Dateadded = x.Dateadded;
                    reg.Lastupdate = x.Lastupdate;
                    reg.Recid = x.Recid;

                    return Ok(reg);


                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return NoContent();
            }



        }


        [HttpPost]
        public async Task<IActionResult> Login(Register reg)
        {
            if (reg.Email != null && reg.Password != null)
            {
                string Email = EnryptString(reg.Email);
                string Password = EnryptString(reg.Password);

                var x = _db.Registers.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();

                if (x != null)
                {

                    Register r = new Register();
                    r.Email = DecryptString(x.Email);
                    return Ok(r);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NoContent();
            }


        }

        [Route("PostData")]
        [HttpPost]

        public async Task<IActionResult> Post([FromBody] Register reg)
        {
            Register r = new Register();
            r.Fname = EnryptString(reg.Fname);
            r.Lname = EnryptString(reg.Lname);
            r.Cno = EnryptString(reg.Cno);
            r.Email = EnryptString(reg.Email);
            r.Dob = reg.Dob;
            r.Dateadded = DateTime.Now;
            r.Password = EnryptString(reg.Password);
            _db.Registers.Add(r);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] Register reg)
        {
            
            Register r = new Register();
            r.Fname = EnryptString(reg.Fname);
            r.Lname = EnryptString(reg.Lname);
            r.Cno = EnryptString(reg.Cno);
            r.Email = EnryptString(reg.Email);
            r.Dob = reg.Dob;
            r.Dateadded = reg.Dateadded;
            r.Lastupdate = DateTime.Now;
            r.Password = EnryptString(reg.Password);
            r.Recid = reg.Recid;
            _db.Registers.Update(r);
            _db.SaveChanges();
            return Ok();
        }

        private string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "" + fe;
            }
            return decrypted;
        }
        private string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}