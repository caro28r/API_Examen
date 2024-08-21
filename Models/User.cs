using System;
using System.Collections.Generic;

namespace API_Examen.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string UserPassword { get; set; } = null!;

    public int StrikeCount { get; set; }

    public string BackUpEmail { get; set; } = null!;

    public string? JobDescription { get; set; }

    public int UserStatusId { get; set; }

    public int CountryId { get; set; }

    public int UserRoleId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Ask> Asks { get; set; } = new List<Ask>();

    public virtual ICollection<Chat> ChatReceivers { get; set; } = new List<Chat>();

    public virtual ICollection<Chat> ChatSenders { get; set; } = new List<Chat>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual UserRole UserRole { get; set; } = null!;

    public virtual UserStatus UserStatus { get; set; } = null!;
}
