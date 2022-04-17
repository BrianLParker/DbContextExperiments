// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;

namespace DbContextExperiments.Api.Brokers.DbUpdateExceptions;

public interface IDbUpdateExceptionBroker
{
    Exception ConvertToMeaningfulException(DbUpdateException dbUpdateException);
}
