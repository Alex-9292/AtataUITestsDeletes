using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atata;
using Newtonsoft.Json.Serialization;

namespace AtataUITestsDeletes.ConfirmationPopups;

using _ = ProductsPage;

[Url("products")]
public class ProductTableRow : TableRow<_>
{
    public Text<_> Name { get; private set; }

    public Currency<_> Price { get; private set; } //Currency<_> Price

    public Number<_> Amount { get; private set; }

    [CloseConfirmBox]
    public ButtonDelegate<_> DeleteUsingJSConfirm { get; private set; }

    [FindByContent("Delete Using BS Modal")]
    public ButtonDelegate<DeletionConfirmationBSModal<_>, _> DeleteUsingBSModal { get; private set; }

    [FindByContent("Delete Using BS Modal")]
    [ConfirmDeletionViaBSModal]
    public ButtonDelegate<_> DeleteUsingBSModalViaTrigger { get; private set; }
}
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




