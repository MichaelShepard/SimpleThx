using SimpleThx.Data;
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

        public bool CreateComment (CommentCreate model)
        {

            var entity = new Comment()
            {
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

                var query = ctx.Comments
                .Where(e => e.CommentorUserID == _userID)
                .Select(e => new CommentList
                {
                    CommentID = e.CommentID,
                    CommentContent = e.CommentContent,
                    CreateUTC = e.CreateUTC

                });

                return query.ToList();

            }
        }



    } // END Comment Service Class
} // END Namespace
