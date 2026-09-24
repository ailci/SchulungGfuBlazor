using System;
using System.Collections.Generic;
using System.Text;
using Application.ViewModels.Qotd;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Quote, QuoteOfTheDayViewModel>();
    }
}