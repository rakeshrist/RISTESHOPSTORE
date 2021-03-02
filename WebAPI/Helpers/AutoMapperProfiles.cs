namespace WebAPI.Helpers
{
    using AutoMapper;
    using WebAPI.Dtos;
    using WebAPI.Models;
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<tbl_category, CategoryDto>().ReverseMap();
            CreateMap<tbl_category, CategoryUpdateDto>().ReverseMap();
        }
    }
}