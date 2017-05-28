using System.Data.Entity.Migrations;

namespace Lx.Identity.Persistence.Migrations
{
    public partial class Created : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.ClientClaims",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ClientKey = c.Guid(false),
                        Type = c.String(false, 250),
                        Value = c.String(false, 250),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.ClientCorsOrigins",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ClientKey = c.Guid(false),
                        Origin = c.String(false, 150),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.ClientCustomGrantTypes",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ClientKey = c.Guid(false),
                        GrantType = c.String(false, 250),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.ClientIdPRestrictions",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ClientKey = c.Guid(false),
                        Scope = c.String(false, 200),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.ClientPostLogoutRedirectUris",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ClientKey = c.Guid(false),
                        Uri = c.String(maxLength: 2000),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.ClientRedirectUris",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ClientKey = c.Guid(false),
                        Uri = c.String(false, 2000),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.Clients",
                    c => new
                    {
                        Id = c.Long(false, true),
                        Enabled = c.Boolean(false),
                        ClientId = c.String(false, 200),
                        ClientName = c.String(false, 200),
                        ClientUri = c.String(maxLength: 2000),
                        LogoUri = c.String(),
                        RequireConsent = c.Boolean(false),
                        AllowRememberConsent = c.Boolean(false),
                        AllowAccessTokensViaBrowser = c.Boolean(false),
                        Flow_Value = c.Int(false),
                        Flow_Name = c.String(maxLength: 128),
                        AllowClientCredentialsOnly = c.Boolean(false),
                        LogoutUri = c.String(),
                        LogoutSessionRequired = c.Boolean(false),
                        RequireSignOutPrompt = c.Boolean(false),
                        AllowAccessToAllScopes = c.Boolean(false),
                        IdentityTokenLifetime = c.Int(false),
                        AccessTokenLifetime = c.Int(false),
                        AuthorizationCodeLifetime = c.Int(false),
                        AbsoluteRefreshTokenLifetime = c.Int(false),
                        SlidingRefreshTokenLifetime = c.Int(false),
                        RefreshTokenUsage_Value = c.Int(false),
                        RefreshTokenUsage_Name = c.String(maxLength: 128),
                        UpdateAccessTokenOnRefresh = c.Boolean(false),
                        RefreshTokenExpiration_Value = c.Int(false),
                        RefreshTokenExpiration_Name = c.String(maxLength: 128),
                        AccessTokenType_Value = c.Int(false),
                        AccessTokenType_Name = c.String(maxLength: 128),
                        EnableLocalLogin = c.Boolean(false),
                        IncludeJwtId = c.Boolean(false),
                        AlwaysSendClientClaims = c.Boolean(false),
                        PrefixClientClaims = c.Boolean(false),
                        AllowAccessToAllGrantTypes = c.Boolean(false),
                        UserProfileOriginator = c.String(maxLength: 100),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ClientId, unique: true)
                .Index(t => t.ClientName, unique: true)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.ClientScopes",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ClientKey = c.Guid(false),
                        Scope = c.String(false, 200),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.ClientSecrets",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ClientKey = c.Guid(false),
                        Value = c.String(maxLength: 2000),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.Consents",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ClientId = c.String(false, 200),
                        Subject = c.String(maxLength: 100),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.ScopeClaims",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ScopeKey = c.Guid(false),
                        Name = c.String(false, 200),
                        Description = c.String(maxLength: 1000),
                        AlwaysIncludeInIdToken = c.Boolean(false),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.Scopes",
                    c => new
                    {
                        Id = c.Long(false, true),
                        Enabled = c.Boolean(false),
                        Name = c.String(false, 200),
                        DisplayName = c.String(maxLength: 200),
                        Description = c.String(maxLength: 1000),
                        Required = c.Boolean(false),
                        Emphasize = c.Boolean(false),
                        Type = c.Int(false),
                        IncludeAllClaimsForUser = c.Boolean(false),
                        ClaimsRule = c.String(maxLength: 200),
                        ShowInDiscoveryDocument = c.Boolean(false),
                        AllowUnrestrictedIntrospection = c.Boolean(false),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.ScopeSecrets",
                    c => new
                    {
                        Id = c.Long(false, true),
                        ScopeKey = c.Guid(false),
                        Description = c.String(maxLength: 1000),
                        Expiration = c.DateTimeOffset(precision: 7),
                        Type = c.String(maxLength: 250),
                        Value = c.String(false, 250),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.UserProfiles",
                    c => new
                    {
                        Id = c.Long(false, true),
                        UserProfileOriginator = c.String(maxLength: 100),
                        Body = c.String(),
                        UserKey = c.Guid(false),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);

            CreateTable(
                    "dbo.Users",
                    c => new
                    {
                        Id = c.Long(false, true),
                        HashedPassword = c.String(maxLength: 200),
                        VerificationPurpose_Value = c.Int(false),
                        VerificationPurpose_Name = c.String(maxLength: 128),
                        ResetPasswordMethod_Value = c.Int(false),
                        ResetPasswordMethod_Name = c.String(maxLength: 128),
                        HashedVerificationCode = c.String(maxLength: 1000),
                        TimeVerificationCodeSent = c.DateTimeOffset(precision: 7),
                        TimeVerificationCodeExpires = c.DateTimeOffset(precision: 7),
                        TimeTemporaryPasswordSent = c.DateTimeOffset(precision: 7),
                        PriorUserState_Value = c.Int(false),
                        PriorUserState_Name = c.String(maxLength: 128),
                        TimeLockedOut = c.DateTimeOffset(precision: 7),
                        UserState_Value = c.Int(false),
                        UserState_Name = c.String(maxLength: 128),
                        Name_FamilyName = c.String(maxLength: 100),
                        Name_GivenName = c.String(maxLength: 100),
                        Name_MiddleName = c.String(maxLength: 100),
                        IsAdmin = c.Boolean(false),
                        Username = c.String(maxLength: 20),
                        Email_Address = c.String(maxLength: 200),
                        Email_Verified = c.Boolean(false),
                        MobileNumber_LocalNumberWithAreaCodeInDigits = c.String(maxLength: 100),
                        MobileNumber_LocalNumberWithAreaCode = c.String(maxLength: 100),
                        MobileNumber_CountryCode = c.Int(),
                        MobileNumber_CountryName = c.String(maxLength: 100),
                        MobileNumber_Verified = c.Boolean(false),
                        Nickname = c.String(),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7),
                        Key = c.Guid(false),
                        Avatar_Id = c.Long()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Avatars", t => t.Avatar_Id)
                .Index(t => t.Username, unique: true)
                .Index(t => t.Email_Address, unique: true)
                .Index(t => t.MobileNumber_LocalNumberWithAreaCodeInDigits, unique: true)
                .Index(t => t.Key, unique: true)
                .Index(t => t.Avatar_Id);

            CreateTable(
                    "dbo.Avatars",
                    c => new
                    {
                        Id = c.Long(false, true),
                        UriRelative = c.String(maxLength: 100),
                        UriDefault = c.String(maxLength: 200),
                        Description = c.String(maxLength: 500),
                        Width = c.Int(),
                        Height = c.Int(),
                        FullFilePath = c.String(),
                        UserKey = c.Guid(false),
                        Deactivated = c.Boolean(false),
                        TimeCreated = c.DateTimeOffset(precision: 7),
                        TimeModified = c.DateTimeOffset(precision: 7)
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Users", "Avatar_Id", "dbo.Avatars");
            DropIndex("dbo.Users", new[] {"Avatar_Id"});
            DropIndex("dbo.Users", new[] {"Key"});
            DropIndex("dbo.Users", new[] {"MobileNumber_LocalNumberWithAreaCodeInDigits"});
            DropIndex("dbo.Users", new[] {"Email_Address"});
            DropIndex("dbo.Users", new[] {"Username"});
            DropIndex("dbo.UserProfiles", new[] {"Key"});
            DropIndex("dbo.ScopeSecrets", new[] {"Key"});
            DropIndex("dbo.Scopes", new[] {"Key"});
            DropIndex("dbo.Scopes", new[] {"Name"});
            DropIndex("dbo.ScopeClaims", new[] {"Key"});
            DropIndex("dbo.Consents", new[] {"Key"});
            DropIndex("dbo.ClientSecrets", new[] {"Key"});
            DropIndex("dbo.ClientScopes", new[] {"Key"});
            DropIndex("dbo.Clients", new[] {"Key"});
            DropIndex("dbo.Clients", new[] {"ClientName"});
            DropIndex("dbo.Clients", new[] {"ClientId"});
            DropIndex("dbo.ClientRedirectUris", new[] {"Key"});
            DropIndex("dbo.ClientPostLogoutRedirectUris", new[] {"Key"});
            DropIndex("dbo.ClientIdPRestrictions", new[] {"Key"});
            DropIndex("dbo.ClientCustomGrantTypes", new[] {"Key"});
            DropIndex("dbo.ClientCorsOrigins", new[] {"Key"});
            DropIndex("dbo.ClientClaims", new[] {"Key"});
            DropTable("dbo.Avatars");
            DropTable("dbo.Users");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.ScopeSecrets");
            DropTable("dbo.Scopes");
            DropTable("dbo.ScopeClaims");
            DropTable("dbo.Consents");
            DropTable("dbo.ClientSecrets");
            DropTable("dbo.ClientScopes");
            DropTable("dbo.Clients");
            DropTable("dbo.ClientRedirectUris");
            DropTable("dbo.ClientPostLogoutRedirectUris");
            DropTable("dbo.ClientIdPRestrictions");
            DropTable("dbo.ClientCustomGrantTypes");
            DropTable("dbo.ClientCorsOrigins");
            DropTable("dbo.ClientClaims");
        }
    }
}