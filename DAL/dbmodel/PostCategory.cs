using System;
using System.Collections.Generic;

namespace DAL.dbmodel;

public partial class PostCategory
{
    public int PostCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
