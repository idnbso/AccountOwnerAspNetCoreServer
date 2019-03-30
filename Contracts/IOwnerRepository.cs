using Entities.ExtendedModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IOwnerRepository : IRepositoryBase<Owner>
    {
        IEnumerable<Owner> GetAllOwners();
        Owner GetOwnerById(string ownerId);
        OwnerExtended GetOwnerWithDetails(string ownerId);
        void CreateOwner(Owner owner);
        void UpdateOwner(Owner dbOwner, Owner owner);
        void DeleteOwner(Owner owner);
    }
}
