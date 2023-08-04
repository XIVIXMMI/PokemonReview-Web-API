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
    }
}

