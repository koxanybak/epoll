using epoll.Domain.Models;

namespace epoll.Domain.Services.Communication
{
    public class SavePollResponse : BaseResponse
    {
        public Poll Poll { get; private set; }

        private SavePollResponse(bool success, string message, Poll poll) : base(success, message)
        {
            Poll = poll;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="poll">Saved poll.</param>
        /// <returns>Response.</returns>
        public SavePollResponse(Poll poll) : this(true, string.Empty, poll)
        {}

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SavePollResponse(string message) : this(false, message, null)
        {}
    }
}