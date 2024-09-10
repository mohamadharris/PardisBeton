using System;
using System.Collections.Generic;


namespace DAL.dbmodel
{
    public partial class Personnel
    {
        public int PersonnelId { get; set; }
        public string Name { get; set; } 
        public string Family { get; set; }
        public string? Sex { get; set; }
        public string? Nickname { get; set; }
        public string? ImagePath { get; set; }
        public string? Position { get; set; }
        public string? Description { get; set; }
    }
}
