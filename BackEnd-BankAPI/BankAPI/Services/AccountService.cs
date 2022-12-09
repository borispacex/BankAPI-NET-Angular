using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services
{
    public class AccountService
    {
        private readonly BankAPIContext _context;

        public AccountService(BankAPIContext context)
        {
            _context = context;
        }

        // Obtener todas las cuentas
        public async Task<IEnumerable<AccountDtoOut>> GetAll()
        {
            return await _context.Accounts.Select(account => new AccountDtoOut
            {
                Id = account.Id,
                AccountName = account.AccountTypeNavigation.Name,
                ClientName = account.Client != null ? account.Client.Name : "",
                Balance = account.Balance,
                RegDate = account.RegDate
            }).ToListAsync();
        }

        // Obtener cuenta por ID, pero con una salida diferente
        public async Task<AccountDtoOut?> GetDtoById(int id)
        {
            return await _context.Accounts
                .Where(a => a.Id == id)
                .Select(account => new AccountDtoOut
                {
                    Id = account.Id,
                    AccountName = account.AccountTypeNavigation.Name,
                    ClientName = account.Client != null ? account.Client.Name : "",
                    Balance = account.Balance,
                    RegDate = account.RegDate
                }).SingleOrDefaultAsync();
        }

        // Obtener cuenta por ID
        public async Task<Account?> GetById(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        // Crear nueva cuenta
        public async Task<Account> Create(AccountDtoIn newAccountDTO)
        {
            var newAccount = new Account();
            newAccount.AccountType = newAccountDTO.AccountType;
            newAccount.ClientId = newAccountDTO.ClientId;
            newAccount.Balance = newAccountDTO.Balance;

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();
            return newAccount;
        }

        // Actualizar cuenta
        public async Task Update(int id, AccountDtoIn account)
        {
            var existingAccount = await GetById(id);
            if (existingAccount is not null)
            {
                existingAccount.AccountType = account.AccountType;
                existingAccount.ClientId = account.ClientId is null ? existingAccount.ClientId : account.ClientId;
                existingAccount.Balance = account.Balance;
                await _context.SaveChangesAsync();
            }
        }

        // Eliminar cuenta
        public async Task Delete(int id)
        {
            var accountToDelete = await GetById(id);
            if (accountToDelete is not null)
            {
                _context.Accounts.Remove(accountToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
