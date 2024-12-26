using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
//using Loai = Api.Models.Loai;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private readonly MyDbContext _context;
        public LoaisController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var dsLoai = _context.Loais.ToList();
                return Ok(dsLoai);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult getById(int id)
        {
            var loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == id);
            if (loai == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(loai);
            }
        }
        [HttpPost]
        public IActionResult CreateNew(LoaiModel model)
        {
            try
            {
                var loai = new Loai
                {
                    TenLoai = model.TenLoai
                };
                _context.Add(loai);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, loai);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, LoaiModel model)
        {
            var loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == id);
            if (loai == null)
            {
                return NotFound();
            }
            else
            {
                loai.TenLoai = model.TenLoai;
                _context.SaveChanges();
                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLoaiById(int id)
        {
            var loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == id);
            if (loai == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(loai);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
        }



    }
}
