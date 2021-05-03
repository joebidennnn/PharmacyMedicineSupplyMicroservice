using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupplyMicroservice.Models;
using PharmacyMedicineSupplyMicroservice.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PharmacySupplyController : ControllerBase
    {
        private readonly IPharmacySupplyService _supplyService;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PharmacySupplyController));

        public PharmacySupplyController(IPharmacySupplyService service)
        {
            _supplyService = service;
        }

        [HttpPost]
        public async Task<IActionResult> GetPharmacySupply(List<MedicineDemand> m)
        {
            try
            {
                var pharmacyMedicineSupply = await _supplyService.GetSupply(m);
                if (pharmacyMedicineSupply.Count > 0)
                {
                    _log4net.Info("Pharmacy supply details successfully got and sent.");
                    return Ok(pharmacyMedicineSupply);
                }

                _log4net.Info("No details found to supply to pharmacies");
                return NotFound("No such details found please try again.");
            }
            catch (Exception exception)
            {
                _log4net.Error("Excpetion:" + exception.Message + " has found while trying to get supply info.");
                return StatusCode(500);
            }
        }
    }
}
