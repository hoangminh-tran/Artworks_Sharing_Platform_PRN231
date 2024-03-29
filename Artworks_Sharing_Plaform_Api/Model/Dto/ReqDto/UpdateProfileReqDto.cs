﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class UpdateProfileReqDto
    {
        [StringLength(50, MinimumLength = 1)]
        public string? FirstName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string? LastName { get; set; }

        [StringLength(11, MinimumLength = 10)]
        public string? PhoneNumber { get; set; }

    }
}
