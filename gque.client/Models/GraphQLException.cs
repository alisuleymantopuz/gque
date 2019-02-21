using System;

namespace gque.client.Models
{
    public class GraphQLException : ApplicationException
    {
        public GraphQLException(string message) : base(message)
        {
        }
    }
}
