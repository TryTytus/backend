using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend;

public partial class Post
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int LikesCount { get; set; }

    public int ViewsCount { get; set; }

    public int CommentsCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }


    public int UserId { get; set; }
    
    [ForeignKey("UserId" )]
    public AspNetUser User { get; set; } = null!;
}
