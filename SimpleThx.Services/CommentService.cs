
﻿using SimpleThx.Data;
using SimpleThx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Services
{
    public class CommentService
    {

        private readonly Guid _userID;

        public CommentService(Guid userID)
        {
            _userID = userID;
        }


        public CommentCreate CommentModelCreate(CommentCreate model, int id)
        {

            model.CommentorUserID = _userID;
            model.PostID = id;
            model.Status = CommentStatus.Pending;
            model.CreateUTC = DateTimeOffset.Now;

            return model;
        }

        public bool CreateComment (CommentCreate model, int id)
        {

            var entity = new Comment()
            {
                PostID = id,
                CommentorUserID = _userID,
                CommentContent = model.CommentContent,
                CreateUTC = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CommentList> GetComments()
        {
            using (var ctx = new ApplicationDbContext()) 
            { 

                var query = from e in ctx.Comments
                where e.CommentorUserID == _userID
                join d in ctx.Posts on e.PostID equals d.PostID

                select new CommentList
                {
                    CommentID = e.CommentID,
                    Title = d.Title,
                    CommentorUserID = e.CommentorUserID,
                    CommentContent = e.CommentContent,
                    CreateUTC = e.CreateUTC

                };

                return query.ToList();

            }
        }




    } // END Comment Service Class
} // END Namespace
