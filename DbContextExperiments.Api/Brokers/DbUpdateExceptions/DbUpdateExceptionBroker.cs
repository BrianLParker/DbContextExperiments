// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using DbContextExperiments.Api.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DbContextExperiments.Api.Brokers.DbUpdateExceptions;

public class DbUpdateExceptionBroker : IDbUpdateExceptionBroker
{
    public Exception ConvertToMeaningfulException(DbUpdateException dbUpdateException)
    {
        SqlException sqlException = (SqlException)dbUpdateException.InnerException;

        return sqlException.Number switch
        {
            207 => new InvalidColumnNameException(sqlException.Message),
            208 => new InvalidObjectNameException(sqlException.Message),
            547 => new ForeignKeyConstraintConflictException(sqlException.Message),
            2601 => new DuplicateKeyWithUniqueIndexException(sqlException.Message),
            2627 => new DuplicateKeyException(sqlException.Message),
            _ => throw new ArgumentException("dbUpdateException.InnerException.Number"),
        };
    }
}
