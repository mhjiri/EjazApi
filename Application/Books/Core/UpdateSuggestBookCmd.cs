using System.Diagnostics.CodeAnalysis;

namespace Application.Books.Core
{
    public class UpdateSuggestBookCmd
    {
        public Guid Bk_ID { get; set; }
        public string Bk_Code { get; set; } = null!;
        public string Bk_Title { get; set; } = null!;
        public string Bk_Language { get; set; } = null!;
        public string Bk_Author { get; set; } = null!;
        public string Bk_Editor { get; set; } = null!;
        public string Bk_Comments { get; set; } = null!;
    }
}