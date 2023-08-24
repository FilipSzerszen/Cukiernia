using AutoMapper;
using Cukiernia.Aplication.Cukiernia.Commands.CreateBaking;
using Cukiernia.Aplication.Cukiernia.Commands.DeleteBacking;
using Cukiernia.Aplication.Cukiernia.Commands.EditBaking;
using Cukiernia.Aplication.Cukiernia.Queries.GetAllBakings;
using Cukiernia.Aplication.Cukiernia.Queries.GetBakingByEncodedName;
using Cukiernia.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cukiernia.MVC.Controllers
{
    public class BakingController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BakingController(IMediator mediator, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }



        [Route("Baking/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetBakingByEncodedNameQuery(encodedName));
            return View(dto);
        }

        [HttpGet]
        [Authorize]
        [Route("Baking/{encodedName}/Delete")]
        public async Task<IActionResult> Delete(string encodedName)
        {
            var dto = await _mediator.Send(new GetBakingByEncodedNameQuery(encodedName));

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            return View(dto);
        }


        [HttpPost]
        [Authorize]
        [Route("Baking/{encodedName}/Delete")]
        public async Task<IActionResult> Delete(string encodedName, DeleteBakingCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(new DeleteBakingCommand() { EncodedName = encodedName });
            this.SetNotification("success", $"Pomyślnie usunięto wypiek: {command.Name}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        [Route("Baking/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetBakingByEncodedNameQuery(encodedName));

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            EditBakingCommand model = _mapper.Map<EditBakingCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [Route("Baking/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(List<IFormFile> Images, string encodedName, EditBakingCommand command)
        {
            string imageName;
            string imageExtension;
            List<string> uploadedFiles = new List<string>();

            foreach (IFormFile urlItem in Images)
            {
                imageExtension = Path.GetExtension(urlItem.FileName);
                imageName = Guid.NewGuid().ToString() + imageExtension;

                if (imageExtension == ".jpg" || imageExtension == ".gif")
                {
                    var uploadImage = Path.Combine(_webHostEnvironment.WebRootPath, "Fotos", imageName);
                    using (FileStream stream = new FileStream(uploadImage, FileMode.Create))
                    {
                        await urlItem.CopyToAsync(stream);
                        uploadedFiles.Add(Path.Combine(imageName, imageExtension));
                    }
                    command.Images.Add(new Domain.Entities.ImageUrl { BakingId = command.Id, Url = imageName });
                }
            }

            if (!ModelState.IsValid)
            {
                var dto = await _mediator.Send(new GetBakingByEncodedNameQuery(encodedName));
                EditBakingCommand model = _mapper.Map<EditBakingCommand>(dto);
                return View(model);
            }
            await _mediator.Send(command);

            this.SetNotification("success", $"Pomyślnie zmodyfikowano wypiek: {command.Name}");

            return RedirectToAction(nameof(Edit));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bakings = await _mediator.Send(new GetAllBakingsQuery());
            return View(bakings);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(List<IFormFile> Images, CreateBakingCommand command)
        {
            string imageName;
            string imageExtension;
            List<string> uploadedFiles = new List<string>();

            foreach (IFormFile urlItem in Images)
            {
                imageExtension = Path.GetExtension(urlItem.FileName);
                imageName = Guid.NewGuid().ToString() + imageExtension;

                if (imageExtension == ".jpg" || imageExtension == ".gif")
                {
                    var uploadImage = Path.Combine(_webHostEnvironment.WebRootPath, "Fotos", imageName);
                    using (FileStream stream = new FileStream(uploadImage, FileMode.Create))
                    {
                        await urlItem.CopyToAsync(stream);
                        uploadedFiles.Add(Path.Combine(imageName, imageExtension));
                    }
                    command.Images.Add(new Domain.Entities.ImageUrl { BakingId = command.Id, Url = imageName });
                }
            }

            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            this.SetNotification("success", $"Utworzono nowy wypiek: {command.Name}");
            command.EncodeName();
            return RedirectToAction("Edit", "Baking", new { encodedName = command.EncodedName});
        }
    }
}
