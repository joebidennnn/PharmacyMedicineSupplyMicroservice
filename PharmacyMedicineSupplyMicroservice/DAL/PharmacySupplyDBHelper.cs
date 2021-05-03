using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupplyMicroservice.Models;

namespace PharmacyMedicineSupplyMicroservice.DAL
{
    public class PharmacySupplyDBHelper:DbContext
    {
        public PharmacySupplyDBHelper(DbContextOptions<PharmacySupplyDBHelper> options):base(options)
        {

        }

        public DbSet<Pharmacy> Pharmacies { get; set; }
    }
}
