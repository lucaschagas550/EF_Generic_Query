namespace EF.Generic_Query.API.Data.Repositories.Pagination
{
    public class Pageable
    {
        private static readonly int MAX_SIZE = 500;

        private int _page = 1;
        private int _size = 10;

        public int Page
        {
            get => _page;
            set => _page = value > 0 ? value : _page;
        }

        public int Size
        {
            get => _size;
            set => _size = value > 0 && value <= MAX_SIZE ? value : _size;
        }
    }
}