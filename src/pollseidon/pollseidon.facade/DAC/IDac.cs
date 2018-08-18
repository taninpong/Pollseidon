using pollseidon.facade.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace pollseidon.facade.DAC
{
    public interface IDac
    {
        Topic GetTopic(Expression<Func<Topic, bool>> expression);
        IEnumerable<Topic> GetTopicList(Expression<Func<Topic, bool>> expression);
        void CreateTopic(Topic newTopic);
        void UpdateTopic(Topic topic);
    }
}
