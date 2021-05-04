﻿using SimpleThx.Data;
using SimpleThx.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Services
{
    public class AccountInfoService
    {
        private readonly Guid _userID;

        public AccountInfoService(Guid userID)
        {
            _userID = userID;
        }


        // public bool CreateAccountInfo(AccountInfoCreate model, string uniqueFileName)
        public bool CreateAccountInfo(AccountInfoCreate model)
        {
            var entity = new AccountInfo()
            {

                UserID = _userID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                State = model.State,
                Country = model.Country,
                //PictureURL = uniqueFileName,
                CreateUTC = DateTimeOffset.Now

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Accounts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //public string UploadedFile(AccountInfoCreate model)
        //{

        //    string uniqueFileName = null;

        //    if (model.PictureImage != null)
        //    {

        //        var uploadDir = "~/Content/Images"

        //             //var filePath = Path.Combine(uploads, uniqueFileName);
        //            var imagePath = Path.Combine(Server.MapPath(uploadDir), model.PictureImage.FileName);






        //        var uniqueFileName = GetUniqueFileName(model.PictureImage.FileName);
        //        var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Images");
               
        //        model.PictureImage.CopyTo(new FileStream(filePath, FileMode.Create));

                



        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.PictureImage.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}


        public string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }


        public IEnumerable<AccountInfoList> GetAccountInfo()
        {

            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Accounts
                    .Where(e => e.UserID == _userID)
                    .Select(e => new AccountInfoList
                    {
                        AccountID = e.AccountID,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        State = e.State,
                        Country = e.Country

                    });

                return query.ToArray();
            }

        }


        public AccountInfoList GetAccountInfoByAccountID(int id)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Accounts
                    .Single(e => e.UserID == _userID && e.AccountID == id);

                return new AccountInfoList
                {
                    AccountID = entity.AccountID,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    State = entity.State,
                    Country = entity.Country,
                    CreateUTC = entity.CreateUTC,
                    ModifiedUTC = entity.ModifiedUTC

                };
            }
        }

        public bool UpdateAccountInfo(AccountInfoEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Accounts
                    .Single(e => e.UserID == _userID && e.AccountID == model.AccountID);

                entity.AccountID = model.AccountID;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.State = model.State;
                entity.Country = model.Country;
                entity.ModifiedUTC = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }




    } // END Account Info Service
} // END NameSpace
