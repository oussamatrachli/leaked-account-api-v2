using LeakedAccountApi.Common.Abstraction;
using LeakedAccountApi.Common.Extentions;
using LeakedAccountApi.Models.Commands;
using LeakedAccountApi.Models.ViewModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeakedAccountApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeakedAccountController : ControllerBase
    {

        private readonly ILogger<LeakedAccountController> _logger;
        private readonly ILeakedAccountRepository _leakedAccountRepository;

        public LeakedAccountController(ILogger<LeakedAccountController> logger, ILeakedAccountRepository leakedAccountRepository)
        {
            _logger = logger;
            _leakedAccountRepository = leakedAccountRepository;
        }

        [HttpGet]
        public async Task<ActionResult<LeakedAccountViewModel>> GetAsync([FromQuery] string mail)
        {
            try
            {
                if (string.IsNullOrEmpty(mail))
                {
                    return BadRequest();
                }

                var result = await _leakedAccountRepository.GetLeakedAccountByEmail(mail).ConfigureAwait(false);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result.ToViewModel());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("check")]
        public async Task<ActionResult<bool>> CheckAsync([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return BadRequest();
                }

                return Ok(await _leakedAccountRepository.CheckIsLeakedAccount(email, password).ConfigureAwait(false));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public async Task<ActionResult> POST([FromBody] LeakedAccountRequest leakedAccount)
        {
            try
            {
                if (leakedAccount == null)
                {
                    return BadRequest();
                }
                return Ok(await _leakedAccountRepository.CreateLeakedAccount(leakedAccount.ToLeakedAccount()).ConfigureAwait(false));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] List<string> passwords)
        {
            try
            {
                if (passwords == null || passwords.Count == 0)
                {
                    return BadRequest();
                }
                return Ok(await _leakedAccountRepository.UpdateLeakedAccount(passwords).ConfigureAwait(false));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest();
                }
                return Ok(await _leakedAccountRepository.DeleteLeakedAccount(email).ConfigureAwait(false));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
