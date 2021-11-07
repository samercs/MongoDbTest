using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbTest.Configration;
using MongoDbTest.Keys;
using MongoDbTest.Models;

namespace MongoDbTest.Services
{
    public class EmployeeService: IServiceBase
    {
        private readonly ServiceBase _serviceBase;
        private readonly string _collectionName = DatabaseCollectionNames.EmployeeCollectionsName;
        private readonly IMongoCollection<Employee> _client;
        public EmployeeService(ServiceBase serviceBase)
        {
            _serviceBase = serviceBase;
            _client = _serviceBase.GetMongoDb().GetCollection<Employee>(_collectionName);
        }

        public async Task<IReadOnlyCollection<Employee>> GetAll()
        {
            return await (await _client.FindAsync(i => true)).ToListAsync();
        }

        public async Task<Employee> Create(Employee employee)
        {
            await _client.InsertOneAsync(employee);
            return employee;
        }

        public async Task Delete(string id)
        {
            await _client.DeleteOneAsync(i => i.Id == id);
        }

        public async Task<Employee> GetById(string id)
        {
            return (await _client.FindAsync(i => i.Id == id)).FirstOrDefault();
        }

        public async Task<Employee> Update(Employee employee)
        {
            var update = Builders<Employee>.Update
                .Set(c => c.Name, employee.Name)
                .Set(c => c.DeptName, employee.DeptName);
            await _client.UpdateOneAsync(i => i.Id == employee.Id, update);
            return employee;
        }
    }
}