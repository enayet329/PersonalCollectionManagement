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
            // Custom mapping ResponseDTOs to Domain Entities
            CreateMap<Collection, CollectionDto>()
                .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src => src.Items.Count))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ReverseMap();

            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.CollectionName, opt => opt.MapFrom(src => src.Collection.Name))
                .ForMember(dest => dest.CollectionId, opt => opt.MapFrom(src => src.Collection.Id))
                .ForMember(dest => dest.TagNames, opt => opt.MapFrom(src => src.ItemTags.Select(it => it.Tag.Name)))
                .ReverseMap();

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username));
                 


            CreateMap<Like, LikeDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<ItemAlgoliaDto, ItemDto>().ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.PrefrredLanguage, opt => opt.MapFrom(src => src.PreferredLanguage)) // Fixed property name
                .ForMember(dest => dest.PreffrredThemeDark, opt => opt.MapFrom(src => src.PreferredThemeDark)) // Fixed property name
                .ReverseMap();

            // RequestDTOs to Domain Entities
            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PreferredThemeDark, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokens, opt => opt.Ignore())
                .ForMember(dest => dest.IsAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.IsBlocked, opt => opt.Ignore())
                .ForMember(dest => dest.Collections, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore());

            CreateMap<CollectionRequestDto, Collection>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore());

            CreateMap<ItemRequestDto, Item>()
                .ForMember(dest => dest.Collection, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore());

            CreateMap<TagRequestDto, Tag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CommentRequestDto, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Item, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<LikeRequestDto, Like>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Item, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            // Domain Entities to UpdateRequestDTOs and vice versa
            CreateMap<ItemUpdateRequestDto, Item>()
                .ForMember(dest => dest.Collection, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore());

            CreateMap<Item, ItemUpdateRequestDto>();

            // CustomField mappings
            CreateMap<CustomField, CustomFieldDto>().ReverseMap();
            CreateMap<CustomFieldCreateDto, CustomField>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Collection, opt => opt.Ignore())
                .ForMember(dest => dest.CustomFieldValues, opt => opt.Ignore());
            CreateMap<CustomFieldUpdateDto, CustomField>()
                .ForMember(dest => dest.Collection, opt => opt.Ignore())
                .ForMember(dest => dest.CustomFieldValues, opt => opt.Ignore());
            CreateMap<CustomField, CustomFieldUpdateDto>();

            // CustomFieldValue mappings
            CreateMap<CustomFieldValue, CustomFieldValueDto>().ReverseMap();
            CreateMap<CustomFieldValueCreateDto, CustomFieldValue>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CustomField, opt => opt.Ignore())
                .ForMember(dest => dest.Item, opt => opt.Ignore());
            CreateMap<CustomFieldValueUpdateDto, CustomFieldValue>()
                .ForMember(dest => dest.CustomField, opt => opt.Ignore())
                .ForMember(dest => dest.Item, opt => opt.Ignore());
            CreateMap<CustomFieldValue, CustomFieldValueUpdateDto>();
        }
    }
}
