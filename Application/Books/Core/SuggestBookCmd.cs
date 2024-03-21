namespace Application.Books.Core
{
    public class SuggestBookCmd
    {
        public Guid Bk_ID { get; set; }
        public string Bk_Code { get; set; }
        public string Bk_Title { get; set; }
        public string Bk_Language { get; set; }
        public string Bk_Author { get; set; }
        public string Bk_Editor { get; set; }
        public string Bk_Comments { get; set; }
    }
}