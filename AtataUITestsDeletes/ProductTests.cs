using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atata;
using NUnit.Framework;

namespace AtataUITestsDeletes.ConfirmationPopups;

public class ProductTests : UITestFixture
{
    [Test]
    public void DeleteUsingJSConfirm() =>
    
        Go.To<ProductsPage>()
        .Products.Rows.Count.Get(out int count)

        .Products.Rows[x => x.Name == "Armchair"].DeleteUsingJSConfirm()
        .Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent()
        .Products.Rows.Count.Should.Equal(count - 1);
    [Test]
    public void DeleteUsingBSModal() =>
    Go.To<ProductsPage>()
        .Products.Rows.Count.Get(out int count)

        .Products.Rows[x => x.Name == "Armchair"].DeleteUsingBSModal()
            .Cancel() // Cancel and verify that nothing is deleted.
        .Products.Rows[x => x.Name == "Armchair"].Should.BePresent()
        .Products.Rows.Count.Should.Equal(count)

        .Products.Rows[x => x.Name == "Armchair"].DeleteUsingBSModal()
            .Delete() // Delete and verify that item is deleted.
        .Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent()
        .Products.Rows.Count.Should.Equal(count - 1);
    [Test]
    public void DeleteUsingBSModal_ViaTrigger() =>
    Go.To<ProductsPage>()
        .Products.Rows.Count.Get(out int count)

        .Products.Rows[x => x.Name == "Armchair"].DeleteUsingBSModalViaTrigger()
        .Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent()
        .Products.Rows.Count.Should.Equal(count - 1);

    [Test]
    public void DeleteArmchair_And_ValidateSummary()
    {
        var page = Go.To<ProductsPage>();

        int initialCount = page.Products.Rows.Count;

        var armchairRow = page.Products.Rows.FirstOrDefault(x => x.Name == "Armchair");

        if (armchairRow != null)
            armchairRow.DeleteUsingJSConfirm();

        page.Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent();

        page.Products.Rows.Count.Should.Equal(initialCount - 1);

        decimal expectedPrice = page.GetTotalPrice() - armchairRow?.Price.Value.GetValueOrDefault() ?? 0m;
        decimal expectedAmount = page.GetTotalAmount() - armchairRow?.Amount.Value.GetValueOrDefault() ?? 0m;
        Console.WriteLine($"Expected Price: {expectedPrice}, Actual: {page.GetTotalPrice()}");
        Console.WriteLine($"Expected Amount: {expectedAmount}, Actual: {page.GetTotalAmount()}");
        //page.GetTotalPrice().Should.Equal(expectedPrice);
        //page.GetTotalAmount().Should.Equal(expectedAmount);
        //page.GetTotalPrice().Should.Equal(565m);
        //page.GetTotalAmount().Should.Equal(245m);
    }
}


