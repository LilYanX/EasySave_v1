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

            Backup FirstBackup = new Backup(Name, SourceDirectory, TargetDirectory);
            Log.Information("Application started");

            while (true)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("| 1 -- Backup   |");
                Console.WriteLine("-----------------");
                Console.WriteLine("| 2 -- Modify   |");
                Console.WriteLine("-----------------");
                Console.WriteLine("| 3 -- Delete   |");
                Console.WriteLine("-----------------");
                Console.WriteLine("| 4 -- Quit     |");
                Console.WriteLine("-----------------");
                Console.WriteLine("\nEnter your choose : ");
                string Choose = Console.ReadLine();
                switch (Choose)
                {
                    //Create a TargetRepertory on local machine
                    case "Backup":
                        try
                        {
                            Console.WriteLine("Enter Target Directory :");
                            TargetDirectory = Console.ReadLine();
                            Console.WriteLine("Enter Directory Name :");
                            Name = Console.ReadLine();
                            TargetDirectory = TargetDirectory + @"\" + Name;
                            FirstBackup.CreateTargetDirectory(TargetDirectory);
                            Console.WriteLine("Repository has been created successfully!");
                            Log.Information("Repository has been created successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error creating target directory : {ex.Message}");
                            Log.Error(ex, "Error creating target directory");
                        }
                        break;
                    //Backup a source directory into target directory 
                    case "Modify":
                        try
                        {
                            Console.WriteLine("\nEnter Source Directory :");
                            SourceDirectory = Console.ReadLine();
                            FirstBackup.CopyRepertory(TargetDirectory, SourceDirectory);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error backup : {ex.Message}");
                            Log.Error(ex, "Error backup");
                        }
                        break;
                    case "Delete":
                        FirstBackup.CompareElement(TargetDirectory, SourceDirectory, "Complet");
                        break;
                    case "Quit":
                        Log.Information("Close the application succesfully");
                        Environment.Exit(0);
                        break;

                }
            }
        }
    }
}
