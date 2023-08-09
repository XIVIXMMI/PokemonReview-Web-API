using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class OwnerRepository : IOwnerRepository
	{
        private readonly ApplicationDbContext _context;

        public OwnerRepository(ApplicationDbContext context)
		{
            _context = context;
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public Owner GetOwner(int ownerId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return _context.PokemonOwners.Where(p => p.Pokemon.Id == pokeId).Select(o => o.Owner).ToList();

        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(p => p.Owner.Id == ownerId).Select(p => p.Pokemon).ToList();

        }

        public bool OwnerExists(int ownerId)
        {
            return _context.PokemonOwners.Any(o => o.OwnerId == ownerId);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

