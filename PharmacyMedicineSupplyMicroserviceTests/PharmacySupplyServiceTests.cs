using Castle.Core.Internal;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PharmacyMedicineSupplyMicroservice.Models;
using PharmacyMedicineSupplyMicroservice.Services;
using PharmacyMedicineSupplyMicroservice.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyTest
{
    public class Tests
    {
        List<PharmacyMedicineSupply> pharmacySupplyList;
        Mock<IPharmacyRepository> supplyRepository;
        List<Pharmacy> pharmacies;
        List<string> stock;
        Mock<IMedicineStockService> stockMock;
        List<MedicineDemand> medicineDemand;
        [SetUp]
        public void Setup()
        {
            pharmacies = new List<Pharmacy> {
            new Pharmacy{ pharmacyName="Life Pharmacy"}
            };
            stock = new List<string>() { "Gaviscon", "Hilact" };
            medicineDemand = new List<MedicineDemand>(){
                new MedicineDemand{Medicine="Gaviscon",DemandCount=18 }
            };
            supplyRepository = new Mock<IPharmacyRepository>();
            supplyRepository.Setup(m => m.GetPharmacies()).Returns(pharmacies);
            stockMock = new Mock<IMedicineStockService>();

        }

        [Test]
        public async Task GetSupply_ValidData_ReturnOkResult()
        {
            stockMock.Setup(s => s.GetStock("Gaviscon")).Returns(Task.FromResult(10));
            var pro = new PharmacySupplyService(supplyRepository.Object, stockMock.Object);
            List<PharmacyMedicineSupply> res = await pro.GetSupply(medicineDemand);
            pharmacySupplyList = new List<PharmacyMedicineSupply>
            {
                new PharmacyMedicineSupply{ PharmacyName="Life Pharmacy",MedicineName="Gaviscon",SupplyCount=10},

            };
            Assert.AreEqual(pharmacySupplyList[0].SupplyCount, res[0].SupplyCount);
            Assert.AreEqual(pharmacySupplyList[0].MedicineName, res[0].MedicineName);
            Assert.AreEqual(pharmacySupplyList[0].PharmacyName, res[0].PharmacyName);
        }
        [Test]
        public async Task GetSupply_ValidData_NotEnoughStock()
        {
            stockMock.Setup(s => s.GetStock("Gaviscon")).Returns(Task.FromResult(10));
            var pro = new PharmacySupplyService(supplyRepository.Object, stockMock.Object);
            List<MedicineDemand> medicineDemands = new List<MedicineDemand>(){
                new MedicineDemand{Medicine="Gaviscon",DemandCount=55 }
            };
            List<PharmacyMedicineSupply> res = await pro.GetSupply(medicineDemands);
            pharmacySupplyList = new List<PharmacyMedicineSupply>
            {
                new PharmacyMedicineSupply{ PharmacyName="Life Pharmacy",MedicineName="Gaviscon",SupplyCount=10},

            };
            Assert.AreEqual(pharmacySupplyList[0].SupplyCount, res[0].SupplyCount);
            Assert.AreEqual(pharmacySupplyList[0].MedicineName, res[0].MedicineName);
            Assert.AreEqual(pharmacySupplyList[0].PharmacyName, res[0].PharmacyName);
        }
        [Test]
        public async Task GetSupply_ValidData_NoMedicine()
        {
            stockMock.Setup(s => s.GetStock("Medicine8")).Returns(Task.FromResult(-1));
            var pro = new PharmacySupplyService(supplyRepository.Object, stockMock.Object);
            List<MedicineDemand> medicineDemands = new List<MedicineDemand>(){
                new MedicineDemand{Medicine="Medicine8",DemandCount=21 }
            };
            List<PharmacyMedicineSupply> res = await pro.GetSupply(medicineDemands);

            Assert.IsTrue(res.IsNullOrEmpty());

        }
        [Test]
        public async Task GetSupply_ValidData_EnoughStockNotDivisible()
        {
            stockMock.Setup(s => s.GetStock(It.IsAny<string>())).Returns(Task.FromResult(50));
            pharmacies.Add(new Pharmacy { pharmacyName = "Health Easy" });
            var pro = new PharmacySupplyService(supplyRepository.Object, stockMock.Object);
            List<MedicineDemand> medicineDemands = new List<MedicineDemand>(){
                new MedicineDemand{Medicine="Gaviscon",DemandCount=19 }
            };
            List<PharmacyMedicineSupply> res = await pro.GetSupply(medicineDemands);
            pharmacySupplyList = new List<PharmacyMedicineSupply>
            {
                new PharmacyMedicineSupply{ PharmacyName="Life Pharmacy",MedicineName="Gaviscon",SupplyCount=9},
                new PharmacyMedicineSupply{ PharmacyName="Health Easy",MedicineName="Gaviscon",SupplyCount=10}

            };
            Assert.AreEqual(pharmacySupplyList[0].SupplyCount, res[0].SupplyCount);
            Assert.AreEqual(pharmacySupplyList[0].MedicineName, res[0].MedicineName);
            Assert.AreEqual(pharmacySupplyList[0].PharmacyName, res[0].PharmacyName);

            Assert.AreEqual(pharmacySupplyList[1].SupplyCount, res[1].SupplyCount);
            Assert.AreEqual(pharmacySupplyList[1].MedicineName, res[1].MedicineName);
            Assert.AreEqual(pharmacySupplyList[1].PharmacyName, res[1].PharmacyName);
        }
    }
}