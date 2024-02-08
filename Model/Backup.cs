using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Serilog;
using Newtonsoft.Json;
namespace EasySave_v1
{
	public class Backup
	{
		protected string BackupType;
		protected string ChooseBackup;
		protected int NumberBackup;
		private Repository _Repository = new Repository();
		protected List<Tuple<int, string, string, string, string>> backupInfoList = new List<Tuple<int, string, string, string, string>>();
		public Backup() { }
		public void createBackup(int NumberBackup, string Name, string SourceDirectory, string TargetDirectory, string Type)
		{
			Name = _Repository.getName();
			SourceDirectory = _Repository.getSourceDirectory();
			TargetDirectory = _Repository.getTargetDirectory();
			this.NumberBackup = NumberBackup;
			this.BackupType = Type;
			backupInfoList.Add(new Tuple<int, string, string, string, string>(NumberBackup, Name, SourceDirectory, TargetDirectory, Type));
			Log.Information("Creation of backup ", Name);
		}

		public void CreateTargetDirectory(string PathTarget)
		{
			PathTarget = _Repository.getTargetDirectory();

			//Create directory if it doesn't already exist
			if (!Directory.Exists(PathTarget))
			{
				Directory.CreateDirectory(PathTarget);
				Log.Information("Creation of directory ", PathTarget);
			}
		}
		public void JsonConvertBackup(List<Tuple<int, string, string, string, string>> backupliste)
		{
			string json = JsonConvert.SerializeObject(backupliste, Formatting.Indented);
			CreateTargetDirectory(@"C:\Temp");
			File.WriteAllText(@"C:\Temp\backuplist.json", json);
		}
	}
}