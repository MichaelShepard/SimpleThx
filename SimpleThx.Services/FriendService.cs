using SimpleThx.Data;
using SimpleThx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<AccountInfoList> FriendSearch(string searchString)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = ctx
                    .Accounts
                    .Select(e => new AccountInfoList
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName

                    });

                if(!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(e => e.LastName.Contains(searchString));
                }

                return query.ToArray();

            }
        }


        public IEnumerable<FriendList> GetFriends()
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = ctx
                .Friends
                .Where(e => e.FriendSend == _userID)
                .Select(e => new FriendList
                {

                    FriendID = e.FriendID,
                    FriendSend = e.FriendSend,
                    Status = e.Status,
                    CreateUTC = e.CreateUTC


                });

                return query.ToArray();

            }
        } // END Get Friends



        
        public bool AddConnectionWithFriend(FriendCreate model)
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



    } // END Friend Service
} // END Namespace
