using Common.Domain;

namespace Shop.Domain.ProductAggregate.ValueObjects
{
    public class SeoData : ValueObject
    {
        public string? MetaTitle { get; private set; }
        public string? MetaDescription { get; private set; }
        public string? MetaKeywords { get; private set; }
        public string? Canonical { get; private set; }
        public bool IndexPage { get; private set; }
        public string? Schema { get; private set; }

        private SeoData()
        {
            
        }

        public SeoData(string? metaTitle, string? metaDescription, string? metaKeywords, string? canonical, bool indexPage, string? schema)
        {
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            MetaKeywords = metaKeywords;
            Canonical = canonical;
            IndexPage = indexPage;
            Schema = schema;
        }

        public static SeoData CreateEmpty()
        {
            return new SeoData();
        }
        

    }

}
