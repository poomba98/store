using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Linq;

namespace apisite.Controllers
{
    public class BaseController : ApiController
    {
        public List<product> GetBase()
        {
            dbwork obj = new dbwork();
            List<product> l = obj.getdb();
            return l;
        }
        public void Get(int id)
        {
            dbwork obj = new dbwork();
            obj.del(id);
        }
    }
    public class ValuesController : ApiController
    {
   


        // GET api/values
        [HttpGet]
        //[Route("api/values/getAll/")]
        public IEnumerable<product> GetAll()
        {
            DataContext store = new DataContext(@"Data Source=localhost\SQLEXPRESS01;database=store;Integrated Security = True;");
            Table<Instruments> instruments = store.GetTable<Instruments>();
            dbwork obj = new dbwork();
            IEnumerable<product> list= obj.getdb();
            return list;
        }

        // GET api/values/5
        [HttpPut]
        public void Del(int id)
        {
            DataContext store = new DataContext(@"Data Source=localhost\SQLEXPRESS01;database=store;Integrated Security = True;");
            Table<Instruments> instruments = store.GetTable<Instruments>();
            dbwork obj = new dbwork();
            obj.del(id);
        }

        // POST api/values
        public void Add([FromBody]Instruments product)
        {
            DataContext store = new DataContext(@"Data Source=localhost\SQLEXPRESS01;database=store;Integrated Security = True;");
            Table<Instruments> instruments = store.GetTable<Instruments>();
            instruments.InsertOnSubmit(product);
            store.SubmitChanges();
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
