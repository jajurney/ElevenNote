using ElevenNote.Data;
using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    public class NoteController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Note
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();
            return View(model);
        }
        // GET: Note
        // Note/Create/{id}
        public ActionResult Create()
        {
            return View();
        }
        // POST: Note
        // Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid)return View(model);
            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = _db.Notes.Find(id);
            var svc = CreateNoteService();
            //var model = svc.GetNoteById(id);
            if(note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model = new NoteEdit
            {
                NoteId = detail.NoteId,
                Title = detail.Title,
                Content = detail.Content
            };
            return View(model);
        }
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
}