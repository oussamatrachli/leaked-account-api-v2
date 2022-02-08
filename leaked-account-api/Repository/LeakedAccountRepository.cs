using LeakedAccountApi.Common.Abstraction;
using LeakedAccountApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeakedAccountApi.Repository
{
    public class LeakedAccountRepository : ILeakedAccountRepository
    {
        private static IMongoCollection<LeakedAccount> _collection;

        public LeakedAccountRepository()
        {
            //
            var client = new MongoClient("mongodb://localhost:27017/admin?readPreference=primary&authSource=admin&appname=MongoDB%20Compass&ssl=false");
            var database = client.GetDatabase("credentials");
            _collection = database.GetCollection<LeakedAccount>("leakedAccount");
        }

        /// <summary>
        /// Get leaked acount with email
        /// </summary>
        /// <param name="email">The email</param>
        /// <returns>Returns leaked account details</returns>
        public async Task<LeakedAccount> GetLeakedAccountByEmail(string email)
        {
            var builder = Builders<LeakedAccount>.Filter;
            var filter = builder.And(builder.Eq("email", email));

            return await _collection.Find(filter).Limit(1)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Check if the user and password are leaked: exist in the leaked account referential
        /// </summary>
        /// <param name="email">The account email</param>
        /// <param name="password">The account password</param>
        /// <returns>is the account leaked</returns>
        public async Task<bool> CheckIsLeakedAccount(string email, string password)
        {

            try
            {
                var builder = Builders<LeakedAccount>.Filter;
                var filter = builder.And(builder.Eq("email", email), builder.Eq("passwords", password));
                var result = await _collection.Find(filter).ToListAsync();
                return await Task.FromResult(result.Any());
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }

        /// <summary>
        /// Create leaked account
        /// </summary>
        /// <param name="leakedAccount">The leaked account</param>
        /// <returns>true if operation success</returns>
        public async Task<bool> CreateLeakedAccount(LeakedAccount leakedAccount)
        {
            try
            {
                _collection.InsertOneAsync(leakedAccount);
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }

        /// <summary>
        /// Delete leaked account with the email
        /// </summary>
        /// <param name="email">The email</param>
        /// <returns>true if operation success</returns>
        public async Task<bool> DeleteLeakedAccount(string email)
        {
            try
            {
                var builder = Builders<LeakedAccount>.Filter;
                var filter = builder.Eq("email", email);
                var result = await _collection.DeleteManyAsync(filter);
                return await Task.FromResult(result.DeletedCount > 0);
            }
            catch (Exception)
            {

                return await Task.FromResult(false);
            }

        }

        /// <summary>
        /// Updates leaked account: add passwords to the password list
        /// </summary>
        /// <param name="passwords">The passwords list</param>
        /// <returns>ture if operation success</returns>
        public async Task<bool> UpdateLeakedAccount(List<string> passwords)
        {
            try
            {
                var update = Builders<LeakedAccount>.Update.PushEach("passwords", passwords);
                var result = _collection.UpdateMany(_ => true, update);
                return await Task.FromResult(result.ModifiedCount > 0);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
