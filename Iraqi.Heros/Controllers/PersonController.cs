using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Iraqi.Heros.DAL;
using Iraqi.Heros.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iraqi.Heros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly MainDbContext _dbContext;

        public PersonController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm]PersonForm personForm)
        {
            var person = new Person()
            {
                DoB= personForm.DoB,
                DoK= personForm.DoK,
                Gov= personForm.Gov,
                Id=Guid.NewGuid(),
                Name= personForm.Name,
                PoK= personForm.PoK,
                Status=0,
                Story= personForm.Story,
                Type=personForm.Type
            };
            _dbContext.Add(person);
            var supportedTypes = new[] { "jpeg", "png", "jpg" };
            foreach(var check in Request.Form.Files)
            {
                var fileExt = System.IO.Path.GetExtension(check.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    return BadRequest(new
                    {
                        error = "File Extension Is InValid - Only Upload Image File"
                    });
                }
            }
            foreach (var index in Request.Form.Files)
            {
                var filename = index.FileName.Split(".")[0].Replace(index.FileName.Split(".")[0], Guid.NewGuid().ToString() + "_" + DateTime.Now.Day);
                await _dbContext.Images.AddAsync(new Image {
                   Id = Guid.NewGuid(),
                   Name = filename,
                   Key = Path.GetExtension(index.FileName),
                   PersonId = person.Id
               });
                var saved = await SaveFile(index, filename + Path.GetExtension(index.FileName));
              
            }
             await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                person,
                // image
            });
        }

        [HttpGet("All/{start}/{end}")]
        public async Task<IActionResult> All(int start,int end)
        {
            return Ok(
                await _dbContext.Persons.Include(x => x.Images).Where(x=>x.Status!=0).Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.PoK,
                    x.Gov,
                    x.DoB,
                    x.Type,
                    ImageName = x.Images.Select(z=> new string($"{z.Name}{z.Key}")).First()
                }
                    
                    ).AsNoTracking().OrderBy(x=>x.Id).Skip(start).Take(end).ToListAsync()
                );;
        }

        [HttpGet("GetByType/{start}/{end}/{type}")]
        public async Task<IActionResult> GetByType(int start, int end,Type type)
        {
            return Ok(
                await _dbContext.Persons.Include(x => x.Images).Where(x => x.Type.Equals(type) && x.Status != 0).Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.PoK,
                    x.Gov,
                    x.DoB,
                    
                    ImageName = x.Images.Select(z => new string($"{z.Name}{z.Key}")).First()
                }

                    ).AsNoTracking().OrderBy(x => x.Id).Skip(start).Take(end).ToListAsync()
                ); ;
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(
                await (from person in _dbContext.Persons
                       where person.Id==id && person.Status!=0
                       join image in _dbContext.Images on person.Id equals image.PersonId 
                       select new { 
                           person,
                           imageName=  new string($"{image.Name}{image.Key}"),
                           
                       }).FirstAsync()
                );

        }
        private async Task<bool> SaveFile(IFormFile file, string documentFileName)
        {
            if (file == null || file.Length <= 0) return false;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images",documentFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return true;
        }
    }
}