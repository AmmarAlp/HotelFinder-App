using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var hotels = _hotelService.GetAllHotels();
            return Ok(hotels); // 200 + data
        }


        /// <summary>
        /// Get Hotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")] //api/hotels/gethotelbyid/2
        public IActionResult GetHotelById(int id)
        {
            var hotel = _hotelService.GetHotelById(id);

            if (hotel!= null)
            {
                return Ok(hotel); //200 + data
            }
            return NotFound(); //404
        }

        [HttpGet]
        [Route("[action]/{Name}")]
        public IActionResult GetHotelByName(string name)
        {
            var hotel = _hotelService.GetHotelByName(name);

            if (hotel !=null)
            {
                return Ok(hotel); //200 + data
            }
            return NotFound(); //404
        }

        /// <summary>
        /// Create An Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            // if (ModelState.IsValid) "[ApiController]" validation işlemlerini kendi otamatik kontrol ediyor
            //{                                        data geçerli değilse bu actiona girmiyor bile
            var createdHotel = _hotelService.CreateHotel(hotel);
            return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel); //201 + data
            //}
            //return BadRequest(ModelState); //404 + validation errors
        }

        /// <summary>
        /// Update the Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateHotel([FromBody] Hotel hotel)
        {
            if (_hotelService.GetHotelById(hotel.Id)!=null)
            {
                return Ok(_hotelService.UpdateHotel(hotel)); //200 + data
            }
            
            return NotFound();
        }


        /// <summary>
        /// Delete The Hotel
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult DeleteHotel(int id)
        {
            if (_hotelService.GetHotelById(id) != null)
            {
                _hotelService.DeleteHotel(id);
                return Ok(); //200
            }

            return NotFound();
            
        }
    }
}
