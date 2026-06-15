using System.Collections.Generic;

namespace School二手Platform.Repositories
{
    public interface IFavoriteRepository
    {
        bool IsFavorited(int userId, int productId);
        void AddFavorite(int userId, int productId);
        void RemoveFavorite(int userId, int productId);
        List<int> GetFavoriteProductIds(int userId);
        void ToggleFavorite(int userId, int productId);
    }
}
