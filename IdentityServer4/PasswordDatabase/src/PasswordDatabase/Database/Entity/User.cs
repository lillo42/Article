namespace PasswordDatabase.Database.Entity
{
    public class User
    {
        public virtual string Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
