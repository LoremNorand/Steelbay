using Steelbay.Domain.Common.DomainEvents;
using Steelbay.Domain.Common.ValueObjects;
using Steelbay.Domain.Common.ValueObjects.Documents;




namespace Steelbay.Domain.Engagements;

public record EngagementCreated(EngagementId EngagementId) : DomainEvent;

public record EngagementGalleryAppended(EngagementId EngagementId, Key Key) : DomainEvent;

public record EngagementGalleryReordered(EngagementId EngagementId, int OldIndex, int NewIndex) : DomainEvent;

public record EngagementGalleryReduced(EngagementId EngagementId, int DeletionIndex) : DomainEvent;

public record EngagementNameUpdated(EngagementId EngagementId, string OldName, string NewName) : DomainEvent;

public record EngagementDescriptionUpdated(EngagementId EngagementId, string OldDescription, string NewDescription) : DomainEvent;

public record EngagementPriceUpdated(EngagementId EngagementId, Price OldPrice, Price NewPrice) : DomainEvent;

public record EngagementVisibilityToggled(EngagementId EngagementId, bool IsVisible) : DomainEvent;
