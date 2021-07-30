using Core.Process;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequeWriterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChequeWriterController: ControllerBase
    {
        ProcessNumberToString _process;

        public ChequeWriterController(ProcessNumberToString process)
        {
            _process = process;
        }


        [HttpGet]
        public IActionResult Get(string amount)
        {
            var result = string.Empty;

            if (!string.IsNullOrEmpty(amount))
            {
                result = _process.ConvertNumberToStringProcess(amount);
                return Ok(result);
            }
            else
                return BadRequest();
            
        }
    }
}
