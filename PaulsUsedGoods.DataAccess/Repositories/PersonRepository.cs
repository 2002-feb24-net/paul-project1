using System;
using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;
using PaulsUsedGoods.DataAccess.Context;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PaulsUsedGoods.Domain.Interfaces;

namespace PaulsUsedGoods.DataAccess.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly UsedGoodsDbContext _dbContext;
        private readonly ILogger<ItemRepository> _logger;

        public PersonRepository(UsedGoodsDbContext dbContext, ILogger<ItemRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

// ! CLASS SPECIFIC
        public List<Domain.Model.Person> GetPeopleByName(string personName = null)
        {
            _logger.LogInformation($"Retrieving people with the name: {personName}");
            List<Context.Person> personList = _dbContext.People
                .ToList();
            if (personName != null)
            {
                personList = personList.FindAll(p => p.Username == personName);
            }
            return personList.Select(Mapper.MapPerson).ToList();
        }
        public Domain.Model.Person GetPersonById(int personId)
        {
            _logger.LogInformation($"Retrieving person id: {personId}");
            Context.Person returnPerson = _dbContext.People
                .First(p => p.PersonId == personId);
            return Mapper.MapPerson(returnPerson);
        }
        public void AddPerson(Domain.Model.Person inputPerson)
        {
            if (inputPerson.Id != 0)
            {
                _logger.LogWarning($"Person to be added has an ID ({inputPerson.Id}) already: ignoring.");
            }

            _logger.LogInformation("Adding person");

            Context.Person entity = Mapper.UnMapPerson(inputPerson);
            entity.PersonId = _dbContext.People.Max(p => p.PersonId)+1;
            _dbContext.Add(entity);
        }
        public void DeletePersonById(int personId)
        {
            _logger.LogInformation($"Deleting item with ID {personId}");
            Context.Item entity = _dbContext.Items.Find(personId);
            _dbContext.Remove(entity);
        }
        public void UpdatePerson(Domain.Model.Person inputPerson)
        {
            _logger.LogInformation($"Updating person with ID {inputPerson.Id}");
            Context.Person currentEntity = _dbContext.People.Find(inputPerson.Id);
            Context.Person newEntity = Mapper.UnMapPerson(inputPerson);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }
// ! GENERAL COMMANDS
        public void Save()
        {
            _logger.LogInformation("Saving changes to the database");
            _dbContext.SaveChanges();
        }
    }
}
