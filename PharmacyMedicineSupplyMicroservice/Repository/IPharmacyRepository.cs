using PharmacyMedicineSupplyMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyMicroservice.Repository
{
   public interface IPharmacyRepository
    {
        public List<Pharmacy> GetPharmacies();

    }
}
