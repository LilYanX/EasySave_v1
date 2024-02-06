using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EasySave_v1
{
    class Backup
    {
		private string Name;
		private string SourceDirectory;
		private string TargetDirectory;
		private string BackupType;

		public Backup(string Name)
		{
			this.Name = Name;
		}
		public Backup(string Name, string SourceDirectory, string TargetDirectory)
		{
			this.Name = Name;
			this.TargetDirectory = TargetDirectory;
			this.SourceDirectory = SourceDirectory;
		}
		public void CreateTargetDirectory(string PathTarget)
		{
			this.TargetDirectory = PathTarget;

			//Create directory if it doesn't already exist
			if (!Directory.Exists(PathTarget))
			{
				Directory.CreateDirectory(PathTarget);
			}
		}
		public string GetTargetDirectory()
		{
			return TargetDirectory;
		}
		public void CompareElement(string PathTarget, string PathSource, string BackupType)
		{

			this.BackupType = BackupType;
			this.SourceDirectory = PathSource;
			this.TargetDirectory = PathTarget;

			DirectoryInfo Source = new DirectoryInfo(PathSource);
			DirectoryInfo Target = new DirectoryInfo(PathTarget);

			// Take a snapshot of the file system.  
			IEnumerable<FileInfo> list1 = Source.GetFiles("*.*", SearchOption.AllDirectories);
			IEnumerable<FileInfo> list2 = Target.GetFiles("*.*", SearchOption.AllDirectories);

			//A custom file comparer defined below  
			FileCompare myFileCompare = new FileCompare();

			// This query determines whether the two folders contain  
			// identical file lists, based on the custom file comparer  
			// that is defined in the FileCompare class.  
			// The query executes immediately because it returns a bool.  
			bool areIdentical = list1.SequenceEqual(list2, myFileCompare);

			if (areIdentical == true)
			{
				Console.WriteLine("the two folders are the same");
			}
			else
			{
				Console.WriteLine("The two folders are not the same");
			}

			// Find the common files. It produces a sequence and doesn't
			// execute until the foreach statement.  
			var queryCommonFiles = list1.Intersect(list2, myFileCompare);

			if (queryCommonFiles.Any())
			{
				Console.WriteLine("The following files are in both folders:");
				foreach (var v in queryCommonFiles)
				{
					Console.WriteLine(v.FullName); //shows which items end up in result list  
				}
			}
			else
			{
				Console.WriteLine("There are no common files in the two folders.");

				// Modify the files that are not similar
				foreach (var file in list1.Except(list2, myFileCompare))
				{
					Console.WriteLine($"Modifying file: {file.FullName}");

				}
			}

			// Find the set difference between the two folders.  
			// For this example we only check one way.  
			var queryList1Only = (from file in list1
								  select file).Except(list2, myFileCompare);

			Console.WriteLine("The following files are in list1 but not list2:");
			foreach (var v in queryList1Only)
			{
				Console.WriteLine(v.FullName);
			}
		}
		public void CopyRepertory(string PathTarget, string PathSource)
		{
			this.TargetDirectory = PathTarget;
			this.SourceDirectory = PathSource;

			//Create All Repertories
			foreach (string AllDirectory in Directory.GetDirectories(PathSource, "*", SearchOption.AllDirectories))
			{
				Directory.CreateDirectory(AllDirectory.Replace(PathSource, PathTarget));
			}

			//Copy Files
			foreach (string AllFiles in Directory.GetFiles(PathSource, "*.*", SearchOption.AllDirectories))
			{
				File.Copy(AllFiles, AllFiles.Replace(PathSource, PathTarget), true);
			}
		}
	}
}
