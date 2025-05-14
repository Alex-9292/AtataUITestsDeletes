using AtataUITestsDeletes.Pages;

namespace AtataUITestsDeletes.Tests;

public class ProductTests : UITestFixture
{
    [Test]
    public void DeleteUsingJSConfirm()
    {
        //Arrange
        var productPage = Go.To<ProductsPage>();

        //Act
        productPage.Products.Rows.Count.Get(out int count);
        productPage.Products.Rows[x => x.Name == "Armchair"].DeleteUsingJSConfirm();

        //Assert
        productPage.Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent();
        productPage.Products.Rows.Count.Should.Equal(count - 1);
    }

    [Test]
    public void DeleteUsingBSModal()
    {
        //Arrange
        var productPage = Go.To<ProductsPage>();

        //Act
        productPage.Products.Rows.Count.Get(out int count);
        productPage.Products.Rows[x => x.Name == "Armchair"].DeleteUsingBSModal()
            .Cancel();
        productPage.Products.Rows[x => x.Name == "Armchair"].Should.BePresent();
        productPage.Products.Rows.Count.Should.Equal(count);
        productPage.Products.Rows[x => x.Name == "Armchair"].DeleteUsingBSModal()
            .Delete();
        productPage.Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent();
        productPage.Products.Rows.Count.Should.Equal(count - 1);
    }

    [Test]
    public void DeleteUsingBSModal_ViaTrigger()
    {
        //Arrange
        var productPage = Go.To<ProductsPage>();

        //Act
        productPage.Products.Rows.Count.Get(out int count);
        productPage.Products.Rows[x => x.Name == "Armchair"].DeleteUsingBSModalViaTrigger();

        //Assert
        productPage.Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent();
        productPage.Products.Rows.Count.Should.Equal(count - 1);
    }

    [Test]
    public void DeleteArmchair_And_ValidateSummary()
    {
        //Arrange
        var page = Go.To<ProductsPage>();
        int initialCount = page.Products.Rows.Count;
        var armchairRow = page.Products.Rows.FirstOrDefault(x => x.Name == "Armchair");
        var armchairPrice = armchairRow.Price.Value;
        var armchairAmount = armchairRow.Amount.Value;
        var expectedPrice = page.GetTotalPrice() - armchairPrice;
        var expectedAmount = page.GetTotalAmount() - armchairAmount;

        //Act
        armchairRow?.DeleteUsingJSConfirm();

        //Assert
        page.Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent();
        page.Products.Rows.Count.Should.Equal(initialCount - 1);
        //page.Products.Rows.Count.Should.HaveCount(4);
        page.GetTotalPrice().ToSutSubject().Should.Be(expectedPrice.GetValueOrDefault());
        page.GetTotalAmount().ToSutSubject().Should.Be(expectedAmount.GetValueOrDefault());
    }
}


