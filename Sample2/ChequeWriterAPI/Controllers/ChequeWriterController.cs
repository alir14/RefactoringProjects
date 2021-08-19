using Core.Process;
using Microsoft.AspNetCore.Mvc;

namespace ChequeWriterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChequeWriterController: ControllerBase
    {
        ConvertMoneyToString _process;

        public ChequeWriterController(ConvertMoneyToString process)
        {
            _process = process;
        }


        [HttpGet]
        public IActionResult Get(string amount)
        {
            var result = string.Empty;

            if (!string.IsNullOrEmpty(amount))
            {
                result = _process.ConvertMoneyToStringProcess(amount);
                return Ok(result);
            }
            else
                return BadRequest();
            
        }
    }
}
