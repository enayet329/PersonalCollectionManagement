
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using AutoMapper;
using PersonalCollectionManager.Application.DTOs;
using PersonalCollectionManager.Data.Repositories;
using System.Linq;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IItemTagRepository _itemTagRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public TagService(ITagRepository tagRepository, IItemTagRepository itemTagRepository, IItemRepository itemRepository, IMapper mapper, ILogger<TagService> logger)
        {
            _tagRepository = tagRepository;
            _itemTagRepository = itemTagRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<OperationResult> DeleteTagAsync(Guid id)
        {
            try
            {
                var tagEntity = await _tagRepository.GetByIdAsync(id);
                if (tagEntity == null)
                {
                    return new OperationResult(false, "Tag not found.");
                }

                var itemTag = await _tagRepository.GetTagWithItemTagAsync(id);
                if (itemTag == null)
                {
                    return new OperationResult(false, "No item associated with this tag.");
                }


                _itemTagRepository.RemoveTagFromItem(itemTag.Id, tagEntity.Id);
                await _tagRepository.Remove(tagEntity);

                return new OperationResult(true, "Tag deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting tag.");
                return new OperationResult(false, "Error deleting tag.");
            }
        }
        public async Task<IEnumerable<TagDto>> GetAllTagAsync()
        {
            try
            {
                var tags = await _tagRepository.GetAllAsync();
                
                return _mapper.Map<IEnumerable<TagDto>>(tags);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all tags.");
                return null;
            }
        }

        public async Task<TagDto> GetTagByIdAsync(Guid id)
        {
            try
            {
                var tag = await _tagRepository.GetByIdAsync(id);
                return _mapper.Map<TagDto>(tag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tag by id.");
                return null;
            }
        }

        public async Task<IEnumerable<TagDto>> GetTagsByItemIdAsync(Guid id)
        {
            try
            {
                var tags = await _tagRepository.GetByItemId(id);
                return _mapper.Map<IEnumerable<TagDto>>(tags);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tags by item id.");
                return null;
            }
        }

        public async Task<IEnumerable<TagDto>> GetPopularTagsAsync()
        {
            try
            {
                return _mapper.Map<IEnumerable<TagDto>>(await _tagRepository.GetTopTagsAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting popular tags.");
                return null;
            }
        }

        public async Task<OperationResult> AddTagAsync(IEnumerable<TagRequestDto> tag)
        {
            try
            {
                var tagEntities = _mapper.Map<IEnumerable<Tag>>(tag);
                var result = await _tagRepository.AddRangeAsync(tagEntities);

                var itemTags = new List<ItemTag>();
                var tagIds = result.Select(t => t.Id).ToList();

                for (int i = 0; i < tagEntities.Count(); i++)
                {
                    var tagEntity = tagEntities.ElementAt(i);
                    var tagId = tagIds[i];

                    itemTags.Add(new ItemTag { ItemId = tag.ElementAt(i).ItemId, TagId = tagId });
                }

                var resultItemTags = await _itemTagRepository.AddRangeAsync(itemTags);
                Console.WriteLine(resultItemTags);
                return new OperationResult(true, "Tag added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding tag.");
                return new OperationResult(false, "Error adding tag.");
            }
        }


        public async Task<OperationResult> UpdateTagAsync(Guid itemId, IEnumerable<TagDto> tags)
        {
            try
            {
                var tagEntities = _mapper.Map<IEnumerable<Tag>>(tags);
                var itemTags = await _itemTagRepository.getTagsByItemAsync(itemId);
                var existingTags = itemTags.Select(t => t.TagId ).ToList();
                var newTags = tagEntities.Select(t => t.Id).ToList();
                var tagsToRemove = existingTags.Except(newTags).ToList();
                var tagsToAdd = newTags.Except(tagsToRemove).ToList();

                foreach (var tagId in tagsToRemove)
                {
                    _itemTagRepository.RemoveTagFromItem(itemId, tagId);
                    await _itemRepository.SaveChangesAsync();
                }

                foreach (var tag in tagEntities)
                {
                    if (tagsToAdd.Contains(tag.Id))
                    {
                        await _tagRepository.UpdateRangeAsync(tagEntities);
                        await _itemRepository.SaveChangesAsync();
                    }
                }

                return new OperationResult(true, "Tag updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tag.");
                return new OperationResult(false, "Error updating tag.");
            }
        }
    }
}
