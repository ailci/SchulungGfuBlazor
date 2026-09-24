using Application.ViewModels.Author;
using Application.ViewModels.Qotd;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Quote, QuoteOfTheDayViewModel>();
        CreateMap<Author, AuthorViewModel>();
    }
}