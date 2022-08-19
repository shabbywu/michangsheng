using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000586 RID: 1414
	public class PathFilter : IScanFilter
	{
		// Token: 0x06002E68 RID: 11880 RVA: 0x00151930 File Offset: 0x0014FB30
		public PathFilter(string filter)
		{
			this.nameFilter_ = new NameFilter(filter);
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x00151944 File Offset: 0x0014FB44
		public virtual bool IsMatch(string name)
		{
			bool result = false;
			if (name != null)
			{
				string name2 = (name.Length > 0) ? Path.GetFullPath(name) : "";
				result = this.nameFilter_.IsMatch(name2);
			}
			return result;
		}

		// Token: 0x040028E3 RID: 10467
		private readonly NameFilter nameFilter_;
	}
}
