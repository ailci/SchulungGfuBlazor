using Application.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services;

public class AuthorService(ILogger<AuthorService> logger, IDbContextFactory<QotdDbContext> contextFactory) : IAuthorService
{
}