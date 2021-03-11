using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace epoll.Resources
{
    public class GetAllPollResource
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }

    public class GetSinglePollResource
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<PollOptionResource> Options { get; set; }
    }

    public class SavePollResource
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MinLength(1)] // I figured that there is no reason to allow no options
        public List<string> Options { get; set; }
    }
}