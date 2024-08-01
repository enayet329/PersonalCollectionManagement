
using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LikeService> _logger;
        public LikeService(ILikeRepository likeRepository,IMapper mapper,ILogger <LikeService> logger)
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> GetAllLikeByItemId(Guid id)
        {
            try
            {
                var likes = await _likeRepository.GetLikesByItemId(id);
                return likes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllLikeByItemId");
                return 0;
            }
            
        }

        public async Task<OperationResult> ToggleLike(LikeRequestDto likeRequestDto)
        {
            try
            {
                var likeToAdd = _mapper.Map<Like>(likeRequestDto);
                var like = await _likeRepository.GetLikeByUserIdAndItemId(likeRequestDto);

                if (like == null)
                {
                    _likeRepository.AddAsync(likeToAdd);
                    return new OperationResult(true, "Like added successfully.");
                }
                else
                {
                    _likeRepository.Remove(like);
                    return new OperationResult(true, "Like removed successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ToggleLike");
                return new OperationResult(false, "Error in ToggleLike");
            }
        }
    }
}
