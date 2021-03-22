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
        [HttpGet]
        [Route("api/base/search/{id}/{i}/{page}")]
        public List<product> search(string id,int i,int page)
        {
            dbwork obj = new dbwork();
            List<product> l = obj.search(id,i,page);
            return l;
        }
        [HttpGet]
        [Route("api/base/fill/{id}")]
        public List<string> fill(int id)
        {
            dbwork obj = new dbwork();
            List<string> l = obj.fill(id);
            return l;
        }
        [HttpPost]
        [Route("api/base/up/")]
        public void upd([FromBody]product id)
        {
            dbwork obj = new dbwork();
            obj.upd(id);
        }
        [HttpPut]
        [Route("api/base/del/{id}")]
        public void Del(int id)
        {
            DataContext store = new DataContext(@"Data Source=localhost\SQLEXPRESS01;database=store;Integrated Security = True;");
            Table<Instruments> instruments = store.GetTable<Instruments>();
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
            int i = 0;
            IEnumerable<product> list= obj.getdb(i);
            return list;
        }

        // GET api/values/5


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
