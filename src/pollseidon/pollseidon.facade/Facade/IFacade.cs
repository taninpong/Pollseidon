
using pollseidon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace pollseidon.facade.Facade
{
    public interface IFacade
    {
        void CreateTopic(Topic topic, string username);
        void AddChoice(Choice choice, string topicId, string username);
        IEnumerable<Topic> GetPoll();
        IEnumerable<Topic> GetMyPoll(string username);
    }
}
