namespace ASU.Services.Utilities
{
    public static class PagedItemsListUtility
    {
        private const char Separator = '=';

        public static (string searchField, string searchValue) ParseQueryFilter(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return (null, null);

            var splitCommand = filter.Split(Separator);

            if (splitCommand.Length == 2)
                return (splitCommand[0].ToLower(), splitCommand[1]);
            return (null, filter);
        }
    }
}
