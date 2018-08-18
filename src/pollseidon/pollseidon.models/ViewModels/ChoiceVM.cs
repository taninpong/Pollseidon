using System;
using System.Collections.Generic;
using System.Text;

namespace pollseidon.models.ViewModels
{
    public class ChoiceVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CraeteBy { get; set; }
        public DateTime CraeteDate { get; set; }
        public string Rating { get; set; }
        public int VoteCount { get; set; }
        public IEnumerable<VoteVM> VoteList { get; set; }
    }
}
