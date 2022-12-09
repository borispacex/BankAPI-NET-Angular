using BankAPI.Data;
using BankAPI.Data.BankModels;

namespace BankAPI.Services
{
    public class AccountTypeService
    {
        private readonly BankAPIContext _context;
        public AccountTypeService(BankAPIContext context)
        {
            _context = context;
        }

        public async Task<AccountType?> GetById(int id)
        {
            return await _context.AccountTypes.FindAsync(id);
        }
    }
}
