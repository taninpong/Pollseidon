using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using pollseidon.facade.DAC;
using pollseidon.Models;
using pollseidon.models.ViewModels;

namespace pollseidon.facade.Facade
{
    public class Facade : IFacade
    {
        private IDac dac;


        public Facade(IDac dac)
        {
            this.dac = dac;
        }
        public void AddChoice(Choice choice, string topicId, string username)
        {
            var topic = dac.GetTopic(x => x.id == topicId);
            var now = DateTime.UtcNow;
            string newid = Guid.NewGuid().ToString();

            Choice newChoice = new Choice()
            {
                Id = newid,
                Name = choice.Name,
                CraeteBy = username,
            };

            var choicelist = topic.ChoiceList.ToList();

            choicelist.Add(newChoice);
            topic.ChoiceList = choicelist;

            dac.UpdateTopic(topic);
        }

        public void CreateTopic(Topic topic, string username)
        {
            string newid = Guid.NewGuid().ToString();
            var now = DateTime.UtcNow;
            topic.id = newid;
            topic.CreateDate = now;
            topic.CreateBy = username;
            topic.VoteList = new List<Vote>();

            if (topic.ChoiceList.Count() == 0)
            {
                topic.ChoiceList = new List<Choice>();
            }

            dac.CreateTopic(topic);
        }

        public IEnumerable<Topic> GetMyPoll(string username)
        {
            var poll = dac.GetTopicList(x => x.CreateBy == username).ToList();
            return poll.ToList();
        }

        public IEnumerable<Topic> GetPoll()
        {
            var poll = dac.GetTopicList(x => true).ToList();
            return poll.ToList();
        }

        IEnumerable<TopicVM> IFacade.GetMyPoll(string username)
        {
            var poll = dac.GetTopicList(x => x.CreateBy == username).ToList();
            if (poll.Count > 0)
            {
                return ConvertToTopicVM(poll.ToList());
            }
            else
            {
                return new List<TopicVM>();
            }
        }

        IEnumerable<TopicVM> IFacade.GetPoll()
        {
            var poll = dac.GetTopicList(x => true).ToList();
            if (poll.Count > 0)
            {
                return ConvertToTopicVM(poll.ToList());
            }
            else
            {
                return new List<TopicVM>();
            }
        }


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

        public TopicVM GetPollById(string id)
        {
            var poll = dac.GetTopic(x => x.id == id);

            return new TopicVM(){
                id = poll.id,
                TopicName = poll.TopicName,
                CreateBy = poll.CreateBy,
                CreateDate = poll.CreateDate,
                ChoiceList = poll.ChoiceList.Select(c => new ChoiceVM()
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
            };

        }
    }
}
