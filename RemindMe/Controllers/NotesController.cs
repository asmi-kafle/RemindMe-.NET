using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using NotesDataAccess;

namespace RemindMe.Controllers
{
    public class NotesController : ApiController
    {
        /// the router routes the calls from frontend to the right method based on the HTTP action verb
        /// these methods then perform the CRUD operation on a embedded database based on the call's request. 
        /// this change then gets synchronously or asynchronously gets reflected on the databse
        

        // perfoms read operation on the database and returns all entries
        public IEnumerable<Note> Get()
        {
            using (NotesDBEntities entities = new NotesDBEntities())
            {
                return entities.Notes.ToList();
            }
        }


        // performs read operation on the database and find the row with the matching id and returns just that entity
        public HttpResponseMessage Get(int ID)
        {
            using (NotesDBEntities entities = new NotesDBEntities())
            {
                var entityWithID = entities.Notes.FirstOrDefault(e => e.id == ID);
                if (entityWithID != null) //no try catch needed here because if there is no data, it will still return a null JSON object
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entityWithID);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Note not found");
                }
            }
        }


        // performs create request on the database entity to create a new note with note title and a task taken from the body of the HTTP request
        public HttpResponseMessage Post([FromBody] Note note)
        {
            try
            {
                using (NotesDBEntities entities = new NotesDBEntities())
                {
                    entities.Notes.Add(note);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, note);
                    message.Headers.Location = new Uri(Request.RequestUri + note.id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        // performs delete operation on the database entity by matching aginst the given id. The deleted row is returned from this call
        public HttpResponseMessage Delete(int ID)
        {
            try
            {
                using (NotesDBEntities entities = new NotesDBEntities())
                {
                    var entityWithID = entities.Notes.FirstOrDefault(e => e.id == ID);
                    if (entityWithID == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Note not found");
                    }
                    else
                    {
                        entities.Notes.Remove(entityWithID);
                        entities.SaveChanges();
                        return Request.CreateErrorResponse(HttpStatusCode.OK, "Note with id = " + ID.ToString() + " has been deleted.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        // performs update request on the row with the matching input id. The data to be updated is taken from the body of the HTTP request
        public HttpResponseMessage Put(int ID, [FromBody] Note note)
        {

            try
            {
                using (NotesDBEntities entities = new NotesDBEntities())
                {
                    var entity = entities.Notes.FirstOrDefault(e => e.id == ID);

                    if (entity != null)
                    {
                        entity.Title = note.Title;
                        entity.Content = note.Content;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Note not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }

        }
