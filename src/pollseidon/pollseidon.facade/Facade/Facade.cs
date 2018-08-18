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

        public IEnumerable<TopicVM> GetMyPoll(string username)
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

        public IEnumerable<TopicVM> GetPoll()
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
                    Rating = x.VoteList.Where(y => y.ChoiceId == c.Id).Average(z => z.Rating).ToString("#,###.00"),
                    VoteCount = x.VoteList.Where(y => y.ChoiceId == c.Id).Count(),
                    VoteList = x.VoteList.Where(vote => vote.ChoiceId == c.Id).Select(v => new VoteVM()
                    {
                        Id = v.Id,
                        CreateDate = v.CreateDate,
                        ChoiceId = v.ChoiceId,
                        Rating = v.Rating,
                        UserName = v.UserName
                    }).ToList(),
                }).ToList(),
                VoteCount = x.VoteList.Count(),
            }).ToList();
        }

        public TopicVM GetPollById(string id)
        {
            var poll = dac.GetTopic(x => x.id == id);

            return new TopicVM()
            {
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
                    Rating = poll.VoteList.Where(y => y.ChoiceId == c.Id).Average(z => z.Rating).ToString("#,###.00"),
                    VoteCount = poll.VoteList.Where(y => y.ChoiceId == c.Id).Count(),
                    VoteList = poll.VoteList.Where(vote => vote.ChoiceId == c.Id).Select(v => new VoteVM()
                    {
                        Id = v.Id,
                        CreateDate = v.CreateDate,
                        ChoiceId = v.ChoiceId,
                        Rating = v.Rating,
                        UserName = v.UserName
                    }).ToList(),
                }).ToList(),
                VoteCount = poll.VoteList.Count(),
            };

        }

        public void VoteAndRating(string choiceId, int rating, string topicId, string username)
        {
            var poll = dac.GetTopic(x => x.id == topicId);

            var myvote = poll.VoteList.Where(x => x.UserName == username && x.ChoiceId == choiceId).FirstOrDefault();
            if (myvote != null)
            {
                myvote.Rating = rating;
            }
            else
            {
                var newid = Guid.NewGuid().ToString();
                var now = DateTime.UtcNow;
                Vote newvote = new Vote()
                {
                    Id = newid,
                    ChoiceId = choiceId,
                    CreateDate = now,
                    Rating = rating,
                    UserName = username,
                };

                var voteList = poll.VoteList.ToList();
                voteList.Add(newvote);
                poll.VoteList = voteList;
                dac.UpdateTopic(poll);
            }
        }

        public IEnumerable<VoteVM> GetVoteUserList(string choiceId, string topicId, string username)
        {
            var poll = dac.GetTopic(x => x.id == topicId);

            return poll.VoteList.Select(x => new VoteVM()
            {
                Id = x.Id,
                Rating = x.Rating,
                UserName = x.UserName,
                CreateDate = x.CreateDate,
                ChoiceId = x.ChoiceId
            });
        }


    }
}
