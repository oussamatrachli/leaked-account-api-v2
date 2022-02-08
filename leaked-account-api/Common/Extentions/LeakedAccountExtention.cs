using LeakedAccountApi.Models;
using LeakedAccountApi.Models.Commands;
using LeakedAccountApi.Models.ViewModel;

using MongoDB.Bson;

namespace LeakedAccountApi.Common.Extentions
{
    internal static class LeakedAccountExtention
    {
        public static LeakedAccountViewModel ToViewModel(this LeakedAccount leakedAccount)
        {
            return new LeakedAccountViewModel
            {
                Email = leakedAccount.Email,
                integrationTime = leakedAccount.integrationTime,
                Passwords = leakedAccount.Passwords
            };
        }

        public static LeakedAccount ToLeakedAccount(this LeakedAccountRequest leakedAccount)
        {
            return new LeakedAccount
            {
                Id = ObjectId.GenerateNewId(),
                Email = leakedAccount.Email,
                integrationTime = System.DateTime.Now,
                Passwords = leakedAccount.Passwords
            };
        }
    }

}
