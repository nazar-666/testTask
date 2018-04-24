using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using PagedList;
using Place.Core.Consts;
using Place.Core.Data.Entites;
using Place.Services.Services.EntityServices;
using Place.Services.Services.Factory;
using Place.Web.Controllers.Abstract;
using Place.Core.Models;

namespace Place.Web.Controllers
{
    public class HomeController : GeneralController
    {
        private static readonly Random Random = new Random();

        private readonly ICustomerService _customerService;
        public HomeController(IServiceManager serviceManager) : base(serviceManager)
        {
            _customerService = ServiceManager.CustomerService;
        }

        [Authorize]
        public ActionResult Index(int? countOfCustomers, int page = 1)
        {
            if (Request.IsAjaxRequest())
            {
                if(countOfCustomers < 0 || countOfCustomers > Consts.MaxCustomersCount)
                    throw new ArgumentOutOfRangeException("Illegal count of customers, value should be higher than zero and not higher that " + Consts.MaxCustomersCount);

                var insertionPagesCount = (int)(countOfCustomers / Consts.MaxRecordsPerOneInsertionCount);
                if (insertionPagesCount == 0)
                    CreateListOfCustomers(countOfCustomers.Value);
                else
                {
                    for (var i = 1; i <= insertionPagesCount; i++)
                    {
                        CreateListOfCustomers(Consts.MaxRecordsPerOneInsertionCount);
                    }
                    var countOfCustomersLeftToInsert = countOfCustomers.Value -
                                                       insertionPagesCount * Consts.MaxRecordsPerOneInsertionCount;
                    if (countOfCustomersLeftToInsert > 0)
                        CreateListOfCustomers(countOfCustomersLeftToInsert);
                }

                var customers = _customerService.GetCustomers();
                if (customers.Any())
                    return PartialView("_CustomersList", customers.ToPagedList(page, Consts.ShowRecordsPerPage));

            }
            else if (_customerService.GetCustomers().Any())
            {
                var customers = _customerService.GetCustomers();
                return View(customers.ToPagedList(page, Consts.ShowRecordsPerPage));
            }

            return View(new List<CustomerViewModel>().ToPagedList(page, Consts.ShowRecordsPerPage));
        }

        [Authorize]
        public RedirectResult ExportExcel()
        {
            var customers = _customerService.GetCustomers();

            if (!customers.Any()) return Redirect(@Url.Action("Index", "Home"));

            var dataTable = new DataTable();

            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Last Name");
            dataTable.Columns.Add("Email");
            dataTable.Columns.Add("Count Of Tasks");
            dataTable.Columns.Add("Duration");
            dataTable.Columns.Add("Executed Tasks");
            dataTable.Columns.Add("% Of Executed Tasks");
            dataTable.Columns.Add("Duration Of Tasks");

            var customerIds = new List<int>();
            foreach (var customer in customers)
            {
                var row = dataTable.NewRow();

                row["Name"] = customer.FirstName;
                row["Last Name"] = customer.LastName;
                row["Email"] = customer.Email;
                row["Count Of Tasks"] = customer.CountOfTasks;
                row["Duration"] = customer.DurationHours;
                row["Executed Tasks"] = customer.ExecutedTasks;
                row["% Of Executed Tasks"] = customer.PercentOfExecutedTasks.ToString("0.00");
                row["Duration Of Tasks"] = customer.DurationOfExecutedTasks.ToString("0.00");

                dataTable.Rows.Add(row);

                customerIds.Add(customer.Id);
            }

            foreach (var customerId in customerIds)
            {
                _customerService.DeleteById(customerId);
            }

            var grid = new GridView {DataSource = dataTable};
            grid.DataBind();

            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename=Customers.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            var sw = new StringWriter();
            var htmlTextWriter = new HtmlTextWriter(sw);

            grid.RenderControl(htmlTextWriter);

            Response.Output.Write(sw.ToString());
            Response.End();

            return Redirect(@Url.Action("Index", "Home"));
        }

        private void CreateListOfCustomers(int customersCount)
        {
            var listOfCustomers = new List<Customer>();
            for (var i = 0; i < customersCount; i++)
            {
                var countOfTasks = Random.Next(100);
                var customer = new Customer
                {
                    FirstName = RandomStringGenerator(Random.Next(2, 10)),
                    LastName = RandomStringGenerator(Random.Next(2, 10)),
                    Email = RandomStringGenerator(Random.Next(2, 10)) + "@" + RandomStringGenerator(Random.Next(2, 10)) + "." + RandomStringGenerator(Random.Next(2, 3)),
                    CountOfTasks = countOfTasks,
                    DurationHours = Random.Next(100),
                    ExecutedTasks = Random.Next(countOfTasks)
                };
                listOfCustomers.Add(customer);
            }
            _customerService.AddRange(listOfCustomers);

        }

        private static string RandomStringGenerator(int stringLength)
        {
            return new string(Enumerable.Repeat(Consts.Alphabet, stringLength).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}