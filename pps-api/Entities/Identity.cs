public class Identity
{
    public int ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Active { get; set; }

    public Identity(int ID_, string Username_, string Password_, bool Active_)
    {
        this.ID = ID_;
        this.Username = Username_;
        this.Password = Password_;
        this.Active = Active_;
    }
}