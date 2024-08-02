
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using AutoMapper;
using PersonalCollectionManager.Application.DTOs;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public TagService(ITagRepository tagRepository,IMapper mapper,ILogger<TagService> logger)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult> AddTagAsync(TagRequestDto tag)
        {
            try
            {
                var tagEntity = _mapper.Map<Tag>(tag);
                await _tagRepository.AddAsync(tagEntity);

                return new OperationResult(true, "Tag added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding tag.");
                return new OperationResult(false, "Error adding tag.");
            }
        }

        public async Task<OperationResult> DeleteTagAsync(Guid id)
        {
            try
            {
                var tagEntity = await _tagRepository.GetByIdAsync(id);
                if (tagEntity != null)
                {
                    await _tagRepository.Remove(tagEntity); 
                    return new OperationResult(true, "Tag deleted successfully.");
                }

                return new OperationResult(false, "Tag not found.");
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

        public async Task<OperationResult> UpdateTagAsync(TagDto tag)
        {
            try
            {
                var tagEntity = _mapper.Map<Tag>(tag);
                await _tagRepository.Update(tagEntity);

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
