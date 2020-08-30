using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyStore.Controllers
{
    [Route("/")]
    [ApiController]
    public class KeyValueController : ControllerBase
    {
        GithubOptions _githubOptions;
        public KeyValueController(GithubOptions githubOptions)
        {
            _githubOptions = githubOptions;
        }

        [HttpPost]
        public IActionResult Save(string key, string value)
        {
            return Ok(_githubOptions.SaveKeyValue(key, value));
        }

        [HttpGet("{key}")]
        public IActionResult GetValue(string key)
        {
            try
            {
                return Ok(_githubOptions.GetValue(key));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("/View/Repo")]
        public IActionResult GetRepo()
        {
            return Redirect(_githubOptions.Url);
        }
    }
}
