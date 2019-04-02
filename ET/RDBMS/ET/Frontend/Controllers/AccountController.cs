using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class AccountController : Controller
    {
        private ETContext _context;

        public AccountController(ETContext context)
        {
            this._context = context;

        }

        [HttpGet]
        [Route("account/{accountNum}")]
        public IActionResult Index(int accountNum)
        {
            if (accountNum != (int)HttpContext.Session.GetInt32("UserId"))
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Register");
            }
            List<Transaction> MyTransactions = _context.Transactions.Where(transaction => transaction.UserId == accountNum).OrderByDescending(transaction => transaction.CreatedAt).ToList();
            ViewBag.MyTransactions = MyTransactions;
            Console.WriteLine(ViewBag.MyTransactions.Count);
            ViewBag.Balance = 0;
            foreach (Transaction trans in MyTransactions)
            {
                ViewBag.Balance += trans.Amount;
            }
            if (ViewBag.Balance <= 0)
            {
                ViewBag.Minimum = 0;
            }
            else
            {
                ViewBag.Minimum = 0 - ViewBag.Balance;
            }
            ViewBag.Accountholder = _context.Users.SingleOrDefault(user => user.UserId == accountNum).FirstName;
            return View();
        }

        [HttpPost]
        [Route("Transact")]
        public IActionResult Transact(double Amount)
        {
            Console.WriteLine($"Kwota podana w formularzu wynosi: {Amount}PLN.");
            User Transactor = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            double balance = 0;

            List<Transaction> MyTransactions = _context.Transactions.Where(transaction => transaction.UserId == Transactor.UserId).ToList();

            foreach (Transaction trans in MyTransactions)
            {
                balance += (double)trans.Amount;
            }

            if (balance + Amount >= 0 && Transactor != null)
            {
                // startujemy nową transakcję
                Transaction NewTransaction = new Transaction()
                { Amount = (double) Amount,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = Transactor.UserId
                };

                _context.Transactions.Add(NewTransaction);
                _context.SaveChanges();

                return RedirectToAction("Index", new { accountNum = HttpContext.Session.GetInt32("UserId") });
            }
            else
            {
                // zwraca info o błędzie
                return View("Index");
            }
        }
    }
}