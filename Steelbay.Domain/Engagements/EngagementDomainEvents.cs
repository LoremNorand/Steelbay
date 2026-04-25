using Steelbay.Domain.Common.DomainEvents;
using Steelbay.Domain.Common.ValueObjects.Documents;




namespace Steelbay.Domain.Engagements;

public record EngagementCreated(EngagementId EngagementId) : DomainEvent;

public record EngagementGalleryAppended(EngagementId EngagementId, Key Key) : DomainEvent;

public record EngagementGalleryReordered(EngagementId EngagementId, int OldIndex, int NewIndex) : DomainEvent;

public record EngagementGalleryRemoved(EngagementId EngagementId, int DeletionIndex) : DomainEvent;
