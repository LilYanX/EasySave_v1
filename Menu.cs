using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace EasySave_v1
{
    class Menu
    {
        public void script()
        {
            string Name = "";
            string TargetDirectory = "";
            string SourceDirectory = "";
            string Type = "";

            //Total Backup
            string backupCount = "";
            int backupCountInt = 0;
            int NumberBackup = 0;

            List<Tuple<int, string, string, string, string>> backupInfoList = new List<Tuple<int, string, string, string, string>>();

            Backup FirstBackup = new Backup(Name, SourceDirectory, TargetDirectory);
            Log.Information("Application started");

            while (true)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("| 1 -- Backup   |");
                Console.WriteLine("-----------------");
                Console.WriteLine("| 2 -- Quit     |");
                Console.WriteLine("-----------------");
                Console.WriteLine("\nEnter your choose : ");
                string Choose = Console.ReadLine();
                switch (Choose)
                {
                    //Backup
                    case "1":
                        Console.WriteLine("-----------------");
                        Console.WriteLine("| 1 -- Create   |");
                        Console.WriteLine("-----------------");
                        Console.WriteLine("| 2 -- Select   |");
                        Console.WriteLine("-----------------");
                        Console.WriteLine("\nEnter your choose : ");
                        Choose = Console.ReadLine();
                        switch (Choose)
                        {
                            case "1":
                                Console.WriteLine("Enter this amount of back-up");
                                backupCount = Console.ReadLine();
                                backupCountInt = int.Parse(backupCount);

                                if (backupCountInt >= 1 & backupCountInt <= 5)
                                {
                                    while (NumberBackup != backupCountInt)
                                    {
                                        //Donner le nom du répertoire cible
                                        Console.WriteLine("Enter Directory Name :");
                                        Name = Console.ReadLine();

                                        //Selectionner le chemin répertoire source
                                        Console.WriteLine("Enter Source Directory :");
                                        SourceDirectory = Console.ReadLine();

                                        //Selectionner le chemin répertoire cible
                                        Console.WriteLine("Enter Target Directory :");
                                        TargetDirectory = Console.ReadLine();

                                        //Selectionner le type de back-up
                                        Console.WriteLine("Enter Type Backup (Full/Diff)");
                                        Type = Console.ReadLine();

                                        //Combiner le chemin avec le nom
                                        TargetDirectory = TargetDirectory + @"\" + Name;
                                        Console.WriteLine("Repository has been created successfully!");

                                        NumberBackup++; // Incrémenter le compteur de sauvegardes créées

                                        // Store the values in the list
                                        backupInfoList.Add(new Tuple<int, string, string, string, string>(NumberBackup, Name, SourceDirectory, TargetDirectory, Type)); ; ;
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("Erreur!");
                                }
                                break;

                            case "2":
                                // Access the stored values
                                foreach (var backupInfo in backupInfoList)
                                {
                                    Console.WriteLine($"Backup: {backupInfo.Item1} Name: {backupInfo.Item2}, Source Directory: {backupInfo.Item3}, Target Directory: {backupInfo.Item4}, Type: {backupInfo.Item5}");
                                }
                                break;
                        }
                        break;
                    //Quit
                    case "2":
                        Environment.Exit(0);
                        break;

                }
            }
        }
    }
}
