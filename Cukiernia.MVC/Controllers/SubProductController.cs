using AutoMapper;
using Cukiernia.Aplication.Cukiernia.Commands.CreateSubProduct;
using Cukiernia.Aplication.Cukiernia.Commands.DeleteSubProduct;
using Cukiernia.Aplication.Cukiernia.Commands.EditSubProduct;
using Cukiernia.Aplication.Cukiernia.Queries.GetAllSubProducts;
using Cukiernia.Aplication.Cukiernia.Queries.GetSubProductByEncodedName;
using Cukiernia.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cukiernia.MVC.Controllers
{
    public class SubProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubProductController(IMediator mediator, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSubProduct(CreateSubProductCommand command)
        {

            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            this.SetNotification("success", $"Utworzono nowy składnik: {command.Name}");
            return RedirectToAction(nameof(SubProductBase));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SubProductBase()
        {
            var subProducts = await _mediator.Send(new GetAllSubProductsQuery());
            return View(subProducts);
        }


        [HttpGet]
        [Authorize]
        [Route("SubProductBase/SubProductBaseEdit/{encodedName}")]
        public async Task<IActionResult> SubProductBaseEdit(string encodedName)
        {
            var subProducts = await _mediator.Send(new GetAllSubProductsQuery());
            return View(subProducts);
        }

        [HttpPost]
        [Authorize]
        [Route("SubProductBase/SubProductBaseEdit/{encodedName}")]
        public async Task<IActionResult> EditSubProduct(EditSubProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            this.SetNotification("success", $"Zaktualizowano składnik: {command.Name}");
            return RedirectToAction(nameof(SubProductBase));
        }

        [HttpGet]
        [Authorize]
        [Route("SubProductBase/{encodedName}/Delete")]
        public async Task<IActionResult> DeleteSubProduct(string encodedName)
        {
            var dto = await _mediator.Send(new GetSubProductByEncodedNameQuery(encodedName));

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _mediator.Send(new DeleteSubProductCommand() { EncodedName = encodedName });
            this.SetNotification("success", $"Składnik {dto.Name} został usunięty");
            return RedirectToAction(nameof(SubProductBase));
        }
    }
}
