﻿
using System;
using DBInterface;
using DBInterface.UnitOfWork;

using Models.User;

namespace Testing_to_Domain
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var newUser = new User
            {
                Email = "ab@ab.dk",
                GivenName = "TestingName",
                LastName = "TestingLastName",
                Password = "123%%%aaaa",
                Username = "1337User"
            };

            UnitOfWork xy = new UnitOfWork(new DbContext());

            xy.UserRepository.AddUser(newUser).Wait();
            Console.WriteLine(xy.UserRepository.GetUserByUsername("ab@ab.dk").Email);

            xy.UserRepository.DeleteUserByEmail("ab@ab.dk");

            xy.UserRepository.AddUser(newUser).Wait();
            newUser.GivenName = "replacedName";
            xy.UserRepository.ReplaceUser(newUser);

        }
    }
}
