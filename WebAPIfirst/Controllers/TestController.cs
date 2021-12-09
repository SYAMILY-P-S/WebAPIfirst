using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using WebAPIfirst.Models;

namespace WebAPIfirst.Controllers
{
    public class TestController : ApiController
    {

        SqlConnection sqlcon = new SqlConnection("server=DESKTOP-5CTHP94;database=ust_global;Integrated Security=true");
        ust_globalEntities ust = new ust_globalEntities();

        public IEnumerable<PLAYER> Get()
        {
            var plrn = (from plr in ust.PLAYERs select plr).ToList();
            return plrn;
        }

        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage msg = null;
            var plrn = (from plr in ust.PLAYERs where plr.PLRNO == id select plr).FirstOrDefault<PLAYER>();
            if (plrn != null)
            {
                msg = Request.CreateResponse(HttpStatusCode.OK, plrn);
            }
            else
            {
                msg = Request.CreateResponse(HttpStatusCode.NotFound, "Player Not Found");
            }
            return msg;
        }

        //////// POST: api/Demo
        public HttpResponseMessage Post([FromBody] PLAYER b)
        {
            HttpResponseMessage msg = null;
            var plrn = (from plr in ust.PLAYERs where plr.PLRNO == b.PLRNO select plr).FirstOrDefault<PLAYER>();
            if (plrn == null)
            {
                ust.PLAYERs.Add(b);
                ust.SaveChanges();
                msg = Request.CreateResponse(HttpStatusCode.OK, "Player Added");
            }
            else
            {
                msg = Request.CreateResponse(HttpStatusCode.NotFound, "Player Not added");
            }
            return msg;
        }

        //////// PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody] PLAYER b)
        {
            HttpResponseMessage msg = null;
            var plrn = (from plr in ust.PLAYERs where plr.PLRNO == id select plr).FirstOrDefault<PLAYER>();
            if (plrn != null)
            {
                msg = Request.CreateResponse(HttpStatusCode.OK, plrn);
                try
                {
                    var query =
                        from std in ust.PLAYERs where std.PLRNO == id select std;
                    foreach (PLAYER plr in query)
                    {
                        plr.PLRNO = b.PLRNO;
                        plr.PLRNAME = b.PLRNAME;
                        plr.GAME = b.GAME;
                        plr.COUNTRY = b.COUNTRY;
                    }
                    ust.SaveChanges();
                    msg = Request.CreateResponse(HttpStatusCode.OK, "Player Updated");
                }
                catch
                {
                    msg = Request.CreateResponse(HttpStatusCode.NotFound, "Not Updated");
                }
            }
            return msg;
        }

        //////// DELETE api/Test/5
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage msg = null;
            try
            {
                var delobj = ust.PLAYERs.Where(p => p.PLRNO == id).SingleOrDefault();
                ust.PLAYERs.Remove(delobj);
                ust.SaveChanges();

                msg = Request.CreateResponse(HttpStatusCode.OK, "Player Deleted");
            }
            catch
            {
                msg = Request.CreateResponse(HttpStatusCode.OK, "Check Details");
            }
            return msg;
        }
       
    }

}

