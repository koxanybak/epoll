using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace epoll.Extensions
{
    public static class ModelStateExtensions
    {
        // Convert 'ModelState' errors into a list of strings to send to the client.
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                            .Select(m => m.ErrorMessage)
                            .ToList();
        }
    }
}