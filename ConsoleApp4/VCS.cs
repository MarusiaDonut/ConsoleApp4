using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp4
{
    class VCS
    {
        const string pathStart = "D:\\Start.txt";
        public void New()
        {
            FileVersion a = new FileVersion();
            DirectoryInfo di = new DirectoryInfo(FileVersion.PathToActiveDirectory1);
            //Directory da = new Directory(pathCatalog);
            //Сharacteristics a = da.a;

            foreach (FileInfo f in di.GetFiles("*.*"))
            {
                
                int FlagFileNew = 1;
                int FlagRemove = 0;

                a.Name1 = f.Name;
                a.Size = Convert.ToString(f.Length);
                a.Create = Convert.ToString(f.CreationTime);
                a.Modify = Convert.ToString(f.LastAccessTime);

                for (int i = 0; i < FileVersion.fileList.Count; i++)
                {
                    if ((FileVersion.fileList[i].Name1 == a.Name1)
                        && (FileVersion.fileList[i].Note1 != "0")
                        && (FileVersion.fileList[i].Directoria == FileVersion.PathToActiveDirectory1))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;

                        if (FileVersion.fileList[i].Note1 == "remove")
                            Console.ForegroundColor = ConsoleColor.Red; 
                            Console.Write("file {0}<<-- {1}d/ed\n", a.Name1, FileVersion.fileList[i].Note1);
                            Console.Write("size {0}\n", a.Size);
                            Console.Write("create {0}\n", a.Create);
                            Console.Write("modify {0}\n\n", a.Modify);
                            Console.ResetColor();

                        FlagRemove = 1;
                            break; 
                    }
                }
                if (FlagRemove == 1)
                {
                    continue;
                }
                for (int i = 0; i < FileVersion.file.Count; i++)
                {
                    if (FileVersion.file[i].Directoria == FileVersion.PathToActiveDirectory1)
                    {
                        if (a.Name1 == FileVersion.file[i].Name1) // Сравнение имён файлов на текущий момент в Файле.
                        {
                            if (a.Size != FileVersion.file[i].Size)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;

                                Console.Write("file {0}\n", a.Name1);
                                Console.Write("size {0} b <<-- {1} b\n", FileVersion.file[i].Size, a.Size);
                                Console.Write("create {0}\n", a.Create);
                                Console.Write("modify {0}\n\n", a.Modify);

                                Console.ResetColor();
                            }
                            else if (a.Modify != FileVersion.file[i].Modify)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;

                                Console.Write("file {0}\n", a.Name1);
                                Console.Write("size {0} b\n", a.Size);
                                Console.Write("create {0}\n", a.Create);
                                Console.Write("modify {0} <<-- {1}\n\n", FileVersion.file[i].Modify, a.Modify);

                                Console.ResetColor();
                            }
                            
                              else  if (a.Size == FileVersion.file[i].Size && a.Create == FileVersion.file[i].Create)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("file {0}\n", FileVersion.file[i].Name1);
                                    Console.Write("size {0} b\n", FileVersion.file[i].Size);
                                    Console.Write("create {0}\n", FileVersion.file[i].Create);
                                    Console.Write("modify {0}\n\n", FileVersion.file[i].Modify);

                                    Console.ResetColor();
                                }
                            
                            FlagFileNew = 0;
                            break; 
                        }
                    }
                }
                if (FlagFileNew == 1)
                {
                    int FlagAdd = 1;
                    for (int i = 0; i < FileVersion.fileList.Count; i++)
                    {
                        if ((FileVersion.fileList[i].Name1 == a.Name1) && (FileVersion.fileList[i].Note1 == "add") && (FileVersion.fileList[i].Directoria == FileVersion.PathToActiveDirectory1))
                        {
                            FlagAdd = 0;
                            break;
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Green;

                    if (FlagAdd == 0)
                    {
                        Console.Write("file {0} <<--add\n", a.Name1);
                        Console.Write("size {0} b\n", a.Size);
                        Console.Write("create {0}\n", a.Create);
                        Console.Write("modify {0}\n\n", a.Modify);
                    }
                    else
                    {
                        Console.Write("file {0} <<--new\n", a.Name1);
                        Console.Write("size {0} b\n", a.Size);
                        Console.Write("create {0}\n", a.Create);
                        Console.Write("modify {0}\n\n", a.Modify);
                    }
                    Console.ResetColor();
                }
            }
        }

        public void Delete()
        {
            FileVersion a = new FileVersion();
            DirectoryInfo di = new DirectoryInfo(FileVersion.PathToActiveDirectory1);


            for (int i = 0; i < FileVersion.file.Count; i++)
            {
                int FlagDelete = 1;

                if (FileVersion.file[i].Directoria == FileVersion.PathToActiveDirectory1)
                {
                    foreach (FileInfo f in di.GetFiles("*.*"))
                    {
                        a.Name1 = f.Name;
                        a.Size = Convert.ToString(f.Length);
                        a.Create = Convert.ToString(f.CreationTime);
                        a.Modify = Convert.ToString(f.LastAccessTime);

                        if (FileVersion.file[i].Name1 == a.Name1)
                        {
                            FlagDelete = 0;
                            break;
                        }
                    }

                    if (FlagDelete == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.Write("file {0} <<--delete\n", FileVersion.file[i].Name1);
                        Console.Write("size {0} b\n", FileVersion.file[i].Size);
                        Console.Write("create {0}\n", FileVersion.file[i].Create);
                        Console.Write("modify {0}\n\n", FileVersion.file[i].Modify);

                        Console.ResetColor();
                    }
                }
            }
        }

        public void IdentifyComand(string comand)
        {
            String[] Com = comand.Split(new[] { ' ' }, 2); // Разделяем строку на подкоманды.

            if (Com[0] == "init")
            {
                if (Com.Length == 1)
                {
                    Console.WriteLine("Не введён путь к каталогу.\n");
                }

                else if (Com.Length == 2)
                {
                    if (!Directory.Exists(Com[1]))
                    {
                        Console.WriteLine("Каталог не существует.\n");
                    }
                    else
                    {
                        int flagWrite = 1;
                        // Запоминаем путь к активному каталогу.
                        FileVersion.PathToActiveDirectory1 = Com[1];

                        // Проверка директорий на инициализацию.
                        for (int i = 0; i < FileVersion.Catalog.Count; i++)
                        {
                            if (FileVersion.file[i].Directoria == FileVersion.PathToActiveDirectory1)
                                flagWrite = 0;
                        }
                        if (flagWrite == 1)
                        {
                            FileVersion p = new FileVersion();
                            init();
                            Console.WriteLine("Каталог: {0} проинициализирован.", FileVersion.PathToActiveDirectory1);
                        }
                        else
                            Console.WriteLine("Каталог: {0} уже был проинициализирован.", FileVersion.PathToActiveDirectory1);
                    }
                }
            }
            else if (Com[0] == "status")
            {
                if (Com.Length == 1)
                {
                    if (FileVersion.PathToActiveDirectory1 == null)
                    {
                        Console.WriteLine("Ни один из каталогов не проинициализирован!\n");
                    }
                    else
                    {
                        New();
                        Delete();
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат команды!\n");
                }
            }
            else if (Com[0] == "add")
            {
                if (Com.Length == 1)
                {
                    Console.WriteLine("Не введёно имя файла.\n");
                }

                else if (Com.Length == 2)
                {
                    if (!File.Exists(FileVersion.PathToActiveDirectory1 + "\\" + Com[1]))
                    {
                        Console.WriteLine("Файл не существует.\n");
                    }
                    else
                    {
                        add(Com[1]);
                    }
                }
            }

            else if (Com[0] == "remove")
            {
                if (Com.Length == 1)
                {
                    Console.WriteLine("Не введёно имя файла.\n");
                }

                else if (Com.Length == 2)
                {
                    remove(Com[1]);
                }
            }

            else if (Com[0] == "apply")
            {
                if (Com.Length == 1)
                {
                    if (FileVersion.PathToActiveDirectory1 == null)
                    {
                        Console.WriteLine("Ни один из каталогов не проинициализирован!\n");
                    }
                    else
                    {
                        apply();
                        Console.WriteLine("Все изменения сохранены в каталоге {0}!\n", FileVersion.PathToActiveDirectory1);
                    }
                }
                else if (Com.Length == 2)
                {
                    if (!Directory.Exists(Com[1]))
                    {
                        Console.WriteLine("Ошибка. Каталог не существует.\n");
                    }
                    else
                    {
                        // Запоминаем путь к активному каталогу.
                        FileVersion.PathToActiveDirectory1 = Com[1];

                        apply();
                        Console.WriteLine("Все изменения сохранены в каталоге {0}!\n", FileVersion.PathToActiveDirectory1);

                    }
                }
            }
            else if (Com[0] == "listbranch")
            {

                if (Com.Length == 1)
                {
                    if (FileVersion.PathToActiveDirectory1 == null)
                    {
                        Console.WriteLine("Ни один каталог не проинициализирован!\n");
                    }
                    else
                        listbranch();
                }
                else
                    Console.WriteLine("Неверный формат команды!\n");
            }

            else if (Com[0] == "checkout")
            {
                if (Com.Length == 1)
                {
                    Console.WriteLine("Не введён путь к каталогу или номер каталога.\n");
                }
                else if (Com.Length == 2)
                {
                    checkout(Com[1]);
                }
            }

            else if (Com[0] == "exit")
            {
                Console.WriteLine("ПОКА");
                Console.Read();
                Environment.Exit(0);
            }

        }

        public void init()
        {
            FileVersion p = new FileVersion();

            // Запись информации в файл о новой директории.
            p.Direct();

            // Заполнение списков НОВЫМИ данными.
            p.InitFile();
            p.InitCatalog();
        }

        public void apply()
        {
            FileVersion p = new FileVersion();

            // Запись НОВОЙ информации в файл о выбранной директории.
            p.ApplyFile();

            // Заполнение списков НОВЫМИ данными.
            p.InitFile();
            p.InitCatalog();
        }

        private void add(string file_name)
        {
            int flagAdd = 0; // 0 - не удалось найти, 1 - поиск удачно завершился.

            for (int i = 0; i < FileVersion.fileList.Count; i++)
            {
                if ((FileVersion.fileList[i].Name1 == file_name) && (FileVersion.fileList[i].Directoria == FileVersion.PathToActiveDirectory1))
                {
                    FileVersion.fileList[i].Note1 = "add";
                    Console.WriteLine("Файл добавлен под версионный контроль!\n", file_name);
                    flagAdd = 1;
                    break;
                }
            }
           
            //Если не нашли среди проинициализированных, то добавляем во временный список с меткой "add". 
            if (flagAdd == 0)
            {
                DirectoryInfo d = new DirectoryInfo(FileVersion.PathToActiveDirectory1);
                    foreach (FileInfo f in d.GetFiles(file_name))
                    {
                        FileVersion p = new FileVersion();

                        // Узнаем информацию о файле.
                        p.Name1 = f.Name;
                        p.Size = Convert.ToString(f.Length);
                        p.Create = Convert.ToString(f.CreationTime);
                        p.Modify = Convert.ToString(f.LastWriteTime);
                        p.Note1 = "add";
                        p.Directoria = Convert.ToString(f.DirectoryName);

                        FileVersion.fileList.Add(p);
                        Console.WriteLine("Файл добавлен под версионный контроль!\n", p.Name1);

                        // Добавляем во временный список, который будет хранить метку add.
                    }   
                
            }
        }

        private void remove(string file_name)
        {
            int flagRemove = 0;

            for (int i = 0; i < FileVersion.fileList.Count; i++)
            {
                if ((FileVersion.fileList[i].Name1 == file_name) && (FileVersion.fileList[i].Directoria == FileVersion.PathToActiveDirectory1))
                {
                    FileVersion.fileList[i].Note1 = "remove";
                    Console.WriteLine("Файл убран из-под версионного контроля!\n", file_name);
                    flagRemove = 1;
                    break;
                }
            }

            if (flagRemove == 0)
            {
                DirectoryInfo d = new DirectoryInfo(FileVersion.PathToActiveDirectory1);
                foreach (FileInfo f in d.GetFiles(file_name))
                {
                    FileVersion p = new FileVersion();

                    // Узнаем информацию о файле.
                    p.Name1 = f.Name;
                    p.Size = Convert.ToString(f.Length);
                    p.Create = Convert.ToString(f.CreationTime);
                    p.Modify = Convert.ToString(f.LastWriteTime);
                    p.Note1 = "remove";
                    p.Directoria = Convert.ToString(f.DirectoryName);

                    Console.WriteLine("Файл убран из-под версионного контроля!\n", p.Name1);

                    // Добавляем во временный список, который будет хранить метку remove.
                    FileVersion.fileList.Add(p);

                }
            }
        }

        public void listbranch()
        {
            FileVersion p = new FileVersion();

            p.InitCatalog();//последний каталог проинициализирован будет

            Console.WriteLine("Список проинициализированных каталогов:");
            // Вывод проинициализированных каталогов
            for (int i = 0; i < FileVersion.Catalog.Count; i++)
            {
                Console.WriteLine("{0}", FileVersion.Catalog[i].Directoria);
            }
            Console.WriteLine("");
        }

        public void checkout(string dir_path)
        {
            int flagFound = 0; 

            for (int i = 0; i < FileVersion.Catalog.Count; i++)
            {
                if (FileVersion.Catalog[i].Directoria == @dir_path)
                {
                    FileVersion.PathToActiveDirectory1 = @dir_path;
                    flagFound = 1;
                    break;
                }
            }
            if (flagFound == 1)
                Console.WriteLine("checkout <--Установлен активный каталог: {0}!\n", FileVersion.PathToActiveDirectory1);
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("checkout <--Ошибка. Каталог: {0} не найден среди проинициализированных!\n", dir_path);
                Console.ResetColor();
            }
        }
    }
}