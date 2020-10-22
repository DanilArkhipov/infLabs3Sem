using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Prac4.Models;

namespace Prac4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController:Controller
    {
        private static List<Movie> movies = new List<Movie>()
        {
            new Movie(Guid.Parse("10000000-0000-0000-0000-000000000000"), "Дом, который построил Джек", "Артхаус",
                "Ларс фон Триер", "21.08.2020"),
            new Movie(Guid.Parse("20000000-0000-0000-0000-000000000000"), "Иваново детство", "Военная драмма",
                "Тарковский", "03.04.1974"),
            new Movie(Guid.Parse("30000000-0000-0000-0000-000000000000"), "Довод", "Артхаус",
                "Кристофер Нолан", "01.10.2020"),
            new Movie(Guid.Parse("40000000-0000-0000-0000-000000000000"), "Твин пикс", "Детектив",
                "Дэвид Линч", "21.08.1997"),
            new Movie(Guid.Parse("50000000-0000-0000-0000-000000000000"), "Форсаж 7", "Боевик",    
                "Джеймс Ван", "07.11.2016"),
            new Movie(Guid.Parse("60000000-0000-0000-0000-000000000000"), "Джентельмены", "Криминал",
                "Гай Ричи", "07.03.2019"),
            new Movie(Guid.Parse("70000000-0000-0000-0000-000000000000"), "Ирландец", "Криминал",
                "Мартин Скорсезе", "21.08.2019"),

        };

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return movies;
        }
        
        [HttpGet("get/{id}")]
        public Movie Get(string id)
        {
            return  movies.Where(x => x.Id.Equals(Guid.Parse(id))).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Post([FromForm] string id, [FromForm] string name,[FromForm] string janr,[FromForm] string author,[FromForm] string reliseDate)
        {
            movies.Add(new Movie(Guid.Parse(id),name,janr,author,reliseDate));
            return Ok();
        }
        [HttpPut]
        public IActionResult Put([FromForm] string currentId,[FromForm] string changedId, [FromForm] string name,[FromForm] string janr,[FromForm] string author,[FromForm] string reliseDate)
        {
            var tmp = movies.Where(x => x.Id.ToString() == currentId);
            foreach (var v in tmp)
            {
                if (changedId != null) v.Id = Guid.Parse(changedId);
                if (name != null) v.Name = name;
                if (janr != null) v.Janr = janr;
                if (author != null) v.Author = author;
                if (reliseDate != null) v.ReliseDate = reliseDate;
            }

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromForm] string id)
        {
            movies.RemoveAll(x => x.Id.ToString() == id);
            return Ok();
        }
    }
}