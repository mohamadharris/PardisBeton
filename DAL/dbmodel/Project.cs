using System;
using System.Collections.Generic;

namespace DAL.dbmodel;

public partial class Project
{
    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public string? ImagePath { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }
    public string? Client { get; set; }

    public DateTime? RegDate { get; set; }

    public bool? Active { get; set; }

    public bool? ShowAtHome { get; set; }

    public int? Priority { get; set; }
}
