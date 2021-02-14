using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _rentalService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("rentaldetails")]
        public IActionResult RentalDetail()
        {
            var result = _rentalService.GetRentCarDetail();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("rentacar")]
        public IActionResult RentACar(int carId,int userId)
        {
            var result = _rentalService.RentACar(carId, userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        public IActionResult ReturnCar(int rentId)
        {
            var result = _rentalService.ReturnCar(rentId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
