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

        // Create model to be used in Create Method; Need to do this because I am brining over the ID
        public PostCreate PostModelCreate(PostCreate model, Guid id)
        {

            model.PostUserID = _userID;
            model.AboutUserID = id;
            model.Status = Status.Pending;
            model.CreateUTC = DateTimeOffset.Now;
            
            return model;
        }


        public bool CreatePost(PostCreate model)
        {
            var entity = new Post()
            {
                PostUserID = _userID,
                AboutUserID = model.AboutUserID,
                Title = model.Title,
                Content = model.Content,
                Status = Status.Pending,
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
                        Content = e.Content,
                        CreateUTC = e.CreateUTC,
                        Status = e.Status
                
                    });

                return query.ToArray();
            }

        }




    } // END Post Service
}  //END Namespace
