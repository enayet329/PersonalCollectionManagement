
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
                var tags = await _tagRepository.GatAllTagsAsync();


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
        public async Task<OperationResult> AddTagAsync(IEnumerable<TagRequestDto> tagRequests)
        {
            try
            {
                var itemId = tagRequests.FirstOrDefault()?.ItemId;

                if (itemId == null || itemId == Guid.Empty)
                {
                    return new OperationResult(false, "Error adding tag - Invalid Item ID.");
                }

                var newTags = tagRequests.Select(tr => tr.Name.ToLower()).Distinct().ToList();
                var existingTags = await _tagRepository.GetAllAsync();
                var existingTagNames = existingTags.Select(t => t.Name.ToLower()).ToList();

                // Tags to add (those not already existing)
                var tagsToAdd = newTags.Except(existingTagNames).ToList();

                // Add new tags to the database
                var addedTags = tagsToAdd.Select(tagName => new Tag { Name = tagName }).ToList();
                await _tagRepository.AddRangeAsync(addedTags);
                await _itemRepository.SaveChangesAsync();

                // Associate all tags (both new and existing) with the item
                var allTags = existingTags
                    .Where(t => newTags.Contains(t.Name.ToLower()))
                    .Concat(addedTags)
                    .ToList();

                foreach (var tag in allTags)
                {
                    var itemTag = new ItemTag
                    {
                        ItemId = itemId.Value,
                        TagId = tag.Id
                    };
                    await _itemTagRepository.AddAsync(itemTag);
                }

                await _itemRepository.SaveChangesAsync();
                return new OperationResult(true, "Tag(s) added and associated successfully.");
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
                var tagNames = tags.Select(t => t.Name.ToLower()).Distinct().ToList();
                var existingTags = await _tagRepository.GetAllAsync();
                var existingTagNames = existingTags.Select(t => t.Name.ToLower()).ToList();

                // Tags to add (those not already existing)
                var tagsToAdd = tagNames.Except(existingTagNames).ToList();

                // Add new tags to the database
                var addedTags = tagsToAdd.Select(tagName => new Tag { Name = tagName }).ToList();
                await _tagRepository.AddRangeAsync(addedTags);
                await _itemRepository.SaveChangesAsync();

                // Get current tags associated with the item
                var currentItemTags = await _itemTagRepository.getTagsByItemAsync(itemId);
                var currentTagIds = currentItemTags.Select(it => it.TagId).ToList();

                // Tags to remove (those that are no longer associated)
                var tagsToRemove = currentItemTags
                    .Where(it => !tagNames.Contains(it.Tag.Name.ToLower()))
                    .ToList();

                foreach (var itemTag in tagsToRemove)
                {
                    _itemTagRepository.RemoveTagFromItem(itemTag.ItemId, itemTag.TagId);
                }

                // Tags to associate (existing and newly added ones)
                var allTags = existingTags
                    .Where(t => tagNames.Contains(t.Name.ToLower()))
                    .Concat(addedTags)
                    .ToList();

                foreach (var tag in allTags)
                {
                    if (!currentTagIds.Contains(tag.Id))
                    {
                        var itemTag = new ItemTag
                        {
                            ItemId = itemId,
                            TagId = tag.Id
                        };
                        await _itemTagRepository.AddAsync(itemTag);
                    }
                }

                await _itemRepository.SaveChangesAsync();
                return new OperationResult(true, "Tag(s) updated and associated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tag.");
                return new OperationResult(false, "Error updating tag.");
            }
        }

        public async Task<IEnumerable<TagDto>> GetPopularTagsAsync()
        {
            try
            {
                var tar = await _tagRepository.GetTopTagsAsync();
                return _mapper.Map<IEnumerable<TagDto>>(tar);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting popular tags.");
                return null;
            }
        }
    }
}
