using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class LikeResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int LikeCount { get; set; }

        public LikeResponseDto(bool success, string message, int likeCount)
        {
            Success = success;
            Message = message;
            LikeCount = likeCount;
        }
    }

}
