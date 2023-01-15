namespace DmailApp.Domain.Enums;

public enum ResponseResultType
{
    Success,
    NotFound,
    AlreadyExists,
    NoChanges,
    Error,
    ValidationError,
    Warning
}