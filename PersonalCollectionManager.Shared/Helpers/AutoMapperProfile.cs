
using AutoMapper;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Shared.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // ResponseDTOs to Domain Entities
            CreateMap<Collection, CollectionDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<Like, LikeDTO>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            // RequestDTOs to Domain Entities
            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.IsBlocked, opt => opt.Ignore())
                .ForMember(dest => dest.Collections, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore());
            CreateMap<User, RegisterRequestDto>();

            CreateMap<CollectionRequestDto, Collection>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore());
            CreateMap<Collection, CollectionRequestDto>();

            CreateMap<ItemRequestDto, Item>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Collection, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore());
            CreateMap<Item, ItemRequestDto>();

            CreateMap<TagRequestDto, Tag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Collection, opt => opt.Ignore());
            CreateMap<Tag, TagRequestDto>();

        }
    }
}
