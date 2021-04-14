using SimpleThx.Data;
using SimpleThx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Services
{
    public class PostService
    {

        private readonly Guid _userID;

        public PostService (Guid userID)
        {
            _userID = userID;
        }

        public bool CreatePost (PostCreate model)
        {
            var entity = new Post()
            {
                PostUserID = _userID,
                AboutUserID = model.AboutUserID,
                Title = model.Title,
                Content = model.Content,
                Status = model.Status,
                CreateUTC = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }


        public IEnumerable<PostList> GetPosts()
        {

            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Posts
                    .Where(e => e.PostUserID == _userID)
                    .Select(e => new PostList
                    {
                        PostID = e.PostID,
                        Title = e.Title,
                
                    });

                return query.ToArray();
            }

        }


    } // END Post Service
}  //END Namespace
