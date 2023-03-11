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
            public const int MinNicknameLength = 4;
            public const int MaxNicknameLength = 16;

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

        public static class Notification
        {
            public const string SuccessMessageTempBagKey = "SuccessMessage";
        }
    }
}
