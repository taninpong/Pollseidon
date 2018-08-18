using System;
using System.Collections.Generic;
using System.Text;

namespace pollseidon.models.ViewModels
{
    public class TopicVM
    {
        public string id { get; set; }
        public string TopicName { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<ChoiceVM> ChoiceList { get; set; }
        public IEnumerable<VoteVM> VoteList { get; set; }
        public int VoteCount { get; set; }
    }
}
