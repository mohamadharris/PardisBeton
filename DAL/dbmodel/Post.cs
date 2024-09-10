using System;
using System.Collections.Generic;

namespace DAL.dbmodel;

public partial class Post
{
    public int PostId { get; set; }

    public int? PostCategoryId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime RegDate { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public bool? Active { get; set; }

    public virtual PostCategory? PostCategory { get; set; }
}
