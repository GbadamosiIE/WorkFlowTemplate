namespace Factory.Database
{
    public static class UserDb
    {
        public static ICollection<UserInfo> userInfos { get; set; } = new List<UserInfo>();
        public static ICollection<Video> videos { get; set; } = new List<Video>();
    }
}