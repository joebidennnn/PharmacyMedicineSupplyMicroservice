using PharmacyMedicineSupplyMicroservice.Models;
using PharmacyMedicineSupplyMicroservice.DAL;
using System.Collections.Generic;
using System.Linq;

namespace PharmacyMedicineSupplyMicroservice.Repository
{
    public class PharmacyRepository:IPharmacyRepository
    {
        private readonly PharmacySupplyDBHelper _helper;
        private List<Pharmacy> pharmacyNames;

        public PharmacyRepository(PharmacySupplyDBHelper helper)
        {
            _helper = helper;

            if(!_helper.Pharmacies.Any())
            {
                _helper.Pharmacies.AddRange(
                    new Pharmacy() { pharmacyName = "City Drug" },
                    new Pharmacy() { pharmacyName = "Health Easy" },
                    new Pharmacy() { pharmacyName = "Life Pharmacy" },
                    new Pharmacy() { pharmacyName = "MedImpact store" }
                    );
                _helper.SaveChanges();
            }
            
        }

        
        private readonly List<Pharmacy> pharmacies = new List<Pharmacy>();
        public List<Pharmacy> GetPharmacies()
        {
            pharmacyNames = _helper.Pharmacies.ToList();
            foreach (var i in pharmacyNames)
            {
                pharmacies.Add(new Pharmacy { pharmacyName = i.pharmacyName });
            }
            return pharmacies;
        }
    }
}
