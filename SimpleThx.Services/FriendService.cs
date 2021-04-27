﻿using SimpleThx.Data;
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
                            .Where(e => e.FriendReceive == _userID && e.Status == FriendStatus.Accepted)
                            join d in ctx.Accounts on e.FriendSend equals d.UserID

                                        select new FriendList
                                        {
                                            FriendID = e.FriendID,
                                            FullName = d.FirstName + " " + d.LastName,
                                            State = d.State,
                                            Country = d.Country,
                                            FriendReceive = e.FriendReceive,
                                            FriendSend = e.FriendSend,
                                            Status = e.Status,
                                            CreateUTC = e.CreateUTC

                                        };
                var query1 = query.ToList();


                var query2 = from e2 in ctx.Friends
                             where e2.FriendSend == _userID && e2.Status == FriendStatus.Accepted
                             join d2 in ctx.Accounts on e2.FriendReceive equals d2.UserID

                             select new FriendList
                             {
                                 FriendID = e2.FriendID,
                                 FullName = d2.FirstName + " " + d2.LastName,

                                 State = d2.State,
                                 Country = d2.Country,
                                 FriendReceive = e2.FriendReceive,
                                 FriendSend = e2.FriendSend,
                                 Status = e2.Status,
                                 CreateUTC = e2.CreateUTC

                             };

                var query3 = query2.ToList();
                query3.AddRange(query1);
                return query3;

            }
        } // END Get Friends


    public IEnumerable<FriendList> GetNewFriends()
    {
        using (var ctx = new ApplicationDbContext())
        {

            var query = from e in ctx.Friends

            .Where(e => e.FriendReceive == _userID && e.Status == FriendStatus.Pending)
                        join d in ctx.Accounts on e.FriendSend equals d.UserID
                        select new FriendList
                        {
                            FriendID = e.FriendID,
                            FullName = d.FirstName + " " + d.LastName,
                            State = d.State,
                            Country = d.Country,
                            FriendReceive = e.FriendReceive,
                            FriendSend = e.FriendSend,
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

        


    public IEnumerable<AccountInfoList> FriendSearch(string searchString)
    {
        using (var ctx = new ApplicationDbContext())
        {
                var query = GetFriends();
                

                // This gets all friends where I RECEIVED an invite
               var receivedRequest = query.Where(e2 => e2.FriendReceive == _userID)
                    .Select(e => e.FriendSend).ToList();

                     // This gets all friends where I SENT an invite
                  var sentRequest =  query.Where(e2 => e2.FriendSend == _userID)
                    .Select(e => e.FriendReceive).ToList();


                receivedRequest.AddRange(sentRequest);

                var allAccounts = ctx.Accounts 
                    .Where(e3=> e3.UserID != _userID)
                    .Where(e4=> receivedRequest.All(e5=> e5 != e4.UserID))



                .Select(e => new AccountInfoList

                {

                    AccountID = e.AccountID,
                    FullName = e.FirstName + " " + e.LastName,
                    State = e.State,
                    Country = e.Country

                }).ToList();


                if (!String.IsNullOrEmpty(searchString))
                {
                    allAccounts = allAccounts.Where(e => e.FullName.Contains(searchString)).ToList();
                    
                }


                return allAccounts;

                //where !ctx.Friends.Select(b=> b.FriendSend).Contains(_userID)
                // where !ctx.Friends.Select(b => b.FriendReceive).Contains(_userID)

                //where !ctx.Friends.Any(b => b.FriendSend == _userID && b.FriendReceive == _userID)
                //where (e.UserID != _userID) // Removes the user under search from from

                //select new AccountInfoList

                //{

                //    AccountID = e.AccountID,
                //    FullName = e.FirstName + " " + e.LastName,
                //    State = e.State,
                //    Country = e.Country

                //};
                //var query1 = query.ToList();

                //var query2 = from e in ctx.Accounts
                //                //where !ctx.Friends.Select(b=> b.FriendSend).Contains(_userID)
                //                // where !ctx.Friends.Select(b => b.FriendReceive).Contains(_userID)

                //            where !ctx.Friends.Any(b => b.FriendReceive == _userID)
                //            where (e.UserID != _userID) // Removes the user under search from from

                //            select new AccountInfoList

                //            {

                //                AccountID = e.AccountID,
                //                FullName = e.FirstName + " " + e.LastName,
                //                State = e.State,
                //                Country = e.Country

                //            };


                //var query = from e in ctx.Accounts
                //            //where !ctx.Friends.Select(b=> b.FriendSend).Contains(_userID)
                //            // where !ctx.Friends.Select(b => b.FriendReceive).Contains(_userID)

                //            where !ctx.Friends.Any(b => b.FriendSend == _userID)
                //            where (e.UserID != _userID) // Removes the user under search from from

                //            select new AccountInfoList

                //        {

                //            AccountID = e.AccountID,
                //            FullName = e.FirstName + " " + e.LastName,
                //            State = e.State,
                //            Country = e.Country

                //        };

                

                

                //var query3 = query2.ToList();
                //query3.AddRange(query1);
                //return query3;

            }
    }

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
