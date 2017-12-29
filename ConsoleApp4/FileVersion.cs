using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp4
{
    class FileVersion
    {
        const string pathStart = "D:\\Start.txt";
        string name;
        public string Name1 { get => name; set => name = value; }

        string size;
        public string Size { get => size; set => size = value; }

        string create;
        public string Create { get => create; set => create = value; }

        string modify;
        public string Modify { get => modify; set => modify = value; }

        string directoria;
        public string Directoria { get => directoria; set => directoria = value; }

        string Note;
        public string Note1 { get => Note; set => Note = value; }

        static string PathToActiveDirectory;
        public static string PathToActiveDirectory1 { get => PathToActiveDirectory; set => PathToActiveDirectory = value; }

        public static  List<FileVersion> Catalog = new List<FileVersion>();
        public static List<FileVersion> file = new List<FileVersion>();//список, где хранятся проинициализированный
        public static List<FileVersion> fileList = new List<FileVersion>(); // Временный список.

        public void Write(FileVersion a)
        {
            
             FileStream fl = new FileStream(pathStart, FileMode.Append);
             StreamWriter writer = new StreamWriter(fl);

             writer.WriteLine("{0}", a.Name1);
             writer.WriteLine("{0}", a.Size);
             writer.WriteLine("{0}", a.Create);
             writer.WriteLine("{0}", a.Modify);
             writer.WriteLine("{0}", a.Note1);
             writer.WriteLine("{0}", a.Directoria);
            
            writer.Close();
        }
        
        public void Direct()
        {
            DirectoryInfo di = new DirectoryInfo(FileVersion.PathToActiveDirectory1);

            foreach (FileInfo f in di.GetFiles("*.*"))
            {
                FileVersion a = new FileVersion();
               
                a.Name1 = f.Name;
                a.Size = Convert.ToString(f.Length);
                a.Create = Convert.ToString(f.CreationTime);
                a.Modify = Convert.ToString(f.LastAccessTime);
                a.Note1 = "0";
                a.Directoria = Convert.ToString(f.DirectoryName);

                Write(a);

                // file.Close();
            }
        }

        public void InitCatalog()
        {
            // Очистка списка.
            Catalog.Clear();

            for (int i = 0; i < FileVersion.file.Count; i++)
            {
                int flag = 1;
                 
                FileVersion p = new FileVersion();

                p.Directoria = FileVersion.file[i].Directoria;

                for (int j = 0; j < FileVersion.Catalog.Count; j++)
                {
                    if (p.Directoria == Catalog[j].Directoria)
                    {
                        flag = 0;
                        break;
                    }
                }
                
                if (flag == 1)//если не найдена директория, то добавляется записи в спиоск
                    Catalog.Add(p);
            }
        }

        public void InitFile()
        {
               file.Clear();// Очистка списка.

            // Обход файла для записи в список проинициализированных файлов.
            FileStream File = new FileStream(pathStart, FileMode.Open);
            StreamReader Reader = new StreamReader(File);

            while (!Reader.EndOfStream) // Начальное состояние.
            {
                FileVersion p = new FileVersion();

                // Достаём строки и вынимаем характеристики
                string Name = Reader.ReadLine();
                string Size = Reader.ReadLine();
                string Created = Reader.ReadLine();
                string Modified = Reader.ReadLine();
                string Noted = Reader.ReadLine();
                string Directory = Reader.ReadLine();

                p.Name1 = Name;
                p.Size = Size;
                p.Create = Created;
                p.Modify = Modified;
                p.Note1 = Noted;
                p.Directoria = Directory;

                file.Add(p);// Заполнение списка.
            }
            Reader.Close();
        }

        public void ApplyFile()
        {
            int flagWriteNew = 1;
            
            // Очистка файла.
            FileStream p = new FileStream(pathStart, FileMode.Create);
            StreamWriter Writer = new StreamWriter(p);
            Writer.Close();

            // Очистка меток временного списка для АКТИВНОЙ директории
            for (int i = 0; i < FileVersion.fileList.Count; i++)
            {
                if (FileVersion.fileList[i].Directoria == FileVersion.PathToActiveDirectory)
                {
                    if (FileVersion.fileList[i].Note1 != "remove")
                        FileVersion.fileList[i].Note1 = "0";
                    else
                        FileVersion.fileList[i].Note1 = "new";
                }
            }

            // Берем список "file".
            // Записываем НОВУЮ информацию о ТЕКУЩЕМ каталоге.
            // Записываем СТАРУЮ информацию об остальных ПРОИНИЦИАЛИЗИРОВАННЫХ каталогах из списка "file".
            DirectoryInfo d = new DirectoryInfo(FileVersion.PathToActiveDirectory1);

            for (int i = 0; i < FileVersion.file.Count; i++)
            {

                FileVersion Old = new FileVersion();

                // Узнаем информацию о файле.
                Old.Name1 = FileVersion.file[i].Name1;
                Old.Size = FileVersion.file[i].Size;
                Old.Create = FileVersion.file[i].Create;
                Old.Modify = FileVersion.file[i].Modify;
                Old.Note1 = FileVersion.file[i].Note1;
                Old.Directoria = FileVersion.file[i].Directoria;

                // Записываем НОВУЮ информацию о ТЕКУЩЕМ каталоге.
                if (flagWriteNew == 1)
                {
                    foreach (FileInfo f in d.GetFiles())
                    {
                        FileVersion New = new FileVersion();

                        // Узнаем НОВУЮ информацию о файле в текущей директории.
                        New.Name1 = f.Name;
                        New.Size = Convert.ToString(f.Length);
                        New.Create = Convert.ToString(f.CreationTime);
                        New.Modify = Convert.ToString(f.LastWriteTime);
                        New.Note1 = Old.Note1;
                        New.Note1 = "0";
                        New.Directoria = Convert.ToString(f.DirectoryName);

                        // Если директории совпадают, перезаписываем НОВУЮ информацию.
                        if (Old.Directoria == FileVersion.PathToActiveDirectory1)
                        {
                           // Запись в файл НОВОЙ инфы о директории.
                                Write(New);

                                flagWriteNew = 0;
                            
                        }
                        else
                            break;
                    }
                }
                // Записываем СТАРУЮ информацию об остальных ПРОИНИЦИАЛИЗИРОВАННЫХ каталогах.
                if (Old.Directoria != FileVersion.PathToActiveDirectory1)
                    Write(Old);
            }
                // Очистка списка.
                file.Clear();
            
        }
    }
}
