using MockDemo;
using Moq;
using ProductMoqTask1;
namespace MoqDemo.Test
{
    [TestFixture]
    public class Tests
    {
        #region TESTCASE MockDemo
        ATMCashWithdrawal atmCash;
        [SetUp]
        public void Setup()
        {
            var hsmModuleMock = new Mock<IHSMModule>();
            hsmModuleMock.Setup(h => h.ValidatePIN("123456781234", 1234)).Returns(true);
            var hostBankMock = new Mock<IHostBank>();
            hostBankMock.Setup(h => h.AuthenticateAmount("123456781234", 500)).Returns(true);
            atmCash = new ATMCashWithdrawal(hsmModuleMock.Object, hostBankMock.Object);
        }

        [TestCase]
        public void WithdrawAmount_ValidTransaction_ReturnsTrue()
        {
            bool result = atmCash.WithdrawAmount("123456781234", 1234, 500);
            //Assert
            Assert.IsTrue(result);
        }
        #endregion

        #region TESTCASE ProductMoqTask1
        ProductService productServiceGet;
        [SetUp]
        public void SetupforDiscount()
        {
            var productModuleMock = new Mock<IProductRepository>();
            productModuleMock.Setup(p => p.GetPrice(It.IsAny<int>())).Returns(100m);
            productServiceGet = new ProductService(productModuleMock.Object);
        }
        [Test]
        public void CalculatedDiscountPrice_WhenCalled_ReturnsCorrectdiscountPrice()
        {            
            decimal discountPercentage = 10m;
            var expectedPrice = 90m;
            var result = productServiceGet.CalculatedDiscountPrice(1, discountPercentage);
            //Assert
            Assert.AreEqual(expectedPrice, result);
        }

        ProductService productServiceEx;
        [SetUp]
        public void SetupforException()
        {
            var productModuleMock = new Mock<IProductRepository>();
            productModuleMock.Setup(p => p.GetPrice(It.IsAny<int>())).Returns(0m);
            productServiceEx = new ProductService(productModuleMock.Object);
        }
        [Test]
        public void CalculatedDiscountPrice_WheProductDoesNotExist_ThrowsException()
        {
            
            var ex = Assert.Throws<ArgumentException>(() => productServiceEx.CalculatedDiscountPrice(999, 10m));
            Assert.That(ex.Message, Is.EqualTo("Product not found"));
        }
        #endregion
    }
}