namespace Adas.Api.Security;

public static class AdasAuthorization
{
    public const string OperatorPolicy = "OperatorPolicy";
    public const string AdminPolicy = "AdminPolicy";
    
    public const string RoleClaimType = "roles";
    public const string OperatorRole = "Adas.Operator";
    public const string AdminRole = "Adas.Admin";
}
