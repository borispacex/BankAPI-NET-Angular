using BankAPI.Data.BankModels;

namespace BankAPI.Services.Contrato
{
    public interface IDepartamentoService
    {
        Task<List<Departamento>> GetList();
    }
}
