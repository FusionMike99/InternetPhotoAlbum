﻿using InternetPhotoAlbum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    public class ProceduresRepository : IProceduresRepository
    {
        private readonly ApplicationContext context;

        public ProceduresRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void LockUser(string userId, bool isLocked)
        {
            SqlParameter paramUserId = new SqlParameter("@userId", userId);
            SqlParameter paramIsLocked = new SqlParameter("@isLocked", isLocked);
            context.Database.ExecuteSqlCommand("LockUser @userId, @isLocked", paramUserId, paramIsLocked);
        }
    }
}
