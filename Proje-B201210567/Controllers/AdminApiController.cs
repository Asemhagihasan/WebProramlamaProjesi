using Microsoft.AspNetCore.Mvc;
using Proje_B201210567.Data;
using Proje_B201210567.Models;

namespace Proje_B201210567.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly AppDbContext _db;
            public AdminApiController(AppDbContext db)
            {
                _db = db;
            }
            // api/products
            [HttpGet]
            public List<Admin> Get()
            {
                var products = _db.Admins.ToList();
                return products;

            }
            [HttpPost]
            // api/products
            public IActionResult Post([FromBody] Admin p)
            {
                _db.Add(p);
                _db.SaveChanges();
                return Ok(p);

            }
            [HttpGet("{id}")]
            public ActionResult<Admin> Get(int? id)
            {
                if (id is null)
                {
                    return NotFound();
                }
                var Updated = _db.Admins.Find(id);
                if (Updated == null)
                {
                    return NotFound();
                }
                return Updated;
            }

            // POST api/<YazarApiController>



            // PUT api/<YazarApiController>/5
            [HttpPut("{id}")]
            public IActionResult Put(int? id, [FromBody] Admin y)
            {
                if (id is null)
                {
                    return NotFound();
                }
                var updated = _db.Admins.FirstOrDefault(z => z.Id == id);
                if (updated == null)
                {
                    return NotFound();
                }
                updated.Adi = y.Adi;
                updated.Soyad = y.Soyad;
                updated.TelefonNumarasi = y.TelefonNumarasi;
                updated.Email = y.Email; 
                updated.Sifre = y.Sifre;

                _db.Admins.Update(updated);
                _db.SaveChanges();
                return Ok(updated);
            }

            // DELETE api/<YazarApiController>/5
            [HttpDelete("{id}")]
            public IActionResult Delete(int? id)
            {
                if (id is null)
                {
                    return NotFound();
                }
                var Deleted = _db.Admins.FirstOrDefault(z => z.Id == id);
                if (Deleted == null)
                {
                    return NotFound();
                }

                _db.Admins.Remove(Deleted);
                _db.SaveChanges();
                return Ok(Deleted);
            }
        }
    }

