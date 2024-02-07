using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Serilog;

namespace EasySave_v1
{
	public class Backup
	{
		private string Name;
		private string SourceDirectory;
		private string TargetDirectory;
		private string BackupType;
		private string ChooseBackup;

		private int NumberBackup;
		private List<Tuple<int, string, string, string, string>> backupInfoList = new List<Tuple<int, string, string, string, string>>();
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
				Log.Information("Creation of directory ", PathTarget);
				
			}
		}
		public string GetTargetDirectory()
		{
			return TargetDirectory;
		}

		public void createBackup(int NumberBackup, string Name, string SourceDirectory, string TargetDirectory, string Type)
		{
			this.NumberBackup = NumberBackup;
			this.BackupType = Type;

			backupInfoList.Add(new Tuple<int, string, string, string, string>(NumberBackup, Name, SourceDirectory, TargetDirectory, Type));
			Log.Information("Creation of backup ", Name);
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
			Log.Information("Creation of all repertories for copy");

			//Copy Files
			foreach (string AllFiles in Directory.GetFiles(PathSource, "*.*", SearchOption.AllDirectories))
			{
				File.Copy(AllFiles, AllFiles.Replace(PathSource, PathTarget), true);
			}
			Log.Information("Copy all repertories on ", PathTarget);
		}

		public void ExecBackup(string ChooseBackup)
		{
			this.ChooseBackup = ChooseBackup;

			string[] parts = ChooseBackup.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var backupInfo in backupInfoList)
			{
				foreach (string part in parts)
				{
					if (part.Contains("-"))
					{
						string[] rangeParts = part.Split('-');
						int start = int.Parse(rangeParts[0]);
						int end = int.Parse(rangeParts[1]);

						for (int i = start; i <= end; i++)
						{
							if (i == backupInfo.Item1)
							{
								CopyRepertory(backupInfo.Item4, backupInfo.Item3);
							}
						}
					}
					else
					{
						int number;
						if (int.TryParse(part, out number))
						{
							if (number == backupInfo.Item1)
							{
								CopyRepertory(backupInfo.Item4, backupInfo.Item3);
							}
						}
					}
				}
			}
		}

	}
}