using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_v1
{
    public class Repository
    {
        protected string Name;
        protected string SourceDirectory;
        protected string TargetDirectory;

        public Repository() { }
        public string getName() 
        {
            return Name;
        }

        public string getSourceDirectory() 
        {
            return SourceDirectory;
        }

        public string getTargetDirectory()
        {
            return TargetDirectory;
        }
    }
}
