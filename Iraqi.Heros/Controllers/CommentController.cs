using System;
using System.Collections.Generic;
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
    public class CommentController : ControllerBase
    {
        private readonly MainDbContext _dbContext;
        public CommentController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("AddComment/{personId}")]
        public async Task<IActionResult> AddComment(string  comments, Guid personId)
        {

            var comment = new Comments()
            {
                Id = Guid.NewGuid(),
                Comment = comments,
                CommentDate = DateTime.Now,
                PersonId = personId

            };
            _dbContext.Add(comment);
            await _dbContext.SaveChangesAsync();
            return Ok(comment);
        }
        [HttpGet("Comments/{personId}/{start}/{end}")]
        public async Task<IActionResult> GetAction(Guid personId, int start, int end)
        {
            var result = await _dbContext.Comments.
                Select(x => new {
                    x.Comment,
                    x.CommentDate
                }).OrderBy(x => x.CommentDate).Skip(start).Take(end).ToListAsync();
            if (result.Count == 0)
                return BadRequest();
            return Ok(result);
        }
    }
}