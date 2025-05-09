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
    public void DeleteArmchair_And_ValidateSummary() =>
    
        Go.To<ProductsPage>()

            // Удаляем строку с "Armchair"
            .Products.Rows[x => x.Name == "Armchair"].DeleteUsingJSConfirm()

            // Проверяем, что "Armchair" больше не существует
            .Products.Rows[x => x.Name == "Armchair"].Should.Not.BePresent()
        // Проверка количества строк
            .Products.Rows.Count.Should.Equal(4)

            //.TotalPrice.Should.Equal(565m)
            //.TotalAmount.Should.Equal(245m);
             //Подсчёт общей цены и проверка через Should.Equal
            .GetTotalPrice().Should.Equal(565m)

             //Подсчёт общего количества товаров
            .GetTotalAmount().Should.Equal(245m);
        
    }
