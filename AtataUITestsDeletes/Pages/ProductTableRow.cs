namespace AtataUITestsDeletes.Pages;

using _ = ProductsPage;

[Url("products")]
public class ProductTableRow : TableRow<_>
{
    public Text<_> Name { get; private set; }
    
    public Currency<_> Price { get; private set; }

    public Number<_> Amount { get; private set; }

    [CloseConfirmBox]
    public ButtonDelegate<_> DeleteUsingJSConfirm { get; private set; }

    [FindByContent("Delete Using BS Modal")]
    public ButtonDelegate<DeletionConfirmationBSModal<_>, _> DeleteUsingBSModal { get; private set; }

    [FindByContent("Delete Using BS Modal")]
    [ConfirmDeletionViaBSModal]
    public ButtonDelegate<_> DeleteUsingBSModalViaTrigger { get; private set; }
}







