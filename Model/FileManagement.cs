using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Serilog;
namespace EasySave_v1
{
    class FileManagement : Repository
    {
        public FileManagement() { }

        public void CopyRepertory(string PathTarget, string PathSource)
        {
            this.TargetDirectory = PathTarget;
            this.SourceDirectory = PathSource;
            //Create All Repertories
            foreach (string AllDirectory in Directory.GetDirectories(PathSource, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(AllDirectory.Replace(PathSource, PathTarget));
            }
            Log.Information("Creation of all repertories for copy");

            //Copy Files
            foreach (string AllFiles in Directory.GetFiles(PathSource, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(AllFiles, AllFiles.Replace(PathSource, PathTarget), true);
            }
            Log.Information("Copy all repertories on ", PathTarget);
        }

        public void CompareElement()
        {
            Console.WriteLine("Entrez le chemin du dossier source :");
            string folderPath1 = Console.ReadLine();
            Console.WriteLine("Entrez le chemin du dossier destination :");
            string folderPath2 = Console.ReadLine();
            // Obtenir la liste des fichiers dans le premier dossier
            string[] files = Directory.GetFiles(folderPath1);
            // Parcourir chaque fichier dans le dossier 2
            foreach (string filePath2 in Directory.GetFiles(folderPath2))
            {
                // Vérifier si le fichier existe dans le dossier 1
                string fileName = Path.GetFileName(filePath2);
                string filePath1 = Path.Combine(folderPath1, fileName);
                if (!File.Exists(filePath1))
                {
                    File.Delete(filePath2);
                    Console.WriteLine($"Le fichier '{fileName}' a été supprimé de {folderPath2} car il n'existe plus dans {folderPath1}.");
                }
            }
            // Parcourir chaque fichier dans le dossier 1
            foreach (string filePath1 in files)
            {
                // Obtenir le nom du fichier
                string fileName = Path.GetFileName(filePath1);
                // Chemin vers le fichier dans le dossier 2
                string filePath2 = Path.Combine(folderPath2, fileName);
                // Vérifie si le fichier existe dans le dossier 2
                if (File.Exists(filePath2))
                {
                    // Obtenir la date de dernière modification des deux fichiers
                    DateTime lastModified1 = File.GetLastWriteTime(filePath1);
                    DateTime lastModified2 = File.GetLastWriteTime(filePath2);
                    // Comparer les dates
                    if (lastModified1 > lastModified2)
                    {
                        // Copier le fichier du premier emplacement vers le deuxième emplacement
                        File.Copy(filePath1, filePath2, true);
                        Console.WriteLine($"Le fichier '{fileName}' dans {folderPath1} a été copié vers {folderPath2} car il a été modifié plus récemment.");
                    }
                    else if (lastModified1 < lastModified2)
                    {
                        Console.WriteLine($"Le fichier '{fileName}' dans {folderPath2} a été modifié après le fichier dans {folderPath1}.");
                    }
                    else
                    {
                        Console.WriteLine($"Les fichiers '{fileName}' ont été modifiés à la même date.");
                    }
                }
                else
                {
                    // Copier le fichier du premier emplacement vers le deuxième emplacement
                    File.Copy(filePath1, filePath2);
                    Console.WriteLine($"Le fichier '{fileName}' a été copié de {folderPath1} vers {folderPath2} car il n'existait pas dans {folderPath2}.");
                }
            }
            // Attendre une entrée de l'utilisateur avant de se fermer
            Console.WriteLine("Appuyez sur n'importe quelle touche pour quitter...");
            Console.ReadKey();
        }

        public void CreateTargetDirectory(string PathTarget)
        {
            this.TargetDirectory = PathTarget;

            //Create directory if it doesn't already exist
            if (!Directory.Exists(PathTarget))
            {
                Directory.CreateDirectory(PathTarget);
                Log.Information("Creation of directory ", PathTarget);

            }
        }
        public string GetTargetDirectory()
        {
            return TargetDirectory;
        }

    }
}