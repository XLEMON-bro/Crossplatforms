using CP_LAB_5.Models.ViewModels;
using CP_LAB_5_LIB;
using CP_LAB_5_LIB.LABs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CP_LAB_5.Controllers
{
    [Authorize]
    public class LabWorkController : Controller
    {
        private ILabWorker labWorker;

        public LabWorkController()
        {

        }

        public IActionResult Lab1()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Lab1(LabWorkViewModel viewModel)
        {
            labWorker = new LAB1();

            var result = await labWorker.GetOutputForLab(viewModel.Input);

            viewModel.Output = result;

            return View(viewModel);
        }

        public IActionResult Lab2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Lab2(LabWorkViewModel viewModel)
        {
            labWorker = new LAB2();

            var result = await labWorker.GetOutputForLab(viewModel.Input);

            viewModel.Output = result;

            return View(viewModel);
        }

        public IActionResult Lab3()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Lab13(LabWorkViewModel viewModel)
        {
            labWorker = new LAB3();

            var result = await labWorker.GetOutputForLab(viewModel.Input);

            viewModel.Output = result;

            return View(viewModel);
        }
    }
}
