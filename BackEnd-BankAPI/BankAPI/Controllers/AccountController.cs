using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using BankAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly AccountTypeService _accountTypeService;
        private readonly ClientService _clientService;
        public AccountController(AccountService accountService, AccountTypeService accountTypeService, ClientService clientService)
        {
            this._accountService = accountService;
            this._accountTypeService = accountTypeService;
            this._clientService = clientService;
        }

        [HttpGet]
        public async Task<IEnumerable<AccountDtoOut>> Get()
        {
            return await _accountService.GetAll();
        }

        [HttpGet("dto/{id}")]
        public async Task<ActionResult<AccountDtoOut>> GetDtoById(int id)
        {
            var account = await _accountService.GetDtoById(id);
            if (account is null) return AccountNotFound(id);

            return account;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetById(int id)
        {
            var account = await _accountService.GetById(id);
            if (account is null) return AccountNotFound(id);
            
            return account;
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountDtoIn account)
        {
            string validationResult = await ValidateAccount(account);

            if (!validationResult.Equals("Valid")) return BadRequest(new { message = validationResult });

            var newAccount = await _accountService.Create(account);
            return CreatedAtAction(nameof(GetById), new { id = newAccount.Id }, newAccount);
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AccountDtoIn account)
        {
            if (id != account.Id) return BadRequest(new { nessage = $"El ID {id} de la URL no coincidi con el ID {account.Id} del cuerpo de la solicitud." });

            var accountToUpdate = await _accountService.GetById(id);

            if (accountToUpdate is not null)
            {
                string validationResult = await ValidateAccount(account);
                if (!validationResult.Equals("Valid")) return BadRequest(new { message = validationResult });

                await _accountService.Update(id, account);
                return NoContent();
            } else
            {
                return AccountNotFound(id);
            }
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var accountToDelete = await _accountService.GetById(id);

            if (accountToDelete is not null)
            {
                await _accountService.Delete(id);
                return Ok();
            }
            else
            {
                return AccountNotFound(id);
            }
        }
        private async Task<string> ValidateAccount(AccountDtoIn account)
        {
            string result = "Valid";
            var accountType = await _accountTypeService.GetById(account.AccountType);
            if (accountType is null) result = $"El tipo de cuenta {account.AccountType} no existe.";

            var clientID = account.ClientId.GetValueOrDefault();
            var client = await _clientService.GetById(clientID);
            if (client is null) result = $"El cliente {clientID} no existe.";

            return result;
        }

        private NotFoundObjectResult AccountNotFound(int id)
        {
            return NotFound(new { message = $"La cuenta con ID = {id} no existe." });
        }
    }
}
