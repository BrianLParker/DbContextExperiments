// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DbContextExperiments.Api.Brokers.DbContexts;

public partial class StorageContext<T> : IStorageContext<T>
    where T : class
{
    private IQueryable<T> QueryableDbSet => dbSet.AsNoTracking();

    public Type ElementType => QueryableDbSet.ElementType;

    public Expression Expression => QueryableDbSet.Expression;

    public IQueryProvider Provider => QueryableDbSet.Provider;

    public IEnumerator<T> GetEnumerator() => QueryableDbSet.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => QueryableDbSet.GetEnumerator();
}
