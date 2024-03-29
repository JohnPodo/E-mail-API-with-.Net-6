﻿using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class UserRepo :MasterRepo
    {
        public async Task<MailMeUpUser> GetUserById(int id)
        {
            var user = await _DbContext.MailMeUpUsers.Where(u=>u.Id == id).FirstOrDefaultAsync();
            return user;
        } 

        public async Task<MailMeUpUser> GetUserByActiveToken(Guid activeToken)
        {
            var user = await _DbContext.MailMeUpUsers.Where(u => u.ActiveToken == activeToken).FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<MailMeUpUser>> GetAllUsers() => await _DbContext.MailMeUpUsers.ToListAsync();

        public async Task AddUser(MailMeUpUser user)
        {
            await _DbContext.MailMeUpUsers.AddAsync(user);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var userToDel = await GetUserById(id);
            if (userToDel is null) return;
            _DbContext.MailMeUpUsers.Remove(userToDel);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<MailMeUpUser> GetUserFromUsername(string username)
        {
            var user = await _DbContext.MailMeUpUsers.Where(s => s.Username == username).FirstOrDefaultAsync();
            return user;
        }

        public async Task UpdateUser(MailMeUpUser user)
        {
            if(user == null) return;
            if (user.Id == 0) return;
            var userToModify = await GetUserById(user.Id);
            userToModify.IsAdmin = user.IsAdmin;
            userToModify.Password = user.Password;
            userToModify.Username = user.Username;
            userToModify.EmailPassword = user.EmailPassword;
            userToModify.EmailUsername = user.EmailUsername;
            userToModify.ActiveToken = user.ActiveToken;
            userToModify.EmailAddress = user.EmailAddress;
            await _DbContext.SaveChangesAsync();
        }
    }
}
