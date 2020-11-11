﻿using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;
        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            var entity = new Note()
            {
                OwnerId = _userId,
                Title = model.Title,
                Content = model.Content,
                CreateUtc = DateTimeOffset.Now
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Notes
                    .Where(e => e.OwnerId == _userId)
                    .Select(e => new NoteListItem { NoteId = e.NoteId, Title = e.Title, CreateUtc = e.CreateUtc });
                return query.ToArray();
            }
        }
        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Notes
                    .Single(e => e.NoteId == id && e.OwnerId == _userId);
                return new NoteDetail
                {
                    NoteId = entity.NoteId,
                    Title = entity.Title,
                    Content = entity.Content,
                    CreatedUtc = entity.CreateUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
            }
        }
    }
}