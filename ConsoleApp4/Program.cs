using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp4
{
    class Program
    {
        private static string comand;
        static void Main(string[] args)
        {
            string s;
         

            FileVersion version = new FileVersion();
            VCS e = new VCS();
            version.InitFile();
            version.InitCatalog();
            

            if (FileVersion.file.Count != 0)
                FileVersion.PathToActiveDirectory1 = FileVersion.file[FileVersion.file.Count - 1].Directoria;
            else
                FileVersion.PathToActiveDirectory1 = null;
           
            t:
            Console.WriteLine("Нажмите 1, если знаете как работать\nНажмите 2, если нужна помощь");
            s = Console.ReadLine();
            switch(s)
            {
                case "1":
                    while (true)
                    {

                        comand = Console.ReadLine();
                        e.IdentifyComand(comand);

                    }
                case "2":
                    Console.WriteLine("Команда init инициализирует каталог\n" + 
                                      "Команда status выводит информацию обо всех проинициализированных файлах из списка\n" +
                                      "Команда apply сохраняет все изменения в каталоге\n" +
                                      "Команда listbranch выводит список проинициализированных каталогов\n" +
                                      "Команда checkout устанавливает активный каталог" +
                                      "Команда remove убирает файл из-под версионного контроля" +
                                      "Команда add добавляет файл под версионный контроль");
                    Console.ReadLine();
                    Console.Clear();
                    goto t;
            }
        }
    }
}
