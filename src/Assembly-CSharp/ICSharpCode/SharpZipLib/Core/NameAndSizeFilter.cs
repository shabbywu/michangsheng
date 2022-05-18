using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000831 RID: 2097
	[Obsolete("Use ExtendedPathFilter instead")]
	public class NameAndSizeFilter : PathFilter
	{
		// Token: 0x060036EC RID: 14060 RVA: 0x00027F6C File Offset: 0x0002616C
		public NameAndSizeFilter(string filter, long minSize, long maxSize) : base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x0019C86C File Offset: 0x0019AA6C
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

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x00027F92 File Offset: 0x00026192
		// (set) Token: 0x060036EF RID: 14063 RVA: 0x00027F9A File Offset: 0x0002619A
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

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x00027FBC File Offset: 0x000261BC
		// (set) Token: 0x060036F1 RID: 14065 RVA: 0x00027FC4 File Offset: 0x000261C4
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

		// Token: 0x04003127 RID: 12583
		private long minSize_;

		// Token: 0x04003128 RID: 12584
		private long maxSize_ = long.MaxValue;
	}
}
