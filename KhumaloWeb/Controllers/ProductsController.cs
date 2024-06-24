using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KhumaloWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhumaloWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly KhumaloContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(KhumaloContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Products
        public async Task<IActionResult> Index(string category)
        {
            var categories = await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            var products = string.IsNullOrEmpty(category)
                ? await _context.Products.Include(p => p.User).ToListAsync()
                : await _context.Products
                    .Where(p => p.Category == category)
                    .Include(p => p.User)
                    .ToListAsync();

            // Log the retrieved products
            foreach (var product in products)
            {
                _logger.LogInformation("Product: {ProductName}, Price: {Price}", product.ProductName, product.Price);
            }

            var viewModel = new ProductSearchViewModel
            {
                Products = products,
                Categories = categories,
                SelectedCategory = category
            };

            return View(viewModel);
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name.Contains("@admin.co.za")) { 
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }
       
            else{
                return Forbid();
             }
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,UserId,ProductName,Image,Description,Price,Category,Availability")] Product product)
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name.Contains("@admin.co.za"))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", product.UserId);
                return View(product);
            }
            else
            {
                return Forbid();
            }
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated && User.Identity.Name.Contains("@admin.co.za"))
            {
                ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", product.UserId);
                return View(product);
            }
            else
            {
                return Forbid();
            }
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,UserId,ProductName,Image,Description,Price,Category,Availability")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated && User.Identity.Name.Contains("@admin.co.za"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.ProductId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", product.UserId);
                return View(product);
            }
            else
            {
                return Forbid();
            }
        }

        // POST: Products/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (User.Identity.IsAuthenticated && User.Identity.Name.Contains("@admin.co.za"))
            {
                if (product != null)
                {
                    // Delete related transactions first
                    var transactions = _context.Transactions.Where(t => t.ProductId == id);
                    _context.Transactions.RemoveRange(transactions);

                    // Then delete the product
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Forbid();
            }
        }


        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
