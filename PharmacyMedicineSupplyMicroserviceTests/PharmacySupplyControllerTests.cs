using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PharmacyMedicineSupplyMicroservice.Controllers;
using PharmacyMedicineSupplyMicroservice.Models;
using PharmacyMedicineSupplyMicroservice.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyTest
{
    class PharmacyMedicineSupplyControllerTest
    {
        List<PharmacyMedicineSupply> supplyList;
        Mock<IPharmacySupplyService> providerRepo;
        List<MedicineDemand> demand, emptyDemand, wrongDemand;
        [SetUp]
        public void Setup()
        {
            emptyDemand = new List<MedicineDemand>();
            demand = new List<MedicineDemand>() {
                new MedicineDemand{Medicine="Gaviscon",DemandCount=18 }
            };
            wrongDemand = new List<MedicineDemand>() {
                new MedicineDemand{Medicine="Medicine8",DemandCount=15 }
            };
            supplyList = new List<PharmacyMedicineSupply>()
            {
                new PharmacyMedicineSupply{ PharmacyName="Life Pharmacy",MedicineName="Gaviscon",SupplyCount=20},

            };
            providerRepo = new Mock<IPharmacySupplyService>();
        }
        [Test]
        public void GetPharmacySupply_ValidData_ReturnOkResult()
        {
            providerRepo.Setup(m => m.GetSupply(demand)).Returns(Task.FromResult(supplyList));
            var pro = new PharmacySupplyController(providerRepo.Object);
            var res = pro.GetPharmacySupply(demand).Result as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }
       
        [Test]
        public void PharmacySupply_InValidData_ReturnsBadRequest()
        {
            providerRepo.Setup(m => m.GetSupply(wrongDemand)).Returns(Task.FromResult(new List<PharmacyMedicineSupply>()));
            var pro = new PharmacySupplyController(providerRepo.Object);
            var res = pro.GetPharmacySupply(wrongDemand).Result as ObjectResult;
            Assert.AreEqual(res.StatusCode, 404);
        }

        [Test]
        public void GetPharmacySupply_ValidData_NoContent()
        {
            providerRepo.Setup(m => m.GetSupply(emptyDemand)).Returns(Task.FromResult(supplyList));
            var pro = new PharmacySupplyController(providerRepo.Object);
            var res = pro.GetPharmacySupply(emptyDemand).Result as ObjectResult;
            Assert.AreEqual(res.StatusCode, 404);
        }


    }
}
