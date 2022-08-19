using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000588 RID: 1416
	[Obsolete("Use ExtendedPathFilter instead")]
	public class NameAndSizeFilter : PathFilter
	{
		// Token: 0x06002E76 RID: 11894 RVA: 0x00151B62 File Offset: 0x0014FD62
		public NameAndSizeFilter(string filter, long minSize, long maxSize) : base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x00151B88 File Offset: 0x0014FD88
		public override bool IsMatch(string name)
		{
			bool flag = base.IsMatch(name);
			if (flag)
			{
				long length = new FileInfo(name).Length;
				flag = (this.MinSize <= length && this.MaxSize >= length);
			}
			return flag;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x00151BC6 File Offset: 0x0014FDC6
		// (set) Token: 0x06002E79 RID: 11897 RVA: 0x00151BCE File Offset: 0x0014FDCE
		public long MinSize
		{
			get
			{
				return this.minSize_;
			}
			set
			{
				if (value < 0L || this.maxSize_ < value)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.minSize_ = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x00151BF0 File Offset: 0x0014FDF0
		// (set) Token: 0x06002E7B RID: 11899 RVA: 0x00151BF8 File Offset: 0x0014FDF8
		public long MaxSize
		{
			get
			{
				return this.maxSize_;
			}
			set
			{
				if (value < 0L || this.minSize_ > value)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxSize_ = value;
			}
		}

		// Token: 0x040028E8 RID: 10472
		private long minSize_;

		// Token: 0x040028E9 RID: 10473
		private long maxSize_ = long.MaxValue;
	}
}
