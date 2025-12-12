
using System.Text.Json;

namespace Makarevich_proj.Extensions
{
    public static class SessionExtension   //делаем чтобы записать в сессию объект
    {

        public static void Set<T>(this ISession session, string key, T item)
        {
            var serializedItem = JsonSerializer.Serialize(item);  //серилизуем объект
            session.SetString(key, serializedItem); //Выызываем метод под ключём
        }

        public static T Get<T>(this ISession session, string key) //для чтения достаточно ключа
        {
            var item = session.GetString(key); //получаем строку
            return item == null
                ? Activator.CreateInstance<T>() // или default(T) ПУстой объект если объект не найден
                : JsonSerializer.Deserialize<T>(item); //если найден - десереализуем
        }

    }
}
