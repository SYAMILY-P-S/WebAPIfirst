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
    public class LoginController : ApiController
    {
        SqlConnection sqlcon = new SqlConnection("server=DESKTOP-5CTHP94;database=ust_global;Integrated Security=true");
        ust_globalEntities1 ust = new ust_globalEntities1();
        public HttpResponseMessage Post(user l)
        {
            HttpResponseMessage msg = null;
            var usr = (from u in ust.users select u).ToList();
            var count = 0;
            for (var i = 0; i < usr.Count; i++)
            {
                if (usr[i].pswd == l.pswd && usr[i].uname == l.uname)
                {
                    count++;
                    break;
                }

            }
            if (count == 1)
                msg = Request.CreateResponse(HttpStatusCode.OK, "SuccessFully Logined");
            else
                msg = Request.CreateResponse(HttpStatusCode.NotFound, "Try again");
            return msg;
        }
    }

}

