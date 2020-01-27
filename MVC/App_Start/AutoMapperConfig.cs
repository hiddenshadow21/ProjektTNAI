﻿using AutoMapper;
using Model.Entities;
using MVC.Models.OutputModels;

namespace MVC
{
    internal class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Author, AuthorOutputModel>().ForMember(x => x.FullName, d => d.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<Book, BookOutputModel>().ForMember(x => x.Author, d => d.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName));
            CreateMap<Borrower, BorrowerOutputModel>().ForMember(x => x.FullName, d => d.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<Loan, LoanOutputModel>()
                .ForMember(x => x.Book, d => d.MapFrom(src => src.Book.Name))
                .ForMember(x => x.Borrower, d => d.MapFrom(src => src.Borrower.FirstName + " " + src.Borrower.LastName));
        }
    }
}