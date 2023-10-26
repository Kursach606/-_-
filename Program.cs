using System;
using System.IO;

class Program
{
    static string CaesarEncrypt(string text, int shift)
    {
        string encryptedText = "";
        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                char encryptedChar = (char)(((int)c + shift - 1040) % 32 + 1040);
                encryptedText += encryptedChar;
            }
            else
            {
                encryptedText += c;
            }
        }
        return encryptedText;
    }

    static string CaesarDecrypt(string encryptedText, int shift)
    {
        return CaesarEncrypt(encryptedText, 32 - shift);
    }


    static string VigenereEncrypt(string text, string keyword)
    {
        string encryptedText = "";
        int keywordIndex = 0;
        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                int shift = char.ToUpper(keyword[keywordIndex]) - 1040; // Изменение: Используем числовые значения для русских букв
                char encryptedChar = (char)(((int)c + shift - 1040) % 32 + 1040); // Изменение: Модуль 32 для русского алфавита
                encryptedText += encryptedChar;

                keywordIndex++;
                if (keywordIndex == keyword.Length)
                {
                    keywordIndex = 0;
                }
            }
            else
            {
                encryptedText += c;
            }
        }
        return encryptedText;
    }

    static string VigenereDecrypt(string encryptedText, string keyword)
    {
        string decryptedText = "";
        int keywordIndex = 0;
        foreach (char c in encryptedText)
        {
            if (char.IsLetter(c))
            {
                int shift = char.ToUpper(keyword[keywordIndex]) - 1040; // Изменение: Используем числовые значения для русских букв
                char decryptedChar = (char)(((int)c - shift - 1040 + 32) % 32 + 1040); // Изменение: Модуль 32 для русского алфавита
                decryptedText += decryptedChar;

                keywordIndex++;
                if (keywordIndex == keyword.Length)
                {
                    keywordIndex = 0;
                }
            }
            else
            {
                decryptedText += c;
            }
        }
        return decryptedText;
    }

    static void Main()
    {
        Console.WriteLine("Выберите метод шифрования:");
        Console.WriteLine("1. Шифр Цезаря");
        Console.WriteLine("2. Шифр Виженера");
        int method;
        if (!int.TryParse(Console.ReadLine(), out method))
        {
            Console.WriteLine("Неверный выбор метода шифрования.");
            return;
        }

        Console.WriteLine("Введите текст на русском:");
        string text = Console.ReadLine();

        string encryptedText = "";
        string decryptedText = "";

        if (method == 1)
        {
            Console.WriteLine("Введите сдвиг:");
            int shift;
            if (!int.TryParse(Console.ReadLine(), out shift))
            {
                Console.WriteLine("Неверное значение сдвига.");
                return;
            }

            encryptedText = CaesarEncrypt(text, shift);
            decryptedText = CaesarDecrypt(encryptedText, shift);
        }
        else if (method == 2)
        {
            Console.WriteLine("Введите ключевое слово:");
            string keyword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Неверное значение ключевого слова.");
                return;
            }

            encryptedText = VigenereEncrypt(text, keyword);
            decryptedText = VigenereDecrypt(encryptedText, keyword);
        }
        else
        {
            Console.WriteLine("Неверный выбор метода шифрования.");
            return;
        }

        Console.WriteLine("Зашифрованный текст: " + encryptedText);
        Console.WriteLine("Расшифрованный текст: " + decryptedText);

        Console.WriteLine("Хотите сохранить зашифрованный текст в файл? (y/n)");
        string saveToFile = Console.ReadLine();

        if (saveToFile.ToLower() == "y")
        {
            Console.WriteLine("Введите имя файла для сохранения:");
            string fileName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("Неверное имя файла.");
                return;
            }

            try
            {
                File.WriteAllText(fileName, encryptedText);
                Console.WriteLine("Зашифрованный текст успешно сохранен в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сохранении файла: " + ex.Message);
            }
        }

        Console.WriteLine("Хотите импортировать текст из файла? (y/n)");
        string importFromFile = Console.ReadLine();

        if (importFromFile.ToLower() == "y")
        {
            Console.WriteLine("Введите имя файла для импорта:");
            string fileName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("Неверное имя файла.");
                return;
            }

            try
            {
                string importedText = File.ReadAllText(fileName);
                Console.WriteLine("Текст успешно импортирован из файла: " + importedText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при импорте файла: " + ex.Message);
            }
        }
    }
}
