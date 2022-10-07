﻿namespace Locatudo.Shared.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
