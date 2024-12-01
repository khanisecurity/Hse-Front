using Microsoft.AspNetCore.Mvc;
using TelerikGrid.Data;
using TelerikGrid.DbContext;

namespace TelerikGrid.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IDataService dataService;

        public InvoiceController(IDataService dataService)
        {
            this.dataService = dataService;
        }


        [HttpGet]
        public async Task<IActionResult> GetInvoice()
        {
            var Invoice =  dataService.GetInvoices();
            return Ok(Invoice);
        }

        // بارگذاری واحدهای مرتبط با کالا  
        //[HttpGet("{id}/units")]
        //public IActionResult GetUnitsByProductId(int id)
        //{
        //    var units = dataService.GetUnitsByProductIdAsync(id);
        //    return Ok(units);
        //}
    }
}