using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EasySave_v1
{
	class FullBackup : Backup
	{
		private FileManagement _FileManagement = new FileManagement();
		public FullBackup() { }

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
								_FileManagement.CopyRepertory(backupInfo.Item4, backupInfo.Item3);
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
								_FileManagement.CopyRepertory(backupInfo.Item4, backupInfo.Item3);
							}
						}
					}
				}
			}
		}
	}
}