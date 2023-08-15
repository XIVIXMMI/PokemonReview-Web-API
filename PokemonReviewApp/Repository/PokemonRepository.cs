using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class PokemonRepository : IPokemonRepository
	{
		private readonly ApplicationDbContext _context;
		public PokemonRepository(ApplicationDbContext context)
		{
			_context = context;
		}

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

#pragma warning disable CS8601 // Possible null reference assignment.
            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };
#pragma warning restore CS8601 // Possible null reference assignment.
            _context.Add(pokemonOwner);

#pragma warning disable CS8601 // Possible null reference assignment.
            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };
#pragma warning restore CS8601 // Possible null reference assignment.
            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public Pokemon GetPokemon(string name)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);
            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
		{
			return _context.Pokemons.OrderBy(p => p.Id).ToList();
		}

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }
    }
}

