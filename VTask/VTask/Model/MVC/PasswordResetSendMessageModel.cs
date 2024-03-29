﻿using System.ComponentModel.DataAnnotations;

namespace VTask.Model.MVC
{
    public class PasswordResetSendMessageModel
    {
        [Required]
        [StringLength(
            Constants.User.MaxUsernameLength,
            MinimumLength = Constants.User.MinUsernameLength,
            ErrorMessage = Constants.Format.StringPropertyInvalidLengthMessageFormat
        )]
        public string Username { get; set; } = string.Empty;
    }
}
