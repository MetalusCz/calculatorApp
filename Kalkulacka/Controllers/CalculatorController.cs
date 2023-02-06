using Kalkulacka.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using org.matheval;
using Expression = org.matheval.Expression;
using Kalkulacka.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kalkulacka.Controllers {
    public class CalculatorsController : Controller {
        private readonly ApplicationDbContext _context;

        public CalculatorsController(ApplicationDbContext context) {
            _context = context;
        }
        public Calculator Calculation(string input) {
            Calculator result = new Calculator();
            if (input != null) {
                Object value = new Expression(input).Eval();
                result.Input = $"{input} = {value}";
            }
            return result;
        }

        public async Task<IActionResult> Index() {
            return View(await _context.Results.ToListAsync());
        }
        public async Task<IActionResult> Calculate() {
            ViewBag.History = (await _context.Results.ToListAsync()).TakeLast(10);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Input")] Calculator calculator) {
            if (ModelState.IsValid) {
                var result = Calculation(calculator.Input);
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Calculate));
            }
            return View("Calculate");
        }

    }
}

