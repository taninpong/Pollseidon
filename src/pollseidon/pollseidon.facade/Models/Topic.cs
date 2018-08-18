using System;
using System.Collections.Generic;
using System.Text;

namespace pollseidon.facade.Models
{
    public class Topic
    {
        public string id { get; set; }
        public string TopicName { get; set; }
        public string CreateBy { get; set; }    
        public DateTime CreateDate { get; set; }
        public IEnumerable<Choice> ChoiceList { get; set; }
        public IEnumerable<Vote> VoteList { get; set; }
    }
}
