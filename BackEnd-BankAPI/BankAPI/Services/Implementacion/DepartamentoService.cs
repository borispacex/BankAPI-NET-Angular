using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Services.Contrato;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services.Implementacion
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly BankAPIContext _dbContext;

        public DepartamentoService(BankAPIContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Departamento>> GetList()
        {
            try
            {
                List<Departamento> lista = new List<Departamento>();
                lista = await _dbContext.Departamentos.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
