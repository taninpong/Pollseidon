using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using pollseidon.facade.DAC;
using pollseidon.facade.Models;

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
    }
}
