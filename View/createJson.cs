using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConfigurationBackup
{
    class createJson
    {
        static string directoryPath = @"C:\JSON";
        static string filePath = Path.Combine(directoryPath, "confbackup.json");
        const int MaxBackupSettings = 5;

        static void Main(string[] args)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            List<BackupSetting> backupSettings = LoadBackupSettings();

            while (true)
            {
                Console.WriteLine("\nQue souhaitez-vous faire ?");
                Console.WriteLine("1. Afficher les sauvegardes actuelles");
                Console.WriteLine("2. Ajouter une nouvelle sauvegarde");
                Console.WriteLine("3. Modifier une sauvegarde existante");
                Console.WriteLine("4. Supprimer une sauvegarde existante");
                Console.WriteLine("5. Quitter");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        DisplayBackupSettings(backupSettings);
                        break;
                    case 2:
                        if (backupSettings.Count < MaxBackupSettings)
                        {
                            AddNewBackupSetting(backupSettings);
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Le nombre maximal de sauvegardes a déjà été atteint.");
                        }
                        break;
                    case 3:
                        ModifyBackupSetting(backupSettings);
                        Console.Clear();
                        break;
                    case 4:
                        DeleteBackupSetting(backupSettings);
                        Console.Clear();
                        break;
                    case 5:
                        SaveBackupSettings(backupSettings);
                        Console.WriteLine("Fermeture de l'application.");
                        return;
                    default:
                        Console.WriteLine("Choix non valide.");
                        break;
                }
            }
        }

        static List<BackupSetting> LoadBackupSettings()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<BackupSetting>>(json);
            }
            else
            {
                return new List<BackupSetting>(); // Retourne une nouvelle liste vide si le fichier n'existe pas
            }
        }

        static void SaveBackupSettings(List<BackupSetting> backupSettings)
        {
            if (backupSettings != null)
            {
                string json = JsonConvert.SerializeObject(backupSettings, Formatting.Indented);
                File.WriteAllText(filePath, json);
                Console.WriteLine("Les sauvegardes ont été enregistrées avec succès dans le fichier JSON.");
            }
            else
            {
                Console.WriteLine("Aucune sauvegarde à enregistrer.");
            }
        }

        static void DisplayBackupSettings(List<BackupSetting> backupSettings)
        {
            if (backupSettings.Count == 0)
            {
                Console.WriteLine("Aucune sauvegarde n'est actuellement configurée.");
            }
            else
            {
                foreach (var setting in backupSettings)
                {
                    Console.WriteLine($"Nom: {setting.Name}, Source: {setting.SourcePath}, Destination: {setting.DestinationPath}, Type: {setting.Type}");
                }
            }
        }

        static void AddNewBackupSetting(List<BackupSetting> backupSettings)
        {
            Console.WriteLine("Entrez le nom de la nouvelle sauvegarde :");
            string name = Console.ReadLine();

            Console.WriteLine("Entrez le chemin source :");
            string sourcePath = Console.ReadLine();

            Console.WriteLine("Entrez le chemin de destination :");
            string destinationPath = Console.ReadLine();

            Console.WriteLine("Entrez le type de sauvegarde (Full ou Differential) :");
            BackupType type;
            if (Enum.TryParse(Console.ReadLine(), true, out type))
            {
                if (backupSettings == null)
                {
                    backupSettings = new List<BackupSetting>();
                }
                backupSettings.Add(new BackupSetting { Name = name, SourcePath = sourcePath, DestinationPath = destinationPath, Type = type });
            }
            else
            {
                Console.WriteLine("Type de sauvegarde invalide.");
            }
        }

        static void ModifyBackupSetting(List<BackupSetting> backupSettings)
        {
            Console.WriteLine("Entrez le nom de la sauvegarde à modifier :");
            string nameToModify = Console.ReadLine();

            var settingToModify = backupSettings.Find(s => s.Name == nameToModify);

            if (settingToModify != null)
            {
                Console.WriteLine($"Sauvegarde trouvée : Nom: {settingToModify.Name}, Source: {settingToModify.SourcePath}, Destination: {settingToModify.DestinationPath}, Type: {settingToModify.Type}");

                Console.WriteLine("Entrez le nouveau nom (ou appuyez sur Entrée pour garder le même) :");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                    settingToModify.Name = newName;

                Console.WriteLine("Entrez le nouveau chemin source (ou appuyez sur Entrée pour garder le même) :");
                string newSourcePath = Console.ReadLine();
                if (!string.IsNullOrEmpty(newSourcePath))
                    settingToModify.SourcePath = newSourcePath;

                Console.WriteLine("Entrez le nouveau chemin de destination (ou appuyez sur Entrée pour garder le même) :");
                string newDestinationPath = Console.ReadLine();
                if (!string.IsNullOrEmpty(newDestinationPath))
                    settingToModify.DestinationPath = newDestinationPath;

                Console.WriteLine("Entrez le nouveau type de sauvegarde (Full ou Differential) (ou appuyez sur Entrée pour garder le même) :");
                string typeInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(typeInput))
                {
                    BackupType newType;
                    if (Enum.TryParse(typeInput, true, out newType))
                        settingToModify.Type = newType;
                }

                Console.WriteLine("Sauvegarde modifiée avec succès.");
            }
            else
            {
                Console.WriteLine("Aucune sauvegarde trouvée avec ce nom.");
            }
        }

        static void DeleteBackupSetting(List<BackupSetting> backupSettings)
        {
            Console.WriteLine("Entrez le nom de la sauvegarde à supprimer :");
            string nameToDelete = Console.ReadLine();

            var settingToDelete = backupSettings.Find(s => s.Name == nameToDelete);

            if (settingToDelete != null)
            {
                backupSettings.Remove(settingToDelete);
                Console.WriteLine("Sauvegarde supprimée avec succès.");
            }
            else
            {
                Console.WriteLine("Aucune sauvegarde trouvée avec ce nom.");
            }
        }
    }

    public class BackupSetting
    {
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public BackupType Type { get; set; }
    }

    public enum BackupType
    {
        Full,
        Differential
    }
}
