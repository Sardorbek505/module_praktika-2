using System;
using System.Collections.Generic;

public class User
{
    public string Name;
    public string Email;
    public string Role;

    public User(string name, string email, string role)
    {
        Name = name;
        Email = email;
        Role = role;
    }
}

public class UserManager
{
    private List<User> users = new List<User>();

    public void AddUser(string name, string email, string role)
    {
        // Проверка: если email уже есть — не добавляем
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Email == email)
            {
                Console.WriteLine("❌ Такой email уже существует: " + email);
                return;
            }
        }

        users.Add(new User(name, email, role));
        Console.WriteLine("✅ Добавлен: " + name);
    }

    public void RemoveUser(string email)
    {
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Email == email)
            {
                Console.WriteLine("🗑️ Удалён: " + users[i].Name);
                users.RemoveAt(i);
                return;
            }
        }

        Console.WriteLine("❌ Не найден email: " + email);
    }

    public void UpdateUser(string email, string newName, string newRole)
    {
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Email == email)
            {
                users[i].Name = newName;
                users[i].Role = newRole;
                Console.WriteLine("✏️ Обновлён: " + email);
                return;
            }
        }

        Console.WriteLine("❌ Не найден email: " + email);
    }

    public void ShowAllUsers()
    {
        Console.WriteLine("\n=== СПИСОК ПОЛЬЗОВАТЕЛЕЙ ===");

        if (users.Count == 0)
        {
            Console.WriteLine("Пусто.");
            return;
        }

        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {users[i].Name} | {users[i].Email} | {users[i].Role}");
        }
    }
}

class Program
{
    static void Main()
    {
        UserManager manager = new UserManager();

        // 1) Добавляем пользователей
        manager.AddUser("Ali", "ali@mail.com", "Admin");
        manager.AddUser("Sara", "sara@mail.com", "User");

        manager.ShowAllUsers();

        // 2) Обновляем пользователя
        manager.UpdateUser("sara@mail.com", "Sara K.", "Admin");

        manager.ShowAllUsers();

        // 3) Удаляем пользователя
        manager.RemoveUser("ali@mail.com");

        manager.ShowAllUsers();
    }
}
