using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MuazaAngular.Models;
using MuazaAngular.Repo;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using MuazaWebApi.ProjectProcessing;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MuazaAngular.Controllers
{
    [Route("api/[controller]")]
    
    public class DataController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly DataContext _context;
        private ProjectProcessing _projectProcessing;
        public DataController(DataContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _projectProcessing = new ProjectProcessing(_context);
            if (_context.Messages.Count() == 0)
            {
                _context.Messages.Add(new Message {first_name="Muaz", email="muaza@clemson.edu", content="I appreciate the deisgn integrity of your work"});
                _context.Messages.Add(new Message { first_name = "Ahmad", email = "ahmad@clemson.edu", content = "I appreciate the deisgn integrity of your work" });
                _context.SaveChanges();
            }
        }
        // GET: api/<controller>
        [HttpGet("[action]")]
        public IEnumerable<Message> Get()
        {
            return _context.Messages.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("[action]/{id}")]
        public string Get(int id)
        {
            return "value";
        }
        // GET: api/<controller>
        [HttpPost("[action]")]
        public IActionResult Post([FromBody] Message body)
        {
            if(ModelState.IsValid)
            {
                _context.Add(body);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
           
        }
        
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("[action]/{id}")]
        public StatusCodeResult Delete(int id)
        {
            var element = _context.Messages.Find(id);
            if(element == null)
            {
                return NotFound();
            }
            _context.Messages.Remove(element);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult CreateProject(Project project)
        {
            
           
                var httpRequest = HttpContext.Request.Form;
                string webRootPath = _hostingEnvironment.WebRootPath;
                _projectProcessing.saveProject(project, httpRequest.Files, webRootPath);
                return Ok();
            
            //return NotFound(Json("Please ensure all requried* fields are filled"));
               
            
            
            
        }
        [HttpGet("[action]")]
        public IEnumerable<_Project> GetProjects()
        {
            //return _context.Projects.ToList();
            return _projectProcessing.getProjects();
        }
        // DELETE api/<controller>/5
        [HttpDelete("[action]/{id}")]
        public StatusCodeResult DeleteProject(int id)
        {
            if(!_projectProcessing.deleteProject(id))
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
