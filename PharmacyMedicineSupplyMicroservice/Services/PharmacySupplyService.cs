using PharmacyMedicineSupplyMicroservice.Models;
using PharmacyMedicineSupplyMicroservice.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyMicroservice.Services
{
    public class PharmacySupplyService : IPharmacySupplyService
    {

        private readonly IPharmacyRepository _pharmacyRepository;
        private List<Pharmacy> pharmacyList;
        private readonly IMedicineStockService medicineStockService;
        private readonly List<PharmacyMedicineSupply> pharmacySupply = new List<PharmacyMedicineSupply>();
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PharmacySupplyService));

        public PharmacySupplyService(IPharmacyRepository repository, IMedicineStockService service)
        {
            _pharmacyRepository = repository;
            medicineStockService = service;
        }
        public async Task<List<PharmacyMedicineSupply>> GetSupply(List<MedicineDemand> medicines)
        {
            pharmacyList = _pharmacyRepository.GetPharmacies();
            _log4net.Info("Retrived Pharmacies Data");
            foreach (var medicine in medicines)
            {
                if (medicine.DemandCount > 0)
                {
                    int stockCount = await medicineStockService.GetStock(medicine.Medicine);
                    if (stockCount != -1)
                    {
                        if (stockCount < medicine.DemandCount)
                            medicine.DemandCount = stockCount;
                        int indSupply = (medicine.DemandCount) / pharmacyList.Count;
                        foreach (var i in pharmacyList)
                        {
                            pharmacySupply.Add(new PharmacyMedicineSupply { MedicineName = medicine.Medicine, PharmacyName = i.pharmacyName, SupplyCount = indSupply });
                        }
                        if (medicine.DemandCount > indSupply * pharmacyList.Count)
                        {
                            pharmacySupply[pharmacySupply.Count - 1].SupplyCount += (medicine.DemandCount - indSupply * pharmacyList.Count);
                        }
                    }
                }
            }
            _log4net.Info("Demand medicines are supplied equally to all available pharmacies");
            return pharmacySupply;

        }
    }
}
