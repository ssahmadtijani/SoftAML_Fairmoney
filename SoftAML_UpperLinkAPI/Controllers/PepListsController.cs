using SoftAML_UpperLinkAPI.Contexts;
using SoftAML_UpperLinkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SoftAML_UpperLinkAPI.Controllers
{
    // [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[EnableCors(origins: "https://localhost:44361/", headers: "*", methods: "*")]
    public class PepListsController : ApiController
    {
        private WisdomApiEntities db = new WisdomApiEntities();

        // GET: api/PepLists
        public List<PepListDTO> GetPepLists()
        {

            using (WisdomApiEntities dbContext = new WisdomApiEntities())
            {
                List<PepListDTO> ret = new List<PepListDTO>();
                var list = dbContext.PepLists.ToList();

                foreach (var item in list)
                {
                    PepListDTO dto = new PepListDTO
                    {
                        FullName = $"{item.FirstName} {item.LastName} {item.OtherNames}",
                        PepClassification = item.PresentPosition,
                        PresentPosition = item.PresentPosition,
                        PreviousPosition = item.PreviousPosition,
                        UniqueIdentity = item.UniqueIdentifier
                    };

                    ret.Add(dto);
                }
                return ret;
            }

        }




        public IHttpActionResult LoadWatchlists()
        {
            var list = new List<Watchlist>();

            list = db.Watchlists.ToList();

            return Ok(list);

        }




        // GET: api/PepLists/5
        [ResponseType(typeof(PepList))]
        public IHttpActionResult GetPepList(long id)
        {
            PepList pepList = db.PepLists.Find(id);
            if (pepList == null)
            {
                return NotFound();
            }

            return Ok(pepList);
        }


        //[HttpGet]
        //public HttpResponseMessage AllPeps(long id)
        //{
        //    using (WisdomApiEntities entities = new WisdomApiEntities())
        //    {
        //        var entity = entities.PepLists.Where(x => x.Id == id).First();
        //        if (entity != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound,
        //                "Employee with Id " + id.ToString() + " not found");
        //        }
        //    }
        //}
        public IHttpActionResult Geps()
        {


            List<PepListDTO> ret = new List<PepListDTO>();
            var list = db.PepLists.Take(500);

            foreach (var item in list)
            {
                PepListDTO dto = new PepListDTO
                {
                    FullName = $"{item.FirstName} {item.LastName} {item.OtherNames}",
                    PepClassification = item.PresentPosition,
                    PresentPosition = item.PresentPosition,
                    PreviousPosition = item.PreviousPosition,
                    UniqueIdentity = item.UniqueIdentifier
                };

                ret.Add(dto);
            }
            return Ok(ret);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PepListExists(long id)
        {
            return db.PepLists.Count(e => e.Id == id) > 0;
        }
    }
}
