using System.Collections.Generic;

namespace epoll.Domain.Models
{
    public class Poll
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<PollOption> Options { get; set; } = new List<PollOption>();
    }

    public class PollOption
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Votes { get; set; }

        public long PollId  { get; set; }
        public Poll Poll  { get; set; }
    }
}