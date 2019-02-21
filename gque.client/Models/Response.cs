using System.Collections.Generic;
using System.Linq;

namespace gque.client.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public List<ErrorModel> Errors { get; set; }

        public void ThrowErrors()
        {
            if (Errors != null && Errors.Any())
                throw new GraphQLException($"Message: {Errors[0].Message} Code: {Errors[0].Code}");
        }
    }
}
