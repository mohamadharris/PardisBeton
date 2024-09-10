using System;
using System.Collections.Generic;

namespace DAL.dbmodel
{
    public partial class About
    {
        public int AboutId {get;set;}
        public string? Summary { get; set; }
        public string? PageContent { get; set; }
        public string? SummaryImagePath { get; set; }
    }
}
