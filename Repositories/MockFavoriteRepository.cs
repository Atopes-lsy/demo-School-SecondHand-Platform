using System.Collections.Generic;
using System.Linq;
using School二手Platform.Models;

namespace School二手Platform.Repositories
{
    public class MockFavoriteRepository : IFavoriteRepository
    {
        private static MockFavoriteRepository? _instance;
        public static MockFavoriteRepository Instance => _instance ??= new MockFavoriteRepository();

        private static readonly List<Favorite> _favorites = new();
        private static int _nextId = 1;

        private MockFavoriteRepository()
        {
            if (_favorites.Count == 0)
            {
                _favorites.Add(new Favorite { Id = _nextId++, UserId = 999, ProductId = 1 });
                _favorites.Add(new Favorite { Id = _nextId++, UserId = 999, ProductId = 3 });
                _favorites.Add(new Favorite { Id = _nextId++, UserId = 999, ProductId = 5 });
            }
        }

        public bool IsFavorited(int userId, int productId)
        {
            return _favorites.Any(f => f.UserId == userId && f.ProductId == productId);
        }

        public void AddFavorite(int userId, int productId)
        {
            if (!IsFavorited(userId, productId))
            {
                _favorites.Add(new Favorite { Id = _nextId++, UserId = userId, ProductId = productId });
            }
        }

        public void RemoveFavorite(int userId, int productId)
        {
            _favorites.RemoveAll(f => f.UserId == userId && f.ProductId == productId);
        }

        public List<int> GetFavoriteProductIds(int userId)
        {
            return _favorites.Where(f => f.UserId == userId).Select(f => f.ProductId).ToList();
        }

        public void ToggleFavorite(int userId, int productId)
        {
            if (IsFavorited(userId, productId))
                RemoveFavorite(userId, productId);
            else
                AddFavorite(userId, productId);
        }
    }
}
