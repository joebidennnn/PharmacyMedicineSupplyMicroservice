using Newtonsoft.Json;
using PharmacyMedicineSupplyMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyMicroservice.Services
{
    public class MedicineStockService:IMedicineStockService
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineStockService));
        public async Task<int> GetStock(string medicineName)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://medicinestock.azurewebsites.net/api/MedicineStockInformation")
            };
            var response = await client.GetAsync("MedicineStockInformation");
            if (!response.IsSuccessStatusCode)
            {
                return -1;
            }
            _log4net.Info("Fetched Medicine Stock information from Medicine Stock Microservice");
            string stringStock = await response.Content.ReadAsStringAsync();
            var medicines = JsonConvert.DeserializeObject<List<MedicineStock>>(stringStock);
            var i = medicines.Where(x => x.Name == medicineName).FirstOrDefault();
            return i.NumberOfTabletsInStock;
        }
    }
}
