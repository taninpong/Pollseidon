
using pollseidon.models.ViewModels;
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
        IEnumerable<TopicVM> GetPoll();
        IEnumerable<TopicVM> GetMyPoll(string username);
        TopicVM GetPollById(string id);
        void VoteAndRating(string choiceId, int rateing, string topicId, string username);
        IEnumerable<VoteVM> GetVoteUserList(string choiceId, string topicId, string username);
    }
}
