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
        public double Rating { get; set; }
    }
}
