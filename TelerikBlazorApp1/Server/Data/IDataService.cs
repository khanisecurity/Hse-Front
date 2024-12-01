using System.Collections.Generic;
using TelerikBlazorApp1.Shared.Models.Employee;
using TelerikBlazorApp1.Shared.Models.Sales;

namespace TelerikBlazorApp1.Server.Data
{
    public interface IDataService
    {
        List<Employee> GetEmployees();
        IEnumerable<Sale> GetSales();
        List<Team> GetTeams();
    }
}
