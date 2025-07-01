
using pps_api.Models;

namespace pps_api.Entities;

public class Identity
{
    public int ID { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public bool? Active { get; set; }
    public string? Role { get; set; }
    public DateTime LastUpdated { get; set; }
    public DateTime Created { get; set; }
    public bool? IsPasswordStale { get; set; }

    public Identity()
    {

    }

    public Identity(int ID_, string Username_, string PasswordHash_, bool Active_, string role_, DateTime LastUpdated_, DateTime Created_, bool? isPasswordStale_)
    {
        this.ID = ID_;
        this.Username = Username_;
        this.PasswordHash = PasswordHash_;
        this.Active = Active_;
        this.Role = role_;
        this.LastUpdated = LastUpdated_;
        this.Created = Created_;
        this.IsPasswordStale = isPasswordStale_;
    }

    public Identity(Credentials creds)
    {
        if (creds == null)
        {
            throw new ArgumentNullException(nameof(creds), "Credentials cannot be null.");
        }
        if (string.IsNullOrEmpty(creds.Username))
        {
            throw new ArgumentException("Identity(Credentials) constructor: Username cannot be null or empty.");
        }

        this.ID = 0;                                // Set or Retrieved from the database.
        this.Username = creds.Username;
        this.Active = null;                         // Retrieved from the database.
        this.Role = null;                           // Retrieved from the database.
        this.LastUpdated = DateTime.UtcNow;
        this.Created = DateTime.UtcNow;             // Retrieved from the database.
        this.IsPasswordStale = null;                // Retrieved from the database.
    }
}