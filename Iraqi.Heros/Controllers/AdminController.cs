using Iraqi.Heros.DAL;
using Iraqi.Heros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Iraqi.Heros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class AdminController : ControllerBase
    {
        private readonly MainDbContext _context;

        public AdminController(MainDbContext context)
        {
            _context = context;
        }

        // GET: api/Admin
        [Authorize(Roles = ("admin"))]
        [HttpGet("GetUsers")]
      
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Admin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(Guid id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Admin/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        
        public async Task<IActionResult> PutUsers(Guid id, Users users)
        {
            users.Role = "admin";
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Admin
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("addUser")]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            users.Role = "admin";
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/Admin/5
     

        private bool UsersExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }



        [HttpGet("All/{start}/{end}")]
      
        public async Task<IActionResult> All(int start, int end)
        {
            var result = await _context.Persons.Include(x => x.Images).Where(x => x.Status == 0).Select(x => new
            {
                x.Id,
                x.Name,
                x.PoK,
                x.Gov,
                x.DoB,
                x.Type,
                x.Story,
                x.DoK,
                ImageName = x.Images.Select(z => new string($"{z.Name}{z.Key}")).ToList()
            }

                    ).AsNoTracking().OrderBy(x => x.Id).Skip(start).Take(end).ToListAsync();
            if (result.Count == 0)
                return BadRequest();


            return Ok(result);
        }


        

        [HttpPut("{id}/{status}")]
        public async Task<IActionResult> UpdateStatus(Guid id, int status)
        {
            var result = await _context.Persons.FirstAsync(x => x.Id == id);
            if (result == null)
                return BadRequest(new {
                Error="Not Found Person"
                });
            result.Status = status;
            _context.Persons.Update(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }



       [HttpGet("Comment/{start}/{end}")]
       public async Task<IActionResult> GetAllComment(int start,int end) {
            var result = await _context.Comments.Where(x => x.Status == false).Select(x => new
            {
                x.Id,
                x.Comment,
                x.CommentDate
            }).OrderBy(x => x.CommentDate).Skip(start).Take(end).AsNoTracking().ToListAsync();
            if (result.Count == 0)
                return BadRequest();
            return Ok(result);
        }

        [HttpPut("Comment/{id}/{status}")]
        public async Task<IActionResult> UpdateComment(Guid id, bool status)
        {
            var result = await _context.Comments.FirstAsync(x => x.Id == id);


            if (result == null)
                return BadRequest(new
                {
                    Error = "Not Found Person"
                });

            if (status)
            {
                result.Status = status;
                _context.Comments.Update(result);

            }
            else
            {
                _context.Comments.Remove(result);
            }

         await _context.SaveChangesAsync();
            return Ok(result);
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginForm login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.UserName==login.Username && x.Password==login.Password);
            if (user == null)
                return BadRequest(new
                {
                    Error = "UserName or Password Incorrect"
                });
            var claims = new[]
            {
                //  new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim("Username", user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Role", user.Role),
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(3),
                notBefore: DateTime.UtcNow,
                audience: "Audience",
                issuer: "Issuer",
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("Hlkjds0-324mf34pojf-34r34fwlknef0943")),
                    SecurityAlgorithms.HmacSha256)
            );
            return Ok(new 
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }


    }
}
