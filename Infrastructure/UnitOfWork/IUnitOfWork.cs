﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork<out TContext>
      where TContext : DbContext, new()
    {
        TContext Context { get; }
        void CreateTransaction();
        bool Commit();
        void Rollback();
        bool Save();
    }
}
