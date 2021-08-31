using EPiServer.Core;

namespace EpiContentMgmtDemo.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
