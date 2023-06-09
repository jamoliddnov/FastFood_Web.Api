﻿using FastFood_Web.DataAccess.Interfaces.Common;
using FastFood_Web.Domain.Entities;

namespace FastFood_Web.DataAccess.Interfaces
{
    public interface IAccountRepositorie : IGenericRepositorie<User>
    {
        public Task<User?> GetByEmailAsync(string email);
    }
}
