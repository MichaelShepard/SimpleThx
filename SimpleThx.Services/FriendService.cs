using SimpleThx.Data;
using SimpleThx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SimpleThx.Services
{
    public class FriendService
    {

        private readonly Guid _userID;

        public FriendService(Guid userID)
        {
            _userID = userID;
        }


        public IEnumerable<FriendList> GetFriends()
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = from e in ctx.Friends

                .Where(e => e.FriendSend == _userID  && e.Status != FriendStatus.Declined)
                join d in ctx.Accounts on e.FriendReceive equals d.UserID
                select new FriendList
                {
                    FriendID = e.FriendID,
                    FullName = d.FirstName + " " + d.LastName,
                    FriendReceive = e.FriendReceive,
                    Status = e.Status,
                    CreateUTC = e.CreateUTC

                };

                return query.ToArray();

            }
        } // END Get Friends

        public Guid GetFriendGuidByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity = ctx.Accounts
                    .Single(e => e.AccountID == id);

                return entity.UserID;         

            } 

        }

       
        public AccountInfoList GetFriendByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Accounts
                .Single(e => e.AccountID == id);

                return new AccountInfoList
                {
                    UserID = entity.UserID,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    State = entity.State,
                    Country = entity.Country
                };

            }
        }


        //public IEnumerable<AccountInfoList> FriendSearch(string searchString)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {

        //        var query = ctx.Accounts
        //            .Where (e=> e.UserID != _userID)
        //            .Select(e => new AccountInfoList
        //            {
        //                AccountID = e.AccountID,
        //                FullName = e.FirstName + " " + e.LastName,
        //                State = e.State,
        //                Country = e.Country

        //            });

        //        if(!String.IsNullOrEmpty(searchString))
        //        {
        //            query = query.Where(e => e.FullName.Contains(searchString));
        //        }

        //        return query.ToArray();

        //    }
        //}

        public IEnumerable<AccountInfoList> FriendSearch(string searchString)
        {
            using (var ctx = new ApplicationDbContext())
            {
                
                var query = ctx.Accounts
                    .Where(e=> e.UserID != _userID)
                    .Select (e=> new AccountInfoList

                            {
                                AccountID = e.AccountID,
                                FullName = e.FirstName + " " + e.LastName,
                                State = e.State,
                                Country = e.Country

                            });

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(e => e.FullName.Contains(searchString));
                }

                return query.ToArray();

            }
        }


        // Miscellanous tries on the serach 

        //from e in ctx.Accounts


        //.Where(e => ctx.Friends.All(y => y.FriendReceive == e.UserID))

        //from e in ctx.Accounts
        //        join p in cx.Friends ctx.Friends on e.UserID equals p.
        //        where ctx.Friends.Any(b => b.Status == null)
        //.Where(e => e.UserID != _userID)

        //where !(from o in ctx.Friends
        //        select o.FriendSend)
        //        .Contains(e.UserID)
        //select new AccountInfoList
        // .Where(e=> e.UserID = )


        public FriendList CreatesFriendListModel(int id)
        {

            Guid FriendReceive = GetFriendGuidByID(id);
            

            var entity = new FriendList()
            {
                FriendSend = _userID,
                FriendReceive = FriendReceive,
                Status = FriendStatus.Pending,
                CreateUTC = DateTimeOffset.Now
            };

            return entity;
        }

       //  need anothermethod to add the service

        public bool PostConnection(FriendList model)
        {
            var entity = new Friend()
            {
                FriendSend = _userID,
                FriendReceive = model.FriendReceive,
                Status = FriendStatus.Pending,
                CreateUTC = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Friends.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public FriendList GetFriendListModel(int id)
        {

            
            using (var ctx = new ApplicationDbContext()) 
            {
                var entity = ctx.Friends
                    .Single(e => e.FriendID == id);
            
                return new FriendList
                {
                    FriendID = entity.FriendID,
                    FriendReceive = entity.FriendReceive,
                    FriendSend = entity.FriendSend,
                    Status = entity.Status,
                    
                };
            }
        }

        public bool PostUpdateFriend(FriendList model, FriendStatus status)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Friends
                    .Single(e => e.FriendID == model.FriendID);

                entity.FriendID = model.FriendID;
                entity.FriendReceive = model.FriendReceive;
                entity.FriendSend = model.FriendSend;
                entity.Status = status;
                entity.ModifiedUTC = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }





    } // END Friend Service
} // END Namespace
