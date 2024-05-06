namespace SimpleTask.Models.ViewModels
{
    public static class CurrentUser
    {
        public static string? Name { get; set; }
        public static int? Id { get; set; }
        public static bool IsAuthenticated
        {
            get
            {
                return Name != null;
            }
        }
    }
}
