﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Students.Common.Data;

public class ContextFactory : IDesignTimeDbContextFactory<StudentsContext>
{
    public StudentsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StudentsContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudentsProgrammingContext-23428837-2834-4740-ab78-0b481781e013;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new StudentsContext(optionsBuilder.Options);
    }
}