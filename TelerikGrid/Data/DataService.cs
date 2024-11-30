using TelerikGrid.Data;
using TelerikGrid.DbContext;
using TelerikGrid.Models.Invoice;
using TelerikGrid.Models.Products;

public class DataService : IDataService
{
    // فرض بر این است که InvoiceList یک آرایه یا لیست در DbContext است  
    private readonly ApplicationDbContext _context;
    public IWebHostEnvironment WebHostEnvironment { get; set; }

    public DataService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        WebHostEnvironment = webHostEnvironment;
    }

    // دریافت تمامی فاکتورها  
    public List<Invoice> GetInvoices()
    {
        return _context.Invoices.ToList(); // InvoiceList یک آرایه در DbContext  
    }

    // دریافت فاکتور براساس شناسه  
    public Invoice GetInvoiceById(int id)
    {
        return _context.Invoices.FirstOrDefault(i => i.Id == id);
    }

    // ایجاد فاکتور جدید  
    public void CreateInvoice(Invoice invoice)
    {
        var invoiceList = _context.Invoices.ToList();
        invoiceList.Add(invoice); // افزودن فاکتور جدید به آرایه  
        _context.Invoices = invoiceList; // تبدیل به آرایه و ذخیره مجدد  
    }

    // ویرایش فاکتور موجود  
    public void UpdateInvoice(Invoice invoice)
    {
        var invoiceList = _context.Invoices.ToList(); // تبدیل آرایه به لیست  
        var existingInvoice = invoiceList.FirstOrDefault(i => i.Id == invoice.Id);
        if (existingInvoice != null)
        {
            existingInvoice = invoice; // به‌روزرسانی فاکتور موجود  
        }
        _context.Invoices = invoiceList; // تبدیل به آرایه و ذخیره مجدد  
    }

    // حذف فاکتور  
    public void DeleteInvoice(int id)
    {
        var invoiceList = _context.Invoices.ToList(); // تبدیل آرایه به لیست  
        var invoiceToRemove = invoiceList.FirstOrDefault(i => i.Id == id);
        if (invoiceToRemove != null)
        {
            invoiceList.Remove(invoiceToRemove); // حذف فاکتور از لیست  
            _context.Invoices = invoiceList; // تبدیل به آرایه و ذخیره مجدد  
        }
    }



    public List<Product> GetProducts()
    {
        return _context.Products;
    }

    public List<Unit> LoadUnitsByProductId(int productId) {
        return _context.Units.Where(u => u.ProductId == productId).ToList();
    }

}