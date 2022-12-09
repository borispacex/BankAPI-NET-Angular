using BankAPI.Data;
using BankAPI.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services
{
    public class ClientService
    {
        private readonly BankAPIContext _context;

        public ClientService(BankAPIContext context)
        {
            _context = context;
        }

        // Obtener todos
        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.ToListAsync();
        }

        // Obtener por id
        public async Task<Client?> GetById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        // Crear
        public async Task<Client> Create(Client newClient)
        {
            _context.Clients.Add(newClient);
            await _context.SaveChangesAsync();

            return newClient;
        }

        // Actualizar
        public async Task Update(int id, Client client)
        {
            var existingClient = await GetById(id);
            if (existingClient is not null)
            {
                existingClient.Name = client.Name is null ? existingClient.Name : client.Name;
                existingClient.PhoneNumber = client.PhoneNumber is null ? existingClient.PhoneNumber : client.PhoneNumber;
                existingClient.Email = client.Email is null ? existingClient.Email : client.Email;
                await _context.SaveChangesAsync();
            }
        }

        // Eliminar
        public async Task Delete(int id)
        {
            var clientToDelete = await GetById(id);
            if (clientToDelete is not null)
            {
                _context.Clients.Remove(clientToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
