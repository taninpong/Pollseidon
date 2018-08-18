using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using FluentAssertions;
using FluentAssertions.Collections;
using pollseidon.Models;
using pollseidon.facade.Facade;
using pollseidon.facade.DAC;
using Moq;
using System.Linq.Expressions;
using pollseidon.models.ViewModels;

namespace pollseidon.Tests
{
    public class PollSeidonTests
    {
        Mock<IDac> dac { get; set; }
        Facade facade { get; set; }

        public PollSeidonTests()
        {
            var mock = new MockRepository(MockBehavior.Strict);

            dac = mock.Create<IDac>();

            facade = new Facade(dac.Object);

        }
        [Theory(DisplayName = "ดึงข้อมูล poll พน้อมคำนวณ rating")]
        [InlineData()]
        public void GetPollSuccess()
        {
            List<Topic> topic = new List<Topic>()
            {
                new Topic(){
                    id ="01",
                    TopicName ="โหวตข้าว",
                    ChoiceList = new List<Choice>(){
                        new Choice(){Id ="01",Name ="กะเพาหมูกรอบ" ,CraeteBy="ake", CraeteDate = DateTime.MinValue},
                        new Choice(){Id ="02",Name ="กะเพาหมูสับ" ,CraeteBy="ake", CraeteDate = DateTime.MinValue},
                    },
                    CreateBy ="ake",
                    CreateDate = DateTime.UtcNow,
                    VoteList = new List<Vote>(){
                        new Vote(){ Id ="01",ChoiceId = "01" ,Rating = 5, UserName ="ake" ,CreateDate = DateTime.MinValue},
                        new Vote(){ Id ="02",ChoiceId = "01" ,Rating = 4, UserName ="sorry" ,CreateDate = DateTime.MinValue},
                        new Vote(){ Id ="03",ChoiceId = "02" ,Rating = 3, UserName ="pao" ,CreateDate = DateTime.MinValue},
                        new Vote(){ Id ="04",ChoiceId = "01" ,Rating = 5, UserName ="captain" ,CreateDate = DateTime.MinValue},
                    },

                }

            };

            dac.Setup(dac => dac.GetTopicList(It.IsAny<Expression<Func<Topic, bool>>>()))
    .Returns<Expression<Func<Topic, bool>>>((expression) => topic.Where(expression.Compile()));


            var pollList = facade.GetPoll();

            var expected = new List<TopicVM>()
            {
                new TopicVM(){
                    id ="01",
                    CreateBy ="ake",
                    CreateDate = DateTime.UtcNow,
                    TopicName ="โหวตข้าว",
                    VoteCount = 4,
                    ChoiceList = new List<ChoiceVM>(){
                        new ChoiceVM(){Id ="01",Name ="กะเพาหมูกรอบ" ,CraeteBy="ake", CraeteDate = DateTime.MinValue,Rating = "4.67",VoteCount =3 ,
                            VoteList = new List<VoteVM>(){
                                new VoteVM(){ Id ="01",ChoiceId = "01" ,Rating = 5, UserName ="ake" ,CreateDate = DateTime.MinValue },
                                new VoteVM(){ Id ="02",ChoiceId = "01" ,Rating = 4, UserName ="sorry" ,CreateDate = DateTime.MinValue},
                                new VoteVM(){ Id ="04",ChoiceId = "01" ,Rating = 5, UserName ="captain" ,CreateDate = DateTime.MinValue},
                            }},
                        new ChoiceVM(){Id ="02",Name ="กะเพาหมูสับ" ,CraeteBy="ake", CraeteDate = DateTime.MinValue,Rating = "3.00",VoteCount =1 ,
                            VoteList = new List<VoteVM>(){
                                new VoteVM(){ Id ="03",ChoiceId = "02" ,Rating = 3, UserName ="pao" ,CreateDate = DateTime.MinValue},
                            }},


                    },

                },
            };

            expected.Should().BeEquivalentTo(pollList, option =>
             option.Excluding(x => x.CreateDate));
        }

        [Theory(DisplayName = "ดึงข้อมูล poll byid พน้อมคำนวณ rating")]
        [InlineData("01")]
        public void GetPollByIdSuccess(string pollId)
        {
            List<Topic> topic = new List<Topic>()
            {
                new Topic(){
                    id ="01",
                    TopicName ="โหวตข้าว",
                    ChoiceList = new List<Choice>(){
                        new Choice(){Id ="01",Name ="กะเพาหมูกรอบ" ,CraeteBy="ake", CraeteDate = DateTime.MinValue},
                        new Choice(){Id ="02",Name ="กะเพาหมูสับ" ,CraeteBy="ake", CraeteDate = DateTime.MinValue},
                    },
                    CreateBy ="ake",
                    CreateDate = DateTime.UtcNow,
                    VoteList = new List<Vote>(){
                        new Vote(){ Id ="01",ChoiceId = "01" ,Rating = 5, UserName ="ake" ,CreateDate = DateTime.MinValue},
                        new Vote(){ Id ="02",ChoiceId = "01" ,Rating = 4, UserName ="sorry" ,CreateDate = DateTime.MinValue},
                        new Vote(){ Id ="03",ChoiceId = "02" ,Rating = 3, UserName ="pao" ,CreateDate = DateTime.MinValue},
                        new Vote(){ Id ="04",ChoiceId = "01" ,Rating = 5, UserName ="captain" ,CreateDate = DateTime.MinValue},
                    },

                }

            };

            dac.Setup(dac => dac.GetTopic(It.IsAny<Expression<Func<Topic, bool>>>()))
    .Returns<Expression<Func<Topic, bool>>>((expression) => topic.FirstOrDefault(expression.Compile()));


            var poll = facade.GetPollById(pollId);

            var expected = new TopicVM()
            {
                id = "01",
                CreateBy = "ake",
                CreateDate = DateTime.UtcNow,
                TopicName = "โหวตข้าว",
                VoteCount = 4,
                ChoiceList = new List<ChoiceVM>(){
                        new ChoiceVM(){Id ="01",Name ="กะเพาหมูกรอบ" ,CraeteBy="ake", CraeteDate = DateTime.MinValue,Rating = "4.67",VoteCount =3 ,
                            VoteList = new List<VoteVM>(){
                                new VoteVM(){ Id ="01",ChoiceId = "01" ,Rating = 5, UserName ="ake" ,CreateDate = DateTime.MinValue },
                                new VoteVM(){ Id ="02",ChoiceId = "01" ,Rating = 4, UserName ="sorry" ,CreateDate = DateTime.MinValue},
                                new VoteVM(){ Id ="04",ChoiceId = "01" ,Rating = 5, UserName ="captain" ,CreateDate = DateTime.MinValue},
                            }},
                        new ChoiceVM(){Id ="02",Name ="กะเพาหมูสับ" ,CraeteBy="ake", CraeteDate = DateTime.MinValue,Rating = "3.00",VoteCount =1 ,
                            VoteList = new List<VoteVM>(){
                                new VoteVM(){ Id ="03",ChoiceId = "02" ,Rating = 3, UserName ="pao" ,CreateDate = DateTime.MinValue},
                            }},


                    },
            };


            expected.Should().BeEquivalentTo(poll, option =>
             option.Excluding(x => x.CreateDate));
        }



    }
}
