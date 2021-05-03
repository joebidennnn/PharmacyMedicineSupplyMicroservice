using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyMicroservice.Models
{
    public class Pharmacy
    {
        [Key]
        public string pharmacyName { get; set; }
    }
}
