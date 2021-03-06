﻿using Contracts;
using Entities;
using Entities.ExtendedModels;
using Entities.Extensions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return FindAll().OrderBy(ow => ow.Name);
        }

        public Owner GetOwnerById(string ownerId)
        {
            return FindByCondition(owner => owner.Id.Equals(ownerId.ToString()))
                    .DefaultIfEmpty(new Owner())
                    .FirstOrDefault();
        }

        public OwnerExtended GetOwnerWithDetails(string ownerId)
        {
            return new OwnerExtended(GetOwnerById(ownerId))
            {
                Accounts = RepositoryContext.Accounts
                    .Where(a => a.OwnerId == ownerId)
            };
        }

        public void CreateOwner(Owner owner)
        {
            owner.Id = Guid.NewGuid().ToString();
            Create(owner);
            Save();
        }

        public void UpdateOwner(Owner dbOwner, Owner owner)
        {
            dbOwner.Map(owner);
            Update(dbOwner);
            Save();
        }

        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
            Save();
        }
    }
}
