using System.Collections.Generic;
using System.Linq;

public class CustomerService
{
    private readonly ICustomerRepository _repo;

    public CustomerService(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Customer> SearchByNamePrefix(string prefix)
    {
        var all = _repo.GetAll();
        if (string.IsNullOrWhiteSpace(prefix)) return all;
        return all.Where(c => c.Name.StartsWith(prefix));
    }
}