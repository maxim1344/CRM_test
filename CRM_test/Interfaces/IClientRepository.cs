﻿using CRM_test.Models;

namespace CRM_test.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();

        Task<Client> GetByIdAsync(int id);

        bool Add(Client client);

        bool Update(Client client);

        bool Delete(Client client);

        bool Save();
    }
}
