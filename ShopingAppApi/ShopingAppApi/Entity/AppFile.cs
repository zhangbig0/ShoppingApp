using System;

namespace ShoppingAppApi.Entity
{
    public class AppFile
    {
        public Guid Id { get; set; }

        public byte[] Content { get; set; }
    }
}