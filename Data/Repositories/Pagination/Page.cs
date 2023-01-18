using Azure;

namespace EF.Generic_Query.API.Data.Repositories.Pagination
{
    public class Page<T>
    {
        public Page(int total, IEnumerable<T> content, Pageable pageable)
        {
            Number = pageable.Page;
            Size = pageable.Size;

            Content = content;

            TotalElements = total;

            TotalPages = (int)Math.Ceiling((double)TotalElements / pageable.Size);

            HasPrevious = Number > 1;
            HasNext = Number < TotalPages;

            IsFirst = Number == 1;
            IsLast = Number == TotalPages;
        }

        public IEnumerable<T> Content { get; }

        public int TotalPages { get; }

        public int TotalElements { get; }

        public int Number { get; }

        public int Size { get; }

        public bool HasPrevious { get; }

        public bool HasNext { get; }

        public bool IsFirst { get; }

        public bool IsLast { get; }
    }
}
