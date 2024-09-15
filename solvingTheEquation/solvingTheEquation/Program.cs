using System.Collections;

namespace QuadraticEquation
{
    internal class Program
    {
        enum Severity
        {
            Warning,
            Error,
            Information,
        }
        static void Main()
        {
            Console.WriteLine("Решение квадратного уравнение типа: a * x^2 + b * x + c = 0");
            Dictionary<string, string> uncheckedArrayList = new();
            List<int> checkedArrayList = new();
            Console.Write("Введите a: ");

            var uncheckedA = Console.ReadLine();
            uncheckedArrayList.Add("a", uncheckedA);
            Console.Write("Введите b: ");
            var uncheckedB = Console.ReadLine();
            uncheckedArrayList.Add("b", uncheckedB);
            Console.Write("Введите c: ");
            var uncheckedC = Console.ReadLine();
            uncheckedArrayList.Add("c", uncheckedC);

            foreach (KeyValuePair<string, string> entry in uncheckedArrayList)
            {
                CheckParams(entry, uncheckedArrayList, checkedArrayList);
            }
            SolvingQuadraticEquation(checkedArrayList[0], checkedArrayList[1], checkedArrayList[2]);
        }

        //Метод проверки валидности введенных пользователем переменных в консоли
        static void CheckParams(KeyValuePair<string, string> param, IDictionary uncheckedList, IList checkedList)
        {
            try
            {
                int intParam = int.Parse(param.Value);
                checkedList.Add(intParam);
            }

            catch (FormatException ex)
            {
                FormatData("Неверный формат параметра", Severity.Error, uncheckedList, param);
                Main();
            }
            catch (OverflowException ex)
            {
                FormatData("Значение было слишком большим или слишком маленьким для Int32 параметра", Severity.Information, uncheckedList, param);
                Main();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Main();
            }
        }
        //Метод нахождения дискриминта и корней уравнения
        static void SolvingQuadraticEquation(int a, int b, int c)
        {
            int exponent = 2;
            int degreeResult = 1;
            for (int i = 0; i < exponent; i++)
            {
                degreeResult *= b;
            }
            try
            {
                double discriminant = degreeResult - (4 * a * c);
                double firstRoot = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double secondRoot = (-b - Math.Sqrt(discriminant)) / (2 * a);

                if (discriminant > 0)
                {
                    Console.WriteLine($"x1 = {firstRoot}, x2 = {secondRoot}");
                }
                else if (discriminant == 0)
                {
                    Console.WriteLine($"x = {firstRoot}");
                }
                else
                {
                    throw new PersonException("Вещественных значений не найдено");
                }
                Main();
            }
            catch (PersonException ex)
            {
                FormatData($"{ex.Message}", Severity.Warning);
                Main();
            }
        }
        //Метод вывода отформатированных исключений в консоль.
        static void FormatData(string message, Severity severity, IDictionary data = null, KeyValuePair<string, string> unvalidParam = default)
        {
            string line = "--------------------------------------------------";
            if (severity == Severity.Error)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{line}\n{message} {unvalidParam.Key}\n{line}");
                foreach (DictionaryEntry entry in data)
                {
                    Console.WriteLine($"{entry.Key} = {entry.Value}");
                }
                Console.ResetColor();

            }
            else if (severity == Severity.Warning)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"{message}\n");
                Console.ResetColor();
            }
            else if (severity == Severity.Information)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"{message} {unvalidParam.Key} \n");
                Console.WriteLine("Максимальное допустимое значение: 2147483647\nМинимальное допустимое значение -2147483647");
                Console.ResetColor();
            }
            else
            {
                Console.ResetColor();
            }
        }
    }
    //Персональный класс исключений при ненайденных вещественных значений
    class PersonException : Exception
    {
        public PersonException(string message)
            : base(message) { }
    }
}
