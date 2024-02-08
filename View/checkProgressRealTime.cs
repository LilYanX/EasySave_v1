using System;
using System.IO;
using System.Threading;

namespace CheckProgress
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManagement fileManagement = new FileManagement();
            fileManagement.CompareAndCopyFilesWithProgress();
        }
    }

    class FileManagement
    {
        public void CompareAndCopyFilesWithProgress()
        {
            Console.WriteLine("Entrez le chemin du dossier source :");
            string folderPath1 = Console.ReadLine();

            Console.WriteLine("Entrez le chemin du dossier destination :");
            string folderPath2 = Console.ReadLine();

            // Obtenir la liste des fichiers dans le premier dossier
            string[] files = Directory.GetFiles(folderPath1);

            Console.WriteLine("Progression de la copie: ");

            // Parcourir chaque fichier dans le dossier 1
            foreach (string filePath1 in files)
            {
                // Obtenir le nom du fichier
                string fileName = Path.GetFileName(filePath1);

                // Chemin vers le fichier dans le dossier destination
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
                        // Copier le fichier du premier emplacement vers le deuxième emplacement avec la progression
                        CopyFileWithProgress(filePath1, filePath2);
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
                    // Copier le fichier du premier emplacement vers le deuxième emplacement avec la progression
                    CopyFileWithProgress(filePath1, filePath2);
                    Console.WriteLine($"Le fichier '{fileName}' a été copié de {folderPath1} vers {folderPath2} car il n'existait pas dans {folderPath2}.");
                }
            }

            // Attendre une entrée de l'utilisateur avant de se fermer
            Console.WriteLine("Appuyez sur n'importe quelle touche pour quitter...");
            Console.ReadKey();
        }

        private void CopyFileWithProgress(string sourceFilePath, string destinationFilePath)
        {
            using (FileStream sourceStream = File.Open(sourceFilePath, FileMode.Open))
            {
                using (FileStream destinationStream = File.Create(destinationFilePath))
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    long totalBytes = new FileInfo(sourceFilePath).Length;
                    long copiedBytes = 0;
                    int lastProgressPercentage = 0;

                    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destinationStream.Write(buffer, 0, bytesRead);
                        copiedBytes += bytesRead;

                        // Calculer la progression
                        int progressPercentage = (int)((double)copiedBytes / totalBytes * 100);

                        if (progressPercentage != lastProgressPercentage)
                        {
                            // Afficher la progression uniquement si elle a changé
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write($"Progression de la copie: {progressPercentage}%");
                            lastProgressPercentage = progressPercentage;
                        }
                    }
                    Console.WriteLine(); // Nouvelle ligne après la copie complète
                }
            }
        }
    }
}