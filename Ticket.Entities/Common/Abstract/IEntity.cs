using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Entities.Common.Abstract
{
    public interface IEntity
    {
    }

    public interface IEntity<out Key> : IEntity where Key : IEquatable<Key>
    {
        public Key Id { get; }
       
    }
}
