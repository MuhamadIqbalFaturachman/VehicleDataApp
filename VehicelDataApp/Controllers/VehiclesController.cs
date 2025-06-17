using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicelDataApp.Data;
using VehicelDataApp.Models;

namespace VehicelDataApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehicleContext _context;

        public VehiclesController(VehicleContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index(string noReg, string namaPemilik)
        {
            var vehicles = from v in _context.Vehicles
                           select v;

            if (!string.IsNullOrEmpty(noReg))
            {
                vehicles = vehicles.Where(v => v.NomorRegistrasi.Contains(noReg));
            }

            if (!string.IsNullOrEmpty(namaPemilik))
            {
                vehicles = vehicles.Where(v => v.NamaPemilik.Contains(namaPemilik));
            }

            return View(await vehicles.ToListAsync());
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomorRegistrasi,NamaPemilik,Alamat,MerkKendaraan,TahunPembuatan,KapasitasSilinder,WarnaKendaraan,BahanBakar")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Data kendaraan berhasil ditambahkan!" });
            }
            return BadRequest(ModelState);
        }

        // GET: Vehicles/Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();

            return View(vehicle);
        }

        // POST: Vehicles/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NomorRegistrasi,NamaPemilik,Alamat,MerkKendaraan,TahunPembuatan,KapasitasSilinder,WarnaKendaraan,BahanBakar")] Vehicle vehicle)
        {
            if (id != vehicle.NomorRegistrasi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.NomorRegistrasi))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(new { message = "Data kendaraan berhasil diubah!" });
            }

            return BadRequest(new { message = "Validasi gagal!" });
        }

        // GET: Vehicles/Details
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kendaraan = _context.Vehicles.FirstOrDefault(v => v.NomorRegistrasi == id);

            if (kendaraan == null)
            {
                return NotFound();
            }

            return View(kendaraan);
        }

        // POST: Vehicles/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Data kendaraan berhasil dihapus!" });
        }

        private bool VehicleExists(string id)
        {
            return _context.Vehicles.Any(e => e.NomorRegistrasi == id);
        }
    }
}