namespace AtataUITestsDeletes.Pages;

using _ = SignInPage;

[Url("signin")]
public class SignInPage : Page<_>
{
    [FindById("email")]
    public TextInput<_> Email { get; private set; }
    [FindById("password")]
    public PasswordInput<_> Password { get; private set; }
    [WaitFor(Until.Visible)]
    public Button<_> SignIn { get; private set; }

    public ProductsPage Login(string email, string password)
    {
        Email.Set(email);
        Password.Type(password);
        return SignIn.ClickAndGo<ProductsPage>();

        
    }
}


