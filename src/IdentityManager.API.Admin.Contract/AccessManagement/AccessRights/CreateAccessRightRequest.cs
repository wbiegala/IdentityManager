namespace IdentityManager.API.Admin.Contract.AccessManagement.AccessRights
{
    public class CreateAccessRightRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
