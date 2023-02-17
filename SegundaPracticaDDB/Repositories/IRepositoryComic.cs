using SegundaPracticaDDB.Models;

namespace SegundaPracticaDDB.Repositories {
    public interface IRepositoryComic {

        List<Comic> GetComics();
        void CreateComic(string nombre, string imagen, string descripcion);

    }
}
