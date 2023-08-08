using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
		{
            _context = context;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories.Where(c => c.CategoryId == categoryId)
                .Select(c => c.Pokemon)
                .ToList();
        }
    }
}

