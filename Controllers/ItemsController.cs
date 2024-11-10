using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MyApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        // In-memory list to simulate a database
        private static List<Item> Items = new List<Item>
        {
            new Item { Id = 1, Name = "Item1" },
            new Item { Id = 2, Name = "Item2" }
        };

        // GET: api/items
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Items);
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound();
            
            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public IActionResult Create(Item newItem)
        {
            // Set ID to max existing ID + 1
            newItem.Id = Items.Count > 0 ? Items.Max(i => i.Id) + 1 : 1;
            Items.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        // PUT: api/items/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Item updatedItem)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound();
            
            item.Name = updatedItem.Name;
            return NoContent();
        }

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound();
            
            Items.Remove(item);
            return NoContent();
        }
    }

    // Simple model for item
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
