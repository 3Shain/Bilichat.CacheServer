﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanShain.Bilichat.Models;

namespace SanShain.Bilichat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : Controller
    {
        public BilichatContext Db { get; set; }

        public HistoryController(BilichatContext db)
        {
            Db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await Db.Entrys.GroupBy(x => x.room_id).Select(key => key.OrderByDescending(x => x.entry_time)
              .Select(u => new { u.user_id, u.user_intro, u.user_name, u.room_id, u.id, time = (u.entry_time.Ticks - (new DateTime(1970, 1, 1, 0, 0, 0)).Ticks) / 10000 }).FirstOrDefault()).OrderByDescending(x=>x.time).Take(20).ToArrayAsync());
        }
    }
}