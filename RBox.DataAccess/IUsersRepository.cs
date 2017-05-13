﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBox.Model;

namespace RBox.Data
{
    public interface IUsersRepository
    {
        User AddUser(User user);
        void DeleteUser(Guid userId);
        User GetUser(Guid userId);
        void UpdateUser(Guid userId, User user);
    }
}
