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
    [Authorize]
    public class WatchlistsController : ApiController
    {
        private WisdomApiEntities db = new WisdomApiEntities();

        // GET: api/Watchlists
        public List<WatchlistDTO> GetWatchlists()
        {

            using (WisdomApiEntities dbcontext = new WisdomApiEntities())
            {
                List<WatchlistDTO> watchs = new List<WatchlistDTO>();

                var list = dbcontext.Watchlists.ToList();


                foreach (var item in list)
                {
                    WatchlistDTO dto = new WatchlistDTO();

                    dto.FullName = item.Ind_Name;
                    dto.UniqueIdentity = item.RecordID;
                    dto.City = item.City;

                    watchs.Add(dto);
                }


                return watchs;
            }


        }





        // GET: api/Watchlists/5
        [ResponseType(typeof(Watchlist))]
        public IHttpActionResult GetWatchlist(long id)
        {
            Watchlist watchlist = db.Watchlists.Find(id);
            if (watchlist == null)
            {
                return NotFound();
            }

            return Ok(watchlist);
        }

        #region Commented
        //// PUT: api/Watchlists/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutWatchlist(long id, Watchlist watchlist)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != watchlist.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(watchlist).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WatchlistExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Watchlists
        //[ResponseType(typeof(Watchlist))]
        //public IHttpActionResult PostWatchlist(Watchlist watchlist)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Watchlists.Add(watchlist);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = watchlist.Id }, watchlist);
        //}

        //// DELETE: api/Watchlists/5
        //[ResponseType(typeof(Watchlist))]
        //public IHttpActionResult DeleteWatchlist(long id)
        //{
        //    Watchlist watchlist = db.Watchlists.Find(id);
        //    if (watchlist == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Watchlists.Remove(watchlist);
        //    db.SaveChanges();

        //    return Ok(watchlist);
        //} 
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WatchlistExists(long id)
        {
            return db.Watchlists.Count(e => e.Id == id) > 0;
        }
    }
}
