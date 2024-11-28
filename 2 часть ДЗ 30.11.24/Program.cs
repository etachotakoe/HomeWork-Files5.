using System;
using System.Collections.Generic;
using System.IO;

class Student
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int BirthYear { get; set; }
    public string Exam { get; set; }
    public int Score { get; set; }

    public override string ToString()
    {
        return $"{LastName} {FirstName}, Год рождения: {BirthYear}, Экзамен: {Exam}, Баллы: {Score}";
    }
}

class Program
{
    static void Main()
    {
        Dictionary<string, Student> students = LoadStudentsFromFile("students.txt");

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Работа с изображениями");
            Console.WriteLine("2. Управление списком студентов");
            Console.WriteLine("3. Выход");
            Console.Write("Введите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WorkWithImages();
                    break;
                case "2":
                    ManageStudents(students);
                    break;
                case "3":
                    SaveStudentsToFile(students, "students.txt");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void WorkWithImages()
    {
        List<string> imageUrls = new List<string>
        {
            "https://yandex.ru/images/search?text=%D0%BF%D1%80%D0%B8%D1%80%D0%BE%D0%B4%D0%B0&pos=2&rpt=simage&img_url=https%3A%2F%2Fimg3.akspic.ru%2Foriginals%2F8%2F0%2F5%2F5%2F45508-nauka-nebo-ozero_pejto-pustynya-ekosistema-2560x1440.jpg&from=tabbar&lr=43",
            "https://yandex.ru/images/search?text=%D0%B0%D1%81%D1%82%D0%BE%D0%BB%D1%8C%D1%84%D0%BE&pos=1&rpt=simage&img_url=https%3A%2F%2Fsun9-71.userapi.com%2Fimpg%2FmOK8d58Q-bouyZZLhGeEOVIDqg8VLOFFiXd0lg%2FV47exv7Cjx4.jpg%3Fsize%3D438x604%26quality%3D96%26sign%3Dfd7ec1b344b6cf6d7d3b035e2e25bb8f%26type%3Dalbum&from=tabbar&lr=43",
            "https://yandex.ru/images/search?text=%D1%81%D0%BB%D0%B8%D0%B4%D0%B0%D0%BD&pos=0&rpt=simage&img_url=https%3A%2F%2Fpbs.twimg.com%2Fmedia%2FDusIQ2uX4AAFb8F.jpg&from=tabbar&lr=43",
            "https://yandex.ru/images/search?p=1&text=%D1%81%D0%BB%D0%B8%D0%B4%D0%B0%D0%BD&pos=8&rpt=simage&img_url=https%3A%2F%2Fsun9-64.userapi.com%2F4e4f5LaID23Ofpa7NjmzsKWGpinPOMiO3T2TCw%2FuBhXGGNqbQg.jpg&from=tabbar&lr=43",
            "https://yandex.ru/images/search?p=1&text=%D0%B0%D0%BD%D0%B8%D0%BC%D0%B5&pos=14&rpt=simage&img_url=https%3A%2F%2Fimg.goodfon.com%2Foriginal%2F4000x2500%2F8%2Faf%2Fakatsuki-no-goei-nikaidou.jpg&from=tabbar&lr=43",
            "https://yandex.ru/images/search?text=%D1%82%D1%80%D0%BE%D0%BB%D0%BB%D1%84%D0%B5%D0%B9%D1%81&pos=1&rpt=simage&img_url=https%3A%2F%2Fimg.razrisyika.ru%2Fkart%2F133%2F1200%2F529783-trollfeys-2.jpg&from=tabbar&lr=43",
            "https://yandex.ru/images/search?text=%D1%83%D0%BF%D1%8F%D1%87%D0%BA%D0%B0&pos=1&rpt=simage&img_url=https%3A%2F%2Fcs12.pikabu.ru%2Fpost_img%2F2022%2F12%2F09%2F5%2Fog_og_1670572248292596693.jpg&from=tabbar&lr=43",
            "https://yandex.ru/images/search?p=3&text=%D0%BC%D0%B5%D0%BC%D1%8B&pos=13&rpt=simage&img_url=https%3A%2F%2Fsun9-14.userapi.com%2Fc631930%2Fv631930363%2F27c25%2FF4S22jrgfqA.jpg&from=tabbar&lr=43",
            "https://yandex.ru/images/search?text=%D0%BC%D0%B5%D0%BC%D1%8B+%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B8%D1%81%D1%82%D0%BE%D0%B2&pos=2&rpt=simage&img_url=https%3A%2F%2Fsun9-42.userapi.com%2Fimpg%2FrQ0kHskSNow7x61By0ZQpb-57K_kkp90YpzGuA%2F7nAQQoUeCvQ.jpg%3Fsize%3D1280x1280%26quality%3D95%26sign%3D872606208f05b4b975a6787a269519ca%26c_uniq_tag%3DVZp5nNygT4jqZpdumeeeTcAeDGOBDNswa2ULpTEqptw%26type%3Dalbum&from=tabbar&lr=43",
            "https://yandex.ru/images/search?text=%D0%BC%D0%B5%D0%BC%D1%8B+%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B8%D1%81%D1%82%D0%BE%D0%B2&pos=8&rpt=simage&img_url=https%3A%2F%2Fpolinka.top%2Fuploads%2Fposts%2F2023-05%2F1684254864_polinka-top-p-kartinki-prikoli-pro-programmistov-instagr-28.jpg&from=tabbar&lr=43",
            "https://yandex.ru/images/search?text=%D0%BC%D0%B5%D0%BC%D1%8B+%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B8%D1%81%D1%82%D0%BE%D0%B2&pos=29&rpt=simage&img_url=https%3A%2F%2Fcs7.pikabu.ru%2Fpost_img%2Fbig%2F2014%2F03%2F30%2F7%2F1396171703_1766643937.jpg&from=tabbar&lr=43",
            "https://yandex.ru/images/search?p=3&text=%D0%BC%D0%B5%D0%BC%D1%8B+%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B8%D1%81%D1%82%D0%BE%D0%B2&pos=4&rpt=simage&img_url=http%3A%2F%2Fsun9-19.userapi.com%2Fimpg%2F3HCzzbunMrNp1NR8SEGf5ShqEbLCYu_ahFjzFw%2FU3XeYxrYQ6I.jpg%3Fsize%3D600x601%26quality%3D95%26sign%3D23c9901916d00d358010e6618108467e%26c_uniq_tag%3DlGq9QUSaR8W3QaZMb6oY6Fwa4LPtvZ5nR33GjuMKc9U%26type%3Dalbum&from=tabbar&lr=43",
            "https://yandex.ru/images/search?p=5&text=%D0%BC%D0%B5%D0%BC%D1%8B+%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B8%D1%81%D1%82%D0%BE%D0%B2&pos=24&rpt=simage&img_url=https%3A%2F%2Fpolinka.top%2Fuploads%2Fposts%2F2023-05%2F1684950460_polinka-top-p-programmist-smeshnaya-kartinka-krasivo-26.jpg&from=tabbar&lr=43",
            "https://yandex.ru/images/search?p=1&text=%D0%BC%D0%B5%D0%BC%D1%8B+%D0%BF2024&pos=22&rpt=simage&img_url=https%3A%2F%2Fstatic.wikia.nocookie.net%2F37fba185-c303-4801-9173-82b72433d48b&from=tabbar&lr=43",
            "https://yandex.ru/images/search?p=2&text=%D0%BC%D0%B5%D0%BC%D1%8B+%D0%BF2024&pos=12&rpt=simage&img_url=https%3A%2F%2Fwww.meme-arsenal.com%2Fmemes%2F3ace5fa3ac051683377c8de4f8f77f96.jpg&from=tabbar&lr=43",
            "https://example.com/image16.jpg",
            "https://example.com/image17.jpg",
            "https://example.com/image18.jpg",
            "https://example.com/image19.jpg",
            "https://example.com/image20.jpg",
            "https://example.com/image21.jpg",
            "https://example.com/image22.jpg",
            "https://example.com/image23.jpg",
            "https://example.com/image24.jpg",
            "https://example.com/image25.jpg",
            "https://example.com/image26.jpg",
            "https://example.com/image27.jpg",
            "https://example.com/image28.jpg",
            "https://example.com/image29.jpg",
            "https://example.com/image30.jpg",
            "https://example.com/image31.jpg",
            "https://example.com/image32.jpg"
        };

        List<string> imagesList = new List<string>();
        foreach (var url in imageUrls)
        {
            imagesList.Add(url);
            imagesList.Add(url);
        }

        Shuffle(imagesList);

        Console.WriteLine("\nПеремешанный список изображений:");
        for (int i = 0; i < imagesList.Count; i++)
        {
            Console.WriteLine($"Индекс: {i}, Изображение: {imagesList[i]}");
        }
    }

    static void Shuffle(List<string> list)
    {
        Random random = new Random();
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    static void ManageStudents(Dictionary<string, Student> students)
    {
        while (true)
        {
            Console.WriteLine("\nМеню управления студентами:");
            Console.WriteLine("1. Новый студент");
            Console.WriteLine("2. Удалить студента");
            Console.WriteLine("3. Сортировать студентов");
            Console.WriteLine("4. Вернуться в главное меню");
            Console.Write("Введите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent(students);
                    break;
                case "2":
                    RemoveStudent(students);
                    break;
                case "3":
                    SortStudents(students);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static Dictionary<string, Student> LoadStudentsFromFile(string filePath)
    {
        var students = new Dictionary<string, Student>();
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 5)
                {
                    var student = new Student
                    {
                        LastName = parts[0],
                        FirstName = parts[1],
                        BirthYear = int.Parse(parts[2]),
                        Exam = parts[3],
                        Score = int.Parse(parts[4])
                    };
                    students[student.LastName] = student;
                }
            }
        }
        return students;
    }

    static void AddStudent(Dictionary<string, Student> students)
    {
        var student = new Student();

        Console.Write("Введите фамилию: ");
        student.LastName = Console.ReadLine();

        Console.Write("Введите имя: ");
        student.FirstName = Console.ReadLine();

        Console.Write("Введите год рождения: ");
        student.BirthYear = int.Parse(Console.ReadLine());

        Console.Write("Введите экзамен: ");
        student.Exam = Console.ReadLine();

        Console.Write("Введите баллы: ");
        student.Score = int.Parse(Console.ReadLine());

        students[student.LastName] = student;
    }

    static void RemoveStudent(Dictionary<string, Student> students)
    {
        Console.Write("Введите фамилию студента для удаления: ");
        string lastName = Console.ReadLine();

        if (students.Remove(lastName))
        {
            Console.WriteLine($"Студент {lastName} удален.");
        }
        else
        {
            Console.WriteLine($"Студент с фамилией {lastName} не найден.");
        }
    }

    static void SortStudents(Dictionary<string, Student> students)
    {
        var sortedStudents = new List<Student>(students.Values);
        sortedStudents.Sort((s1, s2) => s1.LastName.CompareTo(s2.LastName));

        Console.WriteLine("\nСписок студентов:");
        foreach (var student in sortedStudents)
        {
            Console.WriteLine(student);
        }
    }

    static void SaveStudentsToFile(Dictionary<string, Student> students, string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var student in students.Values)
            {
                writer.WriteLine($"{student.LastName},{student.FirstName},{student.BirthYear},{student.Exam},{student.Score}");
            }
        }
    }
}
