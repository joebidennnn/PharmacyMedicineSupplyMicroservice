using PharmacyMedicineSupplyMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyMicroservice.Services
{
    public interface IPharmacySupplyService
    {
        public Task<List<PharmacyMedicineSupply>> GetSupply(List<MedicineDemand> medicines);
    }
}
