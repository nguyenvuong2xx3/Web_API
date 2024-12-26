using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly ILoaiRepository _loaiRepository;
        public LoaiController(ILoaiRepository loaiRepository)
        {
            _loaiRepository = loaiRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_loaiRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _loaiRepository.GetById(id);
                if (data == null) 
                {
                    return NotFound();
                }
                else
                {
                return Ok(_loaiRepository.GetById(id));
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, LoaiVM loai)
        {
            if(id != loai.MaLoai)
            {
                return BadRequest();
            }
            else 
            {
                try
                {
                    _loaiRepository.Update(loai);
                    return NoContent();
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _loaiRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult Add(LoaiModel loai)
        {
            try
            {
                return Ok(_loaiRepository.Add(loai));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

    }
}
