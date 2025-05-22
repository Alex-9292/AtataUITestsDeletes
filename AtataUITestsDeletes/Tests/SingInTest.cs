using AtataUITestsDeletes.Pages;

namespace AtataUITestsDeletes.Tests;
public class SingInTest : UITestFixture
{

    [Test]
    public void LoginTestWhithAggregate()
    {
        //Arrange
        var singIn = Go.To<SignInPage>();
        //Act
        singIn.Login("admin@mail.com", "abc123");
        //Assert
        Go.To<SignInPage>()
            .AggregateAssert(x =>
            {
                x.PageTitle.Equals("Users - Atata Sample App");

            });
    }
}