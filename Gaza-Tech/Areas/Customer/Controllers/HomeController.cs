using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.BLL.Repositories;
using Gaza_Tech.BLL.Specifications;
using Gaza_Tech.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Gaza_Tech.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using X.PagedList;
using X.PagedList.Extensions;
using Gaza_Tech.Utilities;

namespace Gaza_Tech.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        
        public  IActionResult Index()
        {

            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
