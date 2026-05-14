using AutoMapper;
using Biblioteca.Server.DTOs;
using Biblioteca.Server.Models;

namespace Biblioteca.Server.Profiler
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookInput, Book>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId));

            CreateMap<Book, BookResponse>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
        }
    }
}
