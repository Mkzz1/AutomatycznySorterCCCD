using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            //save every column of spreadsheet into separate text files in same folder
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string filePath1 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv";
            using (var stream = File.Open(filePath1, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    //every column of spreadsheet is saved into separate text file. Like column 1 is file1, column 2 is file2 etc.
                    while (reader.Read())
                    {
                        string filePath2 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + reader.GetValue(0) + ".txt";
                        using (StreamWriter sw = new StreamWriter(filePath2))
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                sw.WriteLine(reader.GetValue(i));
                            }
                        }
                    }
                }
            }
            //remove first line of every .txt file in folder. Move all other lines to first line.
            string[] filePaths = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 1)
                {
                    File.WriteAllLines(filePath, lines.Skip(1));
                }
            }
            //get file names from folder WITHOUT path
            string[] userNames = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            //remove path from fileNames
            for (int i = 0; i < userNames.Length; i++)
            {
                userNames[i] = Path.GetFileNameWithoutExtension(userNames[i]);
            }
            List<string> folderLocaions = new List<string>();
            //add to list folderLocations FolderLocation from database.mdf of specific user from list userNames
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\database.mdf;Version=3;"))
            {
                conn.Open();
                foreach (string user in userNames)
                {
                    string sql = "SELECT FolderLocation FROM Users WHERE Name = '" + user + "'";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                folderLocaions.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            //Move all files that contain charachters from .txt file, new line is a separator. .txt file name is user Name, move to user folder.
            for (int i = 0; i < userNames.Length; i++)
            {
                string[] lines = File.ReadAllLines(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + userNames[i] + ".txt");
                foreach (string line in lines)
                {
                    string[] words = line.Split('\n');
                    foreach (string word in words)
                    {
                        if (word.Length > 0)
                        {
                            //move all files that start with word
                            string[] files = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", word + "*");
                            foreach (string file in files)
                            {
                                File.Move(file, folderLocaions[i] + "\\" + Path.GetFileName(file));
                            }
                        }
                    }
                }
            }
            //delete all .txt files in folder
            string[] txtFiles = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string file in txtFiles)
            {
                File.Delete(file);
            }
            //move AutoAssigment.csv file to folder C:\Users\mikoz\Desktop\doki\User4 named as hour minute and date of move
            File.Move(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv", @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\Logi\" + "SZZ " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".csv");
        }
        private void fileSystemWatcher2_Changed(object sender, FileSystemEventArgs e)
        {
            //save every column of spreadsheet into separate text files in same folder
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string filePath1 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv";
            using (var stream = File.Open(filePath1, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    //every column of spreadsheet is saved into separate text file. Like column 1 is file1, column 2 is file2 etc.
                    while (reader.Read())
                    {
                        string filePath2 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + reader.GetValue(0) + ".txt";
                        using (StreamWriter sw = new StreamWriter(filePath2))
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                sw.WriteLine(reader.GetValue(i));
                            }
                        }
                    }
                }
            }
            //remove first line of every .txt file in folder. Move all other lines to first line.
            string[] filePaths = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 1)
                {
                    File.WriteAllLines(filePath, lines.Skip(1));
                }
            }
            //get file names from folder WITHOUT path
            string[] userNames = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            //remove path from fileNames
            for (int i = 0; i < userNames.Length; i++)
            {
                userNames[i] = Path.GetFileNameWithoutExtension(userNames[i]);
            }
            List<string> folderLocaions = new List<string>();
            //add to list folderLocations FolderLocation from database.mdf of specific user from list userNames
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\database.mdf;Version=3;"))
            {
                conn.Open();
                foreach (string user in userNames)
                {
                    string sql = "SELECT FolderLocation FROM Users WHERE Name = '" + user + "'";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                folderLocaions.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            //Move all files that contain charachters from .txt file, new line is a separator. .txt file name is user Name, move to user folder.
            for (int i = 0; i < userNames.Length; i++)
            {
                string[] lines = File.ReadAllLines(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + userNames[i] + ".txt");
                foreach (string line in lines)
                {
                    string[] words = line.Split('\n');
                    foreach (string word in words)
                    {
                        if (word.Length > 0)
                        {
                            //move all files that start with word
                            string[] files = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", word + "*");
                            foreach (string file in files)
                            {
                                File.Move(file, folderLocaions[i] + "\\" + Path.GetFileName(file));
                            }
                        }
                    }
                }
            }
            //delete all .txt files in folder
            string[] txtFiles = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string file in txtFiles)
            {
                File.Delete(file);
            }
            //move AutoAssigment.csv file to folder C:\Users\mikoz\Desktop\doki\User4 named as hour minute and date of move
            File.Move(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv", @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\Logi\" + "SZZ " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".csv");
        }

        private void fileSystemWatcher3_Created(object sender, FileSystemEventArgs e)
        {
            //save every column of spreadsheet into separate text files in same folder
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string filePath1 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv";
            using (var stream = File.Open(filePath1, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    //every column of spreadsheet is saved into separate text file. Like column 1 is file1, column 2 is file2 etc.
                    while (reader.Read())
                    {
                        string filePath2 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + reader.GetValue(0) + ".txt";
                        using (StreamWriter sw = new StreamWriter(filePath2))
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                sw.WriteLine(reader.GetValue(i));
                            }
                        }
                    }
                }
            }
            //remove first line of every .txt file in folder. Move all other lines to first line.
            string[] filePaths = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 1)
                {
                    File.WriteAllLines(filePath, lines.Skip(1));
                }
            }
            //get file names from folder WITHOUT path
            string[] userNames = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            //remove path from fileNames
            for (int i = 0; i < userNames.Length; i++)
            {
                userNames[i] = Path.GetFileNameWithoutExtension(userNames[i]);
            }
            List<string> folderLocaions = new List<string>();
            //add to list folderLocations FolderLocation from database.mdf of specific user from list userNames
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\database.mdf;Version=3;"))
            {
                conn.Open();
                foreach (string user in userNames)
                {
                    string sql = "SELECT FolderLocation FROM Users WHERE Name = '" + user + "'";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                folderLocaions.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            //Move all files that contain charachters from .txt file, new line is a separator. .txt file name is user Name, move to user folder.
            for (int i = 0; i < userNames.Length; i++)
            {
                string[] lines = File.ReadAllLines(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + userNames[i] + ".txt");
                foreach (string line in lines)
                {
                    string[] words = line.Split('\n');
                    foreach (string word in words)
                    {
                        if (word.Length > 0)
                        {
                            //move all files that start with word
                            string[] files = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", word + "*");
                            foreach (string file in files)
                            {
                                File.Move(file, folderLocaions[i] + "\\" + Path.GetFileName(file));
                            }
                        }
                    }
                }
            }
            //delete all .txt files in folder
            string[] txtFiles = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string file in txtFiles)
            {
                File.Delete(file);
            }
            //move AutoAssigment.csv file to folder C:\Users\mikoz\Desktop\doki\User4 named as hour minute and date of move
            File.Move(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv", @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\Logi\" + "SZZ " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".csv");
        }

        private void fileSystemWatcher4_Created(object sender, FileSystemEventArgs e)
        {
            //save every column of spreadsheet into separate text files in same folder
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string filePath1 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv";
            using (var stream = File.Open(filePath1, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    //every column of spreadsheet is saved into separate text file. Like column 1 is file1, column 2 is file2 etc.
                    while (reader.Read())
                    {
                        string filePath2 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + reader.GetValue(0) + ".txt";
                        using (StreamWriter sw = new StreamWriter(filePath2))
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                sw.WriteLine(reader.GetValue(i));
                            }
                        }
                    }
                }
            }
            //remove first line of every .txt file in folder. Move all other lines to first line.
            string[] filePaths = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 1)
                {
                    File.WriteAllLines(filePath, lines.Skip(1));
                }
            }
            //get file names from folder WITHOUT path
            string[] userNames = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            //remove path from fileNames
            for (int i = 0; i < userNames.Length; i++)
            {
                userNames[i] = Path.GetFileNameWithoutExtension(userNames[i]);
            }
            List<string> folderLocaions = new List<string>();
            //add to list folderLocations FolderLocation from database.mdf of specific user from list userNames
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\database.mdf;Version=3;"))
            {
                conn.Open();
                foreach (string user in userNames)
                {
                    string sql = "SELECT FolderLocation FROM Users WHERE Name = '" + user + "'";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                folderLocaions.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            //Move all files that contain charachters from .txt file, new line is a separator. .txt file name is user Name, move to user folder.
            for (int i = 0; i < userNames.Length; i++)
            {
                string[] lines = File.ReadAllLines(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + userNames[i] + ".txt");
                foreach (string line in lines)
                {
                    string[] words = line.Split('\n');
                    foreach (string word in words)
                    {
                        if (word.Length > 0)
                        {
                            //move all files that start with word
                            string[] files = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", word + "*");
                            foreach (string file in files)
                            {
                                File.Move(file, folderLocaions[i] + "\\" + Path.GetFileName(file));
                            }
                        }
                    }
                }
            }
            //delete all .txt files in folder
            string[] txtFiles = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string file in txtFiles)
            {
                File.Delete(file);
            }
            //move AutoAssigment.csv file to folder C:\Users\mikoz\Desktop\doki\User4 named as hour minute and date of move
            File.Move(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv", @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\Logi\" + "SZZ " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".csv");
        }

        private void fileSystemWatcher5_Created(object sender, FileSystemEventArgs e)
        {
            //save every column of spreadsheet into separate text files in same folder
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string filePath1 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv";
            using (var stream = File.Open(filePath1, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    //every column of spreadsheet is saved into separate text file. Like column 1 is file1, column 2 is file2 etc.
                    while (reader.Read())
                    {
                        string filePath2 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + reader.GetValue(0) + ".txt";
                        using (StreamWriter sw = new StreamWriter(filePath2))
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                sw.WriteLine(reader.GetValue(i));
                            }
                        }
                    }
                }
            }
            //remove first line of every .txt file in folder. Move all other lines to first line.
            string[] filePaths = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 1)
                {
                    File.WriteAllLines(filePath, lines.Skip(1));
                }
            }
            //get file names from folder WITHOUT path
            string[] userNames = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            //remove path from fileNames
            for (int i = 0; i < userNames.Length; i++)
            {
                userNames[i] = Path.GetFileNameWithoutExtension(userNames[i]);
            }
            List<string> folderLocaions = new List<string>();
            //add to list folderLocations FolderLocation from database.mdf of specific user from list userNames
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\database.mdf;Version=3;"))
            {
                conn.Open();
                foreach (string user in userNames)
                {
                    string sql = "SELECT FolderLocation FROM Users WHERE Name = '" + user + "'";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                folderLocaions.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            //Move all files that contain charachters from .txt file, new line is a separator. .txt file name is user Name, move to user folder.
            for (int i = 0; i < userNames.Length; i++)
            {
                string[] lines = File.ReadAllLines(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + userNames[i] + ".txt");
                foreach (string line in lines)
                {
                    string[] words = line.Split('\n');
                    foreach (string word in words)
                    {
                        if (word.Length > 0)
                        {
                            //move all files that start with word
                            string[] files = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", word + "*");
                            foreach (string file in files)
                            {
                                File.Move(file, folderLocaions[i] + "\\" + Path.GetFileName(file));
                            }
                        }
                    }
                }
            }
            //delete all .txt files in folder
            string[] txtFiles = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string file in txtFiles)
            {
                File.Delete(file);
            }
            //move AutoAssigment.csv file to folder C:\Users\mikoz\Desktop\doki\User4 named as hour minute and date of move
            File.Move(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv", @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\Logi\" + "SZZ " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".csv");
        }

        private void fileSystemWatcher6_Created(object sender, FileSystemEventArgs e)
        {
            //save every column of spreadsheet into separate text files in same folder
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string filePath1 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv";
            using (var stream = File.Open(filePath1, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    //every column of spreadsheet is saved into separate text file. Like column 1 is file1, column 2 is file2 etc.
                    while (reader.Read())
                    {
                        string filePath2 = @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + reader.GetValue(0) + ".txt";
                        using (StreamWriter sw = new StreamWriter(filePath2))
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                sw.WriteLine(reader.GetValue(i));
                            }
                        }
                    }
                }
            }
            //remove first line of every .txt file in folder. Move all other lines to first line.
            string[] filePaths = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 1)
                {
                    File.WriteAllLines(filePath, lines.Skip(1));
                }
            }
            //get file names from folder WITHOUT path
            string[] userNames = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            //remove path from fileNames
            for (int i = 0; i < userNames.Length; i++)
            {
                userNames[i] = Path.GetFileNameWithoutExtension(userNames[i]);
            }
            List<string> folderLocaions = new List<string>();
            //add to list folderLocations FolderLocation from database.mdf of specific user from list userNames
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\database.mdf;Version=3;"))
            {
                conn.Open();
                foreach (string user in userNames)
                {
                    string sql = "SELECT FolderLocation FROM Users WHERE Name = '" + user + "'";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                folderLocaions.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            //Move all files that contain charachters from .txt file, new line is a separator. .txt file name is user Name, move to user folder.
            for (int i = 0; i < userNames.Length; i++)
            {
                string[] lines = File.ReadAllLines(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\" + userNames[i] + ".txt");
                foreach (string line in lines)
                {
                    string[] words = line.Split('\n');
                    foreach (string word in words)
                    {
                        if (word.Length > 0)
                        {
                            //move all files that start with word
                            string[] files = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", word + "*");
                            foreach (string file in files)
                            {
                                File.Move(file, folderLocaions[i] + "\\" + Path.GetFileName(file));
                            }
                        }
                    }
                }
            }
            //delete all .txt files in folder
            string[] txtFiles = Directory.GetFiles(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\", "*.txt");
            foreach (string file in txtFiles)
            {
                File.Delete(file);
            }
            //move AutoAssigment.csv file to folder C:\Users\mikoz\Desktop\doki\User4 named as hour minute and date of move
            File.Move(@"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\AutoAssigment.csv", @"C:\Users\mikujawa\Desktop\Prezentacja\OTRS2\Logi\" + "SZZ " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".csv");
        }
    }
}

