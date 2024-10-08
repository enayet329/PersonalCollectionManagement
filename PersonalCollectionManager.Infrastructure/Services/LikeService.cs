﻿
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

        public async Task<OperationResult> IsItemLiked(LikeRequestDto likeRequest)
        {
            try
            {
                var like = await _likeRepository.GetLikeByUserIdAndItemId(likeRequest);
                if(like == null)
                {
                    return new OperationResult(false, "Item not liked.");
                }
                return new OperationResult(like != null ? true : false, "Item liked");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in IsItemLiked");
                return new OperationResult(false, "Error in IsItemLiked");
            }
        }

        public async Task<LikeResponseDto> ToggleLike(LikeRequestDto likeRequestDto)
        {
            try
            {
                var likeToAdd = _mapper.Map<Like>(likeRequestDto);
                var like = await _likeRepository.GetLikeByUserIdAndItemId(likeRequestDto);

                if (like == null)
                {
                    await _likeRepository.AddAsync(likeToAdd);
                    var likeCount = await GetAllLikeByItemId(likeRequestDto.ItemId);
                    return new LikeResponseDto(true, "Like added successfully.", likeCount);
                }
                else
                {
                    await _likeRepository.Remove(like);
                    var likeCount = await GetAllLikeByItemId(likeRequestDto.ItemId);
                    return new LikeResponseDto(false, "Like removed successfully.", likeCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ToggleLike");
                return new LikeResponseDto(false, "Error in ToggleLike", 0);
            }
        }
    }
}
