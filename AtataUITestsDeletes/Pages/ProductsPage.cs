namespace AtataUITestsDeletes.Pages;

using _ = ProductsPage;

[Url("products")]
public class ProductsPage : Page<_>
{
    public Table<ProductTableRow, _> Products { get; private set; }

    public List<ProductTableRow> ProductRows => Products.Rows.ToList();

    public decimal GetTotalPrice() =>
    ProductRows.Select(row => row.Price.Value.GetValueOrDefault()).Sum();
    public decimal GetTotalAmount() =>
    ProductRows.Select(row => row.Amount.Value.GetValueOrDefault()).Sum();
}

