using System;
using System.Collections.Generic;
using System.Text;

namespace pollseidon.Models
{
    public class Vote
    {
        public string Id { get; set; }
        public string ChoiceId { get; set; }
        public string Rating { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
