using lab8; 
using System; 
using System.IO; 
using System.Xml.Serialization; 

namespace lAB8_serialization // Определяем пространство имен для этой программы
{
    class Program // Определяем главный класс программы
    {
        static void Main() // Главный метод, который будет исполнен при запуске программы
        {
            // ЗАДАНИЕ 1: Сериализация объекта Animal
            Animal cow = new Cow("Russia", "No", "Bessie", "Cow", eClassificationAnimal.Herbivores, eFavouriteFood.Plants);
            // Создаем экземпляр коровы с параметрами (страна, прятаться от других животных, имя, тип животного, категория и любимая еда)

            XmlSerializer serializer = new XmlSerializer(typeof(Animal)); // Реализация сериализации для класса Animal
            using (TextWriter writer = new StreamWriter("animal.xml")) // Создаем потоку записи в файл "animal.xml"
            {
                serializer.Serialize(writer, cow); // Сериализуем объект cow и записываем его в файл
            }

            // Десериализация объекта Animal
            XmlSerializer deserializer = new XmlSerializer(typeof(Animal)); // Создаем десериализатор для класса Animal
            using (TextReader reader = new StreamReader("animal.xml")) // Открываем файл "animal.xml" для чтения
            {
                Animal deserializedAnimal = (Animal)deserializer.Deserialize(reader); // Десериализуем объект из файла
                Console.WriteLine($"Deserialized Animal:"); // Выводим заголовок
                // Выводим свойства десериализованного объекта на консоль
                Console.WriteLine($"Country: {deserializedAnimal.Country}");
                Console.WriteLine($"Hide From Other Animals: {deserializedAnimal.HideFromOtherAnimals}");
                Console.WriteLine($"Name: {deserializedAnimal.Name}");
                Console.WriteLine($"What Animal: {deserializedAnimal.WhatAnimal}");
                Console.WriteLine($"Classification: {deserializedAnimal.classification}");
                Console.WriteLine($"Favourite Food: {deserializedAnimal.favourite_food}");

                // ЗАДАНИЕ 2: Поиск и вывод содержимого файла
                string filePath = @"C:\Пользователи\Наталия\source\repos\l8\proj\bin\Debug"; // Путь к директории, где будет производиться поиск
                string searchfile = "animal.xml"; // Имя файла для поиска
                string[] files = Directory.GetFiles(filePath, searchfile, SearchOption.AllDirectories); // Поиск файла в указанной директории

                if (files.Length > 0) // Если файл найден
                {
                    using (FileStream fileStream = File.OpenRead(files[0])) // Открываем найденный файл для чтения
                    {
                        byte[] buffer = new byte[fileStream.Length]; // Создаем буфер для хранения содержимого файла
                        fileStream.Read(buffer, 0, buffer.Length); // Читаем содержимое файла в буфер
                        Console.WriteLine(System.Text.Encoding.Default.GetString(buffer)); // Преобразуем байты в строку и выводим на консоль
                    }
                }
                else // Если файл не найден
                {
                    Console.WriteLine("Файл не найден"); // Выводим сообщение об ошибке
                }

                // Сжатие файла animal.xml
                string fileName = @"C:\Пользователи\Наталия\source\repos\l8\proj\bin\Debug\animal.xml"; // Полный путь к файлу
                string compressedFilePath = $"{fileName}.gz"; // Путь для сохранения сжатого файла

                using (FileStream sourceFile = File.OpenRead(fileName)) // Открываем файл animal.xml для чтения
                {
                    using (FileStream targetFile = File.Create(compressedFilePath)) // Создаем новый файл для сохранения сжатого содержимого
                    {
                        using (var compressor = new System.IO.Compression.GZipStream(targetFile, System.IO.Compression.CompressionMode.Compress)) // Создаем объект для сжатия
                        {
                            sourceFile.CopyTo(compressor); // Копируем содержимое исходного файла в сжатый файл
                        }
                    }
                }
            }
        }
    }
}
