using Steelbay.Domain.Common.DomainEvent;
using Steelbay.Domain.Common.ValueObjects;




namespace Steelbay.Domain.Account;

public record AccountBanned(AccountId AccountId) : DomainEvent;

public record AccountCreated(AccountId AccountId) : DomainEvent;

public record AccountDeleted(AccountId AccountId) : DomainEvent;

public record AccountRestored(AccountId AccountId) : DomainEvent;

public record AccountSuspected(AccountId AccountId) : DomainEvent;

public record AccountEmailUpdated(AccountId AccountId, Email OldEmail, Email NewEmail) : DomainEvent;
