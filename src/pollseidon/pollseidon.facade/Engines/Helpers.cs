using pollseidon.models.ViewModels;
using pollseidon.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace pollseidon.facade.Engines
{
    public class Helpers
    {
        List<TopicVM> ConvertToTopicVM(List<Topic> topic)
        {
            return topic.Select(x => new TopicVM()
            {
                id = x.id,
                TopicName = x.TopicName,
                CreateBy = x.CreateBy,
                CreateDate = x.CreateDate,
                ChoiceList = x.ChoiceList.Select(c => new ChoiceVM()
                {
                    Id = c.Id,
                    Name = c.Name,
                    CraeteBy = c.CraeteBy,
                    CraeteDate = c.CraeteDate,
                    Rating = 0,
                    VoteCount = 0,
                    VoteList = new List<VoteVM>(),
                }).ToList(),
                VoteCount = 0,
            }).ToList();
        }
    }
}
