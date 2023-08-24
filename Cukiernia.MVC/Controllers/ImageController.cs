using AutoMapper;
using Cukiernia.Aplication.Cukiernia.Commands.DeleteImage;
using Cukiernia.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cukiernia.MVC.Controllers
{
    public class ImageController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageController(IMediator mediator, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Authorize]
        [Route("Image/{encodedName}/DeleteImage/{imageId}")]
        public async Task<IActionResult> DeleteImage(string encodedName, int imageId)
        {
            await _mediator.Send(new DeleteImageCommand() { Id = imageId });
            this.SetNotification("success", $"Zdjęcie zostało usunięte");
            return RedirectToAction("Edit", "Baking", new { encodedName = encodedName });
        }
    }
}
