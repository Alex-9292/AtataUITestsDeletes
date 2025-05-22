using AtataUITestsDeletes.Pages;


namespace AtataUITestsDeletes.Tests;

[TestFixture]
public class ProductTestPartB : UITestFixture
{

        [OneTimeSetUp]
    public void GlobalSetUp()
    {
        Go.To<SignInPage>()
            .Login("admin@mail.com", "abc123");
    }

    [Test]
    public void ExapleTestWhithAggregate()
    {
        //Arrange

        var _productsPage = Go.To<ProductsPage>();
        int initialCount = _productsPage.Products.Rows.Count;
        var armchairRow = _productsPage.Products.Rows.FirstOrDefault(x => x.Name == "Armchair");
        var armchairPrice = armchairRow.Price.Value;
        var armchairAmount = armchairRow.Amount.Value;
        var expectedPrice = _productsPage.GetTotalPrice() - armchairPrice;
        var expectedAmount = _productsPage.GetTotalAmount() - armchairAmount;

        //Act

        armchairRow?.DeleteUsingJSConfirm();
        int currentCount = _productsPage.Products.Rows.Count;

        decimal actualPrice = _productsPage.GetTotalPrice();
        decimal actualAmount = _productsPage.GetTotalAmount();

        //Assert

        //Go.To<ProductsPage>()
            _productsPage.AggregateAssert(x =>
        {
            x.Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent();
            x.Products.Rows.Count.Should.Equal(initialCount - 1);
            x.GetTotalAmount().ToSutSubject().Should.Be(actualAmount);
            x.GetTotalPrice().ToSutSubject().Should.Equal(actualPrice);
        });
    }

    [OneTimeTearDown]
    public void Cleanup() => AtataContext.Current?.Dispose();
}



