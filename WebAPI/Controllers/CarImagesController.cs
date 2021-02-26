using Business.Abstract;
using Core.Utilities;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
        IWebHostEnvironment _webHostEnvironment;
        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnvironment)
        {
            _carImageService = carImageService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add( int carId,[FromForm] FileUpload objectFile)
        {
            string photoName = string.Empty;
            string photoExtension = string.Empty;

            
            if (objectFile.files.Length > 0)
            {
                photoExtension = Path.GetExtension(objectFile.files.FileName);
                if (photoExtension.ToLower()==".jpg" || photoExtension.ToLower()==".png")
                {
                    photoName = Guid.NewGuid() + photoExtension;

                    string path = _webHostEnvironment.WebRootPath + "\\images\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fileStream = System.IO.File.Create(path + photoName))
                    {
                        objectFile.files.CopyTo(fileStream);
                        fileStream.Flush();

                    }
                  
                }
             
       
            }

            var result = _carImageService.UploadImage(carId,photoName);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);


        }



        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _carImageService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carImageService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
    }
}
