﻿using users_directory.Models;

namespace users_directory.Services
{
    public interface IPhoneNumbersRepository:IRepository<PhoneNumber>
    {
        Task<bool> Delete(int id);
    }
}
