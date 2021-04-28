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
                // Gets all the Posts that you send
                var query = from e in ctx.Posts
                            .Where(e => e.PostUserID == _userID && e.Status != Status.Declined)
                            join d in ctx.Accounts on e.AboutUserID equals d.UserID

                            select new PostList
                            {
                                PostID = e.PostID,
                                FullName = d.FirstName + " " + d.LastName,
                                Title = e.Title,
                                Content = e.Content,
                                UserID = _userID,
                                PostUserID = e.PostUserID,
                                AboutUserID = e.AboutUserID,
                                Status = e.Status,
                                CreateUTC = e.CreateUTC

                            };
                var query1 = query.ToList();

                // Gets all the Posts that you receive
                var query2 = from e2 in ctx.Posts
                             where e2.AboutUserID == _userID && e2.Status != Status.Declined
                             join d2 in ctx.Accounts on e2.PostUserID equals d2.UserID

                             select new PostList
                             {
                                 PostID = e2.PostID,
                                 FullName = d2.FirstName + " " + d2.LastName,
                                 Title = e2.Title,
                                 Content = e2.Content,
                                 UserID = _userID,
                                 PostUserID = e2.PostUserID,
                                 AboutUserID = e2.AboutUserID,
                                 Status = e2.Status,
                                 CreateUTC = e2.CreateUTC

                             };

                var query3 = query2.ToList();
                query3.AddRange(query1);
                return query3;

            }

        }

        public PostList GetPostListModel(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Posts
                    .Single(e => e.PostID == id);

                return new PostList
                {
                    PostID = entity.PostID,
                    PostUserID = entity.PostUserID,
                    AboutUserID = entity.AboutUserID,
                    Status = entity.Status
                };
            }
        } // END Get Post List Model

        public bool PostUpdatePost(PostList model, Status status)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Posts
                    .Single(e => e.PostID == model.PostID);

                entity.PostID = model.PostID;
                entity.PostUserID = model.PostUserID;
                entity.AboutUserID = model.AboutUserID;
                entity.Status = status;
                entity.ModifiedUTC = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        } //END Post Update Post


    } // END Post Service
}  //END Namespace
