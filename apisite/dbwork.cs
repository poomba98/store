using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;


namespace apisite
{
    public class dbwork
    {
        string cString = @"Data Source=localhost\SQLEXPRESS01;database=store;Integrated Security = True;";
        public List<product> getdb(int i)
        {
            DataContext store = new DataContext(cString);
            Table<Instruments> instruments = store.GetTable<Instruments>();
            Table<Instrument> instrument = store.GetTable<Instrument>();
            Table<Company> company = store.GetTable<Company>();
            List<product> database = new List<product>();
            var query = from T in instruments
                        join ts in instrument on T.Instrument equals ts.ID
                        join cm in company on T.Company equals cm.ID
                        select new
                        {
                            id = T.ID,
                            instrument = ts.Name,
                            type = T.Type,
                            company = cm.company,
                            name = T.Name,
                            cost = T.Cost
                        };
            foreach (var prod in query)
            {
                database.Add(new product
                {
                    id = prod.id,
                    instrument = prod.instrument,
                    type = prod.type,
                    company = prod.company,
                    name = prod.name.Replace(" ", ""),
                    cost = prod.cost
                });
            }
            return database;
        }
        public List<product> search(string id,int i,int page)
        {
            if (id == "$")
                id = "";
            if (id == "undefined")
                id = "";
            DataContext store = new DataContext(cString);
            Table<Instruments> instruments = store.GetTable<Instruments>();
            Table<Instrument> instrument = store.GetTable<Instrument>();
            Table<Company> company = store.GetTable<Company>();
            List<product> database = new List<product>();
            List<product> database1 = new List<product>();
            // string[] prev1 = id.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var query = from T in instruments
                            join ts in instrument on T.Instrument equals ts.ID
                            join cm in company on T.Company equals cm.ID
                            where T.Name.Contains(id)|| cm.company.Contains(id)
                            select new
                            {
                                id = T.ID,
                                instrument = ts.Name,
                                type = T.Type,
                                company = cm.company,
                                name = T.Name,
                                cost = T.Cost
                            };
            
            switch (i)
            {
                case 0:
                    query = query.OrderBy(bas => bas.id);
                    break;
                case 1:
                    query = query.OrderBy(bas => bas.company);
                    break;
                case 2:
                    query = query.OrderBy(bas => bas.cost);
                    break;
                case 3:
                    query = query.OrderBy(bas => bas.name);
                    break;
                case 4:
                    query = query.OrderBy(bas => bas.instrument);
                    break;
                case 5:
                    query = query.OrderBy(bas => bas.type);
                    break;
            }
            int c = 0;
            foreach (var prod in query)
                {
                c++;
                database.Add(new product
                {
                    id = prod.id,
                    instrument = prod.instrument,
                    type = prod.type,
                    company = prod.company,
                    name = prod.name,
                    cost = prod.cost,
                    count = c
                    });
                }
            for (int it = 5 * page; it < 5 * (page + 1); it++)
            {
                if (it >= database.Count)
                    break;
                database1.Add(new product {
                    id = database[it].id,
                    instrument = database[it].instrument,
                    type = database[it].type,
                    company = database[it].company,
                    name = database[it].name,
                    cost = database[it].cost,
                count = database[database.Count-1].count
                });
            }
            return database1;
        }

        public List<string> fill(int id)
        {
            DataContext store = new DataContext(cString);
            Table<Instrument> instrument = store.GetTable<Instrument>();
            Table<Company> company = store.GetTable<Company>();
            List<string> database = new List<string>();
            switch (id)
            {
                case 1:
                    var a = from T in instrument
                               select T.Name;
                    foreach (var prod in a)
                    {
                        database.Add(prod);
                    }
                    return database;
                case 2:
                    var a1 = from T in company
                            select T.company;
                    foreach (var prod in a1)
                    {
                        database.Add(prod);
                    }
                    return database;
            }
            return database;
        }
        public List<product> selectpr(string id)
            {
            DataContext store = new DataContext(cString);
            Table<Instruments> instruments = store.GetTable<Instruments>();
            Table<Instrument> instrument = store.GetTable<Instrument>();
            Table<Company> company = store.GetTable<Company>();
            List<product> database = new List<product>();
            string[] prev1 = id.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string a in prev1)
            {
                var query = from T in instruments
                            join ts in instrument on T.Instrument equals ts.ID
                            join cm in company on T.Company equals cm.ID
                            where T.ID == Convert.ToInt32(a)
                            select new
                            {
                                id = T.ID,
                                instrument = ts.Name,
                                type = T.Type,
                                company = cm.company,
                                name = T.Name,
                                cost = T.Cost
                            };
                foreach (var prod in query)
                {
                    database.Add(new product
                    {
                        id = prod.id,
                        instrument = prod.instrument,
                        type = prod.type,
                        company = prod.company,
                        name = prod.name.Replace(" ",""),
                        cost = prod.cost
                    });
                }
            }
            return database;
        }
        public void ins(product it)
        {
            DataContext store = new DataContext(cString);
            Table<Instruments> instruments = store.GetTable<Instruments>();
            Table<Instrument> instrument = store.GetTable<Instrument>();
            Table<Company> company = store.GetTable<Company>();
            int instr = instrument.Where(n => n.Name == it.instrument).Select(n => n.ID).FirstOrDefault();
            int cp = company.Where(n => n.company == it.company).Select(n => n.ID).FirstOrDefault();
            Instruments nint = new Instruments()
            {
                Instrument = instr,
                Type=it.type,
                Company=cp,
                Name=it.name,
                Cost=it.cost
            }; 

            instruments.InsertOnSubmit(nint);
            store.SubmitChanges();  
        }
        public void upd(product it)
        {
            DataContext store = new DataContext(cString);
            Table<Instruments> instruments = store.GetTable<Instruments>();
            Table<Instrument> instrument = store.GetTable<Instrument>();
            Table<Company> company = store.GetTable<Company>();
            int instr = instrument.Where(n => n.Name == it.instrument).Select(n => n.ID).FirstOrDefault();
            int cp = company.Where(n => n.company == it.company).Select(n => n.ID).FirstOrDefault();
            Instruments nint = instruments.Where(n => n.ID == it.id).FirstOrDefault();
            nint.Instrument = instr;
            nint.Type = it.type;
            nint.Company = cp;
            nint.Name = it.name;
            nint.Cost = it.cost;
            store.SubmitChanges();
        }
        public void del(int id)
        {
            DataContext store = new DataContext(cString);
            Table<Instruments> instruments = store.GetTable<Instruments>();
            Table<Instrument> instrument = store.GetTable<Instrument>();
            Table<Company> company = store.GetTable<Company>();
            Instruments del = instruments.Where(n => n.ID == id).FirstOrDefault();
            instruments.DeleteOnSubmit(del);
            store.SubmitChanges();
        }
    }
}