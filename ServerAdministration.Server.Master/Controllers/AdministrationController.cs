using Microsoft.AspNetCore.Mvc;
using ServerAdministration.Server.Master.Models;
using ServerAdministration.Server.Master.Services;
using System.Diagnostics;

namespace ServerAdministration.Server.Master.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IAdministrationService administrationService;

        public AdministrationController(IAdministrationService administrationService)
        {
            this.administrationService = administrationService;
        }

        [HttpGet("[Action]")]
        public void GetDataFromAllInsurances()
        {
            //administrationService.GetDataFromAllInsurances();
           administrationService.GetDataFromAllInsurancesAsync();

        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        // //   return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
