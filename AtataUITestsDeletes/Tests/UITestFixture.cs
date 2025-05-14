namespace AtataUITestsDeletes.Tests
{
    [Parallelizable(ParallelScope.Self)]
    public class UITestFixture
    {
        [SetUp]
        public void SetUp() =>
            AtataContext.GlobalConfiguration.Build();

        [TearDown]
        public void TearDown() =>
            AtataContext.Current?.Dispose();
    }
}