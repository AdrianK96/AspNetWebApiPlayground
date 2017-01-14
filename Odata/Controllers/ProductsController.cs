using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Odata.Models;

namespace Odata.Controllers
{
    public class ProductsController : ODataController
    {
        ProductsContext db = new ProductsContext();

        private bool ProductExists(int key)
        {
            return db.Products.Any(p => p.Id == key);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        // https://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
    }
}