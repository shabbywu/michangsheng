using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200082F RID: 2095
	public class PathFilter : IScanFilter
	{
		// Token: 0x060036DE RID: 14046 RVA: 0x00027E2E File Offset: 0x0002602E
		public PathFilter(string filter)
		{
			this.nameFilter_ = new NameFilter(filter);
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x0019C778 File Offset: 0x0019A978
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

		// Token: 0x04003122 RID: 12578
		private readonly NameFilter nameFilter_;
	}
}
