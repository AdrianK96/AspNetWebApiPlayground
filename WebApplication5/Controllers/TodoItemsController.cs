using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TodoItemsController : ApiController
    {
        private WebApplication5Context db = new WebApplication5Context();

        // GET: api/TodoItems
        public IQueryable<TodoItemDTO> GetTodoItems()
        {
            // with Eager Loading enabled
            // return db.TodoItems.Include(t => t.Owner);

            var items = from t in db.TodoItems
                        select new TodoItemDTO()
                        {
                            Id = t.Id,
                            Name = t.Name,
                            OwnerName = t.Owner.Name,
                            CreatedDateTime = t.CreatedDateTime,
                            IsCompleted = t.IsCompleted
                        };

            return items;
        }

        // GET: api/TodoItems/5
        [ResponseType(typeof(TodoItemDTO))]
        public async Task<IHttpActionResult> GetTodoItem(int id)
        {
            //TodoItem todoItem = await db.TodoItems.FindAsync(id);
            //if (todoItem == null)
            //{
            //    return NotFound();
            //}

            //return Ok(todoItem);

            var item = await db.TodoItems.Include(t => t.Owner).Select(t =>
                new TodoItemDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                    OwnerName = t.Owner.Name,
                    IsCompleted = t.IsCompleted,
                    CreatedDateTime = t.CreatedDateTime
                }).SingleOrDefaultAsync(t => t.Id == id);

            if(item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/TodoItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            db.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TodoItems
        [ResponseType(typeof(TodoItem))]
        public async Task<IHttpActionResult> PostTodoItem(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TodoItems.Add(todoItem);
            await db.SaveChangesAsync();

            // new code:
            // load owner name
            db.Entry(todoItem).Reference(t => t.Owner).Load();

            var dto = new TodoItemDTO()
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsCompleted = todoItem.IsCompleted,
                CreatedDateTime = todoItem.CreatedDateTime,
                OwnerName = todoItem.Owner.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = todoItem.Id }, dto);
        }

        // DELETE: api/TodoItems/5
        [ResponseType(typeof(TodoItem))]
        public async Task<IHttpActionResult> DeleteTodoItem(int id)
        {
            TodoItem todoItem = await db.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            db.TodoItems.Remove(todoItem);
            await db.SaveChangesAsync();

            return Ok(todoItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TodoItemExists(int id)
        {
            return db.TodoItems.Count(e => e.Id == id) > 0;
        }
    }
}