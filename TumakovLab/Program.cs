using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Выберите упражнение (1-6):");
        Console.WriteLine("1. Подсчет гласных и согласных букв в файле");
        Console.WriteLine("2. Умножение двух матриц");
        Console.WriteLine("3. Средняя температура за год");
        Console.WriteLine("4. Использование List<T>");
        Console.WriteLine("5. Использование LinkedList<LinkedList<T>>");
        Console.WriteLine("6. Использование Dictionary<TKey, TValue>");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                await Task1(); 
                break;
            case 2:
                MultiplyMatrices();
                break;
            case 3:
                AverageTemperature();
                break;
            case 4:
                CountVowelsAndConsonantsWithList(args);
                break;
            case 5:
                MultiplyMatricesWithLinkedList();
                break;
            case 6:
                AverageTemperatureWithDictionary();
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }
    }

   
    public static int[] GetVowelsAndConsonantsCount(char[] text)
    {
        const string vowels = "аеёиоуыэюяАЕЁИОУЫЭЮЯ"; 
        int[] vowelsAndConsonants = new int[2] { 0, 0 };

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                if (vowels.Contains(c))
                    vowelsAndConsonants[0]++;
                else
                    vowelsAndConsonants[1]++;
            }
        }

        return vowelsAndConsonants;
    }

    public static async Task Task1()
    {
        Console.WriteLine("Упр. 6.1");
        Console.WriteLine("Укажите локальный путь к файлу или URL: ");
        string link = Console.ReadLine()!;

        try
        {
            string fileContent;

           
            if (link.StartsWith("http://") || link.StartsWith("https://"))
            {
                using HttpClient client = new HttpClient();
                fileContent = await client.GetStringAsync(link);
            }
            else
            {
                if (File.Exists(link))
                {
                    fileContent = File.ReadAllText(link, Encoding.UTF8);
                }
                else
                {
                    throw new FileNotFoundException("Указан несуществующий файл");
                }
            }

            
            char[] inputFileText = fileContent.ToCharArray();
            int[] inputTextVowelsAndConsonants = GetVowelsAndConsonantsCount(inputFileText);

           
            Console.WriteLine("Количество гласных букв в тексте: {0}", inputTextVowelsAndConsonants[0]);
            Console.WriteLine("Количество согласных букв в тексте: {0}", inputTextVowelsAndConsonants[1]);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при чтении файла: {ex.Message}");
        }
    }

    // Упражнение 6.2
    static void MultiplyMatrices()
    {
        int[,] matrixA = { { 1, 2 }, { 3, 4 } };
        int[,] matrixB = { { 5, 6 }, { 7, 8 } };
        int[,] result = new int[2, 2];
        Console.WriteLine("\nУпражнение 6.2 Написать программу, реализующую умножению двух матриц, заданных в\r\nвиде двумерного массива. В программе предусмотреть два метода: метод печати матрицы,\r\nметод умножения матриц (на вход две матрицы, возвращаемое значение – матрица).\n\n");
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                for (int k = 0; k < 2; k++)
                    result[i, j] += matrixA[i, k] * matrixB[k, j];

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
                Console.Write(result[i, j] + "\t");

            Console.WriteLine();
        }
    }// Упражнение 6.3
    static void AverageTemperature()
    {
        Console.WriteLine("\nУпражнение 6.3 Написать программу, вычисляющую среднюю температуру за год. Создать\r\nдвумерный рандомный массив temperature[12,30], в котором будет храниться температура\r\nдля каждого дня месяца (предполагается, что в каждом месяце 30 дней). Сгенерировать\r\nзначения температур случайным образом. Для каждого месяца распечатать среднюю\r\nтемпературу. Для этого написать метод, который по массиву temperature [12,30] для каждого\r\nмесяца вычисляет среднюю температуру в нем, и в качестве результата возвращает массив\r\nсредних температур. Полученный массив средних температур отсортировать по\r\nвозрастанию.\n\n");
        Random rand = new Random();
        double[] averages = new double[12];

        for (int month = 0; month < 12; month++)
        {
            double sum = 0;
            for (int day = 0; day < 30; day++)
                sum += rand.Next(-10, 35);
            averages[month] = sum / 30;
        }

        Array.Sort(averages);

        Console.WriteLine("Средние температуры по месяцам:");
        foreach (var temp in averages)
            Console.WriteLine(temp);
    }

    // Домашнее задание 6.1
    static void CountVowelsAndConsonantsWithList(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("\nДомашнее задание 6.1 Упражнение 6.1 выполнить с помощью коллекции List<T>.\n\n");
            Console.WriteLine("\nУкажите имя файла.");
            return;
        }

        List<char> content = new List<char>(File.ReadAllText(args[0]));
        int vowels = 0, consonants = 0;

        foreach (char c in content)
        {
            if (char.IsLetter(c))
            {
                if ("aeiouAEIOU".Contains(c))
                    vowels++;
                else
                    consonants++;
            }
        }

        Console.WriteLine($"Гласные: {vowels}, Согласные: {consonants}");
    }

    // Домашнее задание 6.2
    static void MultiplyMatricesWithLinkedList()
    {
        Console.WriteLine("\nДомашнее задание 6.2 Упражнение 6.2 выполнить с помощью коллекций\r\nLinkedList<LinkedList<T>>.\n\n");
        var matrixA = new LinkedList<LinkedList<int>>();
        var matrixB = new LinkedList<LinkedList<int>>();

        matrixA.AddLast(new LinkedList<int>(new[] { 1, 2 }));
        matrixA.AddLast(new LinkedList<int>(new[] { 3, 4 }));

        matrixB.AddLast(new LinkedList<int>(new[] { 5, 6 }));
        matrixB.AddLast(new LinkedList<int>(new[] { 7, 8 }));

        var result = MultiplyMatrices(matrixA, matrixB);

        PrintMatrix(result);
    }

    static LinkedList<LinkedList<int>> MultiplyMatrices(LinkedList<LinkedList<int>> a, LinkedList<LinkedList<int>> b)
    {
        var result = new LinkedList<LinkedList<int>>();

        foreach (var rowA in a)
        {
            var rowResult = new LinkedList<int>();
            foreach (var colB in b)
            {
                int sum = rowA.First.Value * colB.First.Value + rowA.Last.Value * colB.Last.Value;
                rowResult.AddLast(sum);
            }
            result.AddLast(rowResult);
        }

        return result;
    }

    static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
    {
        foreach (var row in matrix)
        {
            foreach (var value in row)
                Console.Write(value + "\t");
            Console.WriteLine();
        }
    }

    // Домашнее задание 6.3
    static void AverageTemperatureWithDictionary()
    {
        Console.WriteLine("\n Домашнее задание 6.3 Написать программу для упражнения 6.3, использовав класс\r\nDictionary<TKey, TValue>. В качестве ключей выбрать строки – названия месяцев, а в\r\nкачестве значений – массив значений температур по дням.\n\n");
        var temperatureData = new Dictionary<string, double[]>();
        string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
                            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        foreach (var month in months)
        {
            double[] temperatures = new double[30];
            for (int day = 0; day < 30; day++)
                temperatures[day] = new Random().Next(-10, 35);
            temperatureData[month] = temperatures;
        }

        foreach (var entry in temperatureData)
        {
            double average = CalculateAverage(entry.Value);
            Console.WriteLine($"{entry.Key}: {average}");
        }
    }

    static double CalculateAverage(double[] temperatures)
    {
        double sum = 0;
        foreach (var temp in temperatures)
            sum += temp;

        return sum / temperatures.Length;
    }
}
