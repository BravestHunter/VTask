using Microsoft.SqlServer.Server;

namespace VTask
{
    public static class Constants
    {
        public static class User
        {
            public const int MinUsernameLength = 4;
            public const int MaxUsernameLength = 32;
            public const int MinPasswordLength = 6;
            public const int MaxPasswordLength = 16;
            public const int MinEmailLength = 3;
            public const int MaxEmailLength = 320;
            public const int MinNicknameLength = MinUsernameLength;
            public const int MaxNicknameLength = MaxUsernameLength;

            public const string DefaultUsername = "Username";
        }

        public static class Task
        {
            public const int MinTitleLength = 1;
            public const int MaxTitleLength = 64;
            public const int MinDescriptionLength = 0;
            public const int MaxDescriptionLength = 1024;

            public const string DefaultTitle = "Title";
        }

        public static class Format
        {
            public const string StringPropertyInvalidLengthMessageFormat = "Length should be between {2} and {1}";
        }

        public static class Notification
        {
            public const string SuccessMessageTempBagKey = "SuccessMessage";
        }
    }
}
