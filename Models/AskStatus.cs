using System;
using System.Collections.Generic;

namespace API_Examen.Models;

public partial class AskStatus
{
    public int AskStatusId { get; set; }

    public string AskStatus1 { get; set; } = null!;

    public virtual ICollection<Ask> Asks { get; set; } = new List<Ask>();
}
