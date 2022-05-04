using System;

namespace ReservationSystem.DataAccess.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }
    }
}