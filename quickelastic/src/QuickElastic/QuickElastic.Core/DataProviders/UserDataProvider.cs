using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using QuickElastic.Core.Domain;

namespace QuickElastic.Core.DataProviders
{
    public class UserDataProvider : IDataProvider<User>
    {
        public UserDataProvider()
        {
            Randomizer.Seed = new Random(14);
        }

        public async Task<IEnumerable<User>> GetData()
        {
            // This would come from a third party data source
            var userFactory = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Username, f => f.Person.UserName)
                .RuleFor(u => u.AvatarUrl, f => f.Person.Avatar)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(u => u.Phone, f => f.Person.Phone)
                .RuleFor(u => u.Website, f => f.Person.Website);

            return await Task<IEnumerable<User>>.Factory.StartNew(() => userFactory.Generate(42));
        }
    }
}
