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

            Backup FirstBackup = new Backup(Name, SourceDirectory, TargetDirectory);

            Log.Information("Start of application");

            while (true)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("| 1 -- Backup   |");
                Console.WriteLine("-----------------");
                Console.WriteLine("| 2 -- Quit     |");
                Console.WriteLine("-----------------");
                Console.WriteLine("\nType your choice : ");
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
                        Console.WriteLine("\nType your choice : ");
                        Choose = Console.ReadLine();
                        switch (Choose)
                        {
                            case "1":
                                Console.WriteLine("Type the amount of back-up desired");
                                backupCount = Console.ReadLine();
                                backupCountInt = int.Parse(backupCount);

                                if (backupCountInt >= 1 & backupCountInt <= 5)
                                {
                                    while (NumberBackup != backupCountInt)
                                    {
                                        //Donner le nom du répertoire cible
                                        Console.WriteLine("Type Directory Name :");
                                        Name = Console.ReadLine();

                                        //Selectionner le chemin répertoire source
                                        Console.WriteLine("Type Source Directory :");
                                        SourceDirectory = Console.ReadLine();

                                        //Selectionner le chemin répertoire cible
                                        Console.WriteLine("Type Target Directory :");
                                        TargetDirectory = Console.ReadLine();

                                        //Selectionner le type de back-up
                                        Console.WriteLine("Type the type Backup");
                                        Type = Console.ReadLine();

                                        //Combiner le chemin avec le nom
                                        TargetDirectory = TargetDirectory + @"\" + Name;
                                        Console.WriteLine("Repository has been created successfully!");

                                        Log.Information("Repository has been created successfully!");

                                        NumberBackup++; // Incrémenter le compteur de sauvegardes créées

                                        FirstBackup.createBackup(NumberBackup, Name, SourceDirectory, TargetDirectory, Type);
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("Error!");
                                    Log.Error("Error");
                                }
                                break;

                            case "2":
                                Console.WriteLine("Type backup :");
                                string ChooseBackup = Console.ReadLine();

                                FirstBackup.ExecBackup(ChooseBackup);
                                break;
                        }
                        break;
                    //Quit
                    case "2":
                        Log.Information("Leaving application successfully");
                        Environment.Exit(0);
                        break;

                }
            }
        }
    }
}