using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class ReviewRepository : IReviewRepository 
	{
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
		{
            _context = context;
        }

        public Review GetReview(int reviewId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();

        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }
    }
}

