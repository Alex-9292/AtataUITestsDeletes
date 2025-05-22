using AtataUITestsDeletes.Pages;

namespace AtataUITestsDeletes.Tests;
[TestFixture]
public class ProductTests : UITestFixture
{
    //private ProductsPage _productsPage;

    //[OneTimeSetUp]
    //public void GlobalSetUp()
    //{
    //    _productsPage = Go.To<SignInPage>()
    //        .Login("admin@mail.com", "abc123");
    //}

    [Test]
    public void DeleteUsingJSConfirm()
    {
        //Arrange
        var page = Go.To<ProductsPage>();

        //Act
        page.Products.Rows.Count.Get(out int count);
        page.Products.Rows[x => x.Name == "Armchair"].DeleteUsingJSConfirm();

        //Assert
        page.Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent();
        page.Products.Rows.Count.Should.Equal(count - 1);
    }

    [Test]
    public void DeleteUsingBSModal()
    {
        //Arrange
        var page = Go.To<ProductsPage>();

        //Act
        page.Products.Rows.Count.Get(out int count);
        page.Products.Rows[x => x.Name == "Armchair"].DeleteUsingBSModal()
            .Cancel();
        page.Products.Rows[x => x.Name == "Armchair"].Should.BePresent();
        page.Products.Rows.Count.Should.Equal(count);
        page.Products.Rows[x => x.Name == "Armchair"].DeleteUsingBSModal()
            .Delete();
        page.Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent();
        page.Products.Rows.Count.Should.Equal(count - 1);
    }

    [Test]
    public void DeleteUsingBSModal_ViaTrigger()
    {
        //Arrange
        var page = Go.To<ProductsPage>();
        int count = page.Products.Rows.Count;
        //Act
        //productPage.Get(count);//.Products.Rows.Count.Get(count);
        page.Products.Rows[x => x.Name == "Armchair"].DeleteUsingBSModalViaTrigger();

        //Assert
        page.Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent();
        page.Products.Rows.Count.Should.Equal(count - 1);
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
        page.Products.Rows.Count.Should.Equal(4);
        page.GetTotalPrice().ToSutSubject().Should.Be(expectedPrice.GetValueOrDefault());
        page.GetTotalAmount().ToSutSubject().Should.Be(245m);
    }
}

















