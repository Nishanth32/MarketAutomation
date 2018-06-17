using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Client;
using System.Configuration;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk.Query;
using TxtMobizApp.Models;

namespace TxtMobizApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult MobizHome()
        {

            //Get UserName and Password from CRM using Organization ID

            IOrganizationService _service = GetOrgServiceInstance();

            QueryExpression queExp = new QueryExpression("account");
            queExp.ColumnSet = new ColumnSet("name", "emailaddress1","accountnumber","websiteurl","address1_composite","revenue");
            queExp.Distinct = true;
            
            AccountViewModel accountViewModel = new AccountViewModel();
            accountViewModel.Accounts = _service.RetrieveMultiple(queExp).Entities.Select(CastEntityToModel).ToList();

            accountViewModel.Parameters = Request.QueryString.AllKeys.Select(x =>x+" - "+ Request.QueryString[x]).ToList();
            
            return View(accountViewModel);
        }


        public AccountModel CastEntityToModel(Entity entity)
        {
            AccountModel acc = new AccountModel();
            acc.Name = entity.GetAttributeValue<string>("name");
            acc.AccountNumber = entity.GetAttributeValue<string>("accountnumber");
            acc.EmailAddress = entity.GetAttributeValue<string>("emailaddress1");
            acc.Address_Composite = entity.GetAttributeValue<string>("address1_composite");
            acc.WebsiteUrl = entity.GetAttributeValue<string>("websiteurl");
            acc.Revenue = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("revenue").Value;
            return acc;
        }

        public static IOrganizationService GetOrgServiceInstance()
        {
            try
            {
                CrmConnection connection = CrmConnection.Parse(ConfigurationManager.ConnectionStrings["mobizConnection"].ConnectionString);
                connection.Timeout = new System.TimeSpan(10, 5, 0);
                return new OrganizationService(connection);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}