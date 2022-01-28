using System;

namespace Source.Scripts.Core
{
    public static class Hash
    {
        public static Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}