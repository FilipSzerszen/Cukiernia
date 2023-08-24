using Cukiernia.Aplication.Cukiernia;
using Cukiernia.Aplication.Cukiernia.Commands.AddSuBProductToList;
using Cukiernia.Aplication.Cukiernia.Commands.CreateProductQuantity;
using Cukiernia.Aplication.Cukiernia.Commands.DeleteSubProductFromList;
using Cukiernia.Aplication.Cukiernia.Queries.GetAllSubProductsNotInBaking;
using Cukiernia.Aplication.Cukiernia.Queries.GetProductQuantityById;
using Cukiernia.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cukiernia.MVC.Controllers
{
    public class QuantityController : Controller
    {
        private readonly IMediator _mediator;

        public QuantityController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProductQuantity(CreateProductQuantityCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            this.SetNotification("success", $"Dodano nowe składniki do wypieku");
            return RedirectToAction("Edit", "Baking");
        }


        [HttpGet]
        [Authorize]
        [Route("Quantity/CreateSubProductsList/{encodedName}")]
        public async Task<IActionResult> CreateSubProductsList(string encodedName)
        {
            var subProducts = await _mediator.Send(new GetAllSubProductsNotInBakingQuery(encodedName));
            return PartialView("CreateSubProductsList", subProducts.ToList());
        }

        [HttpPost]
        [Authorize]
        [Route("Quantity/CreateSubProductsList/{encodedName}")]
        public async Task<IActionResult> CreateSubProductsList(List<SubProductDto> restOfSubProducts, string encodedName)
        {
            var subProductAdded = new List<SubProductDto>();
            foreach (var subProduct in restOfSubProducts)
            {
                if (subProduct.IsInBaking)
                {
                    subProductAdded.Add(subProduct);
                }
            }

            await _mediator.Send(new AddSuBProductToListCommand(subProductAdded, encodedName));
            this.SetNotification("success", $"Dodano nowe składniki do wypieku");
            return RedirectToAction("Edit", "Baking", new { encodedName = encodedName });
        }

        [HttpGet]
        [Authorize]
        [Route("Quantity/DeleteProductQuantity/{encodedName}/{id}")]
        public async Task<IActionResult> DeleteProductQuantity(int id, string encodedName)
        {
            var dto = await _mediator.Send(new GetProductQuantityByIdQuery(id));

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _mediator.Send(new DeleteSubProductFromListCommand() { Id = id });
            this.SetNotification("success", $"Składnik {dto.SubProduct.Name} został usunięty");
            return RedirectToAction("Edit", "Baking", new { encodedName = encodedName });
        }
    }
}
