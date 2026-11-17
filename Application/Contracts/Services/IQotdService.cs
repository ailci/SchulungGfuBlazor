using System;
using System.Collections.Generic;
using System.Text;
using Application.ViewModels.Qotd;

namespace Application.Contracts.Services;

public interface IQotdService
{
    Task<QuoteOfTheDayViewModel> GetQuoteOfTheDayAsync();
}