using Steelbay.Domain.Accounts.AccountStatuses;
using Steelbay.Domain.Common.DomainEvents;
using Steelbay.Domain.Common.ValueObjects;




namespace Steelbay.Domain.Accounts;

public record AccountBanned(AccountId AccountId, AccountStatus ChangedStatus) : DomainEvent;

public record AccountCreated(AccountId AccountId) : DomainEvent;

public record AccountDeleted(AccountId AccountId, AccountStatus ChangedStatus) : DomainEvent;

public record AccountRestored(AccountId AccountId, AccountStatus ChangedStatus) : DomainEvent;

public record AccountSuspected(AccountId AccountId, AccountStatus ChangedStatus) : DomainEvent;

public record AccountEmailUpdated(AccountId AccountId, Email OldEmail, Email NewEmail) : DomainEvent;

public record AccountPasswordUpdated(AccountId AccountId, PasswordHash OldPassword, PasswordHash NewPassword) : DomainEvent;

public record AccountDisplayNameUpdated(AccountId AccountId, string OldDisplayName, string NewDisplayName) : DomainEvent;
