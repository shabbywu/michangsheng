using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000587 RID: 1415
	public class ExtendedPathFilter : PathFilter
	{
		// Token: 0x06002E6A RID: 11882 RVA: 0x0015197B File Offset: 0x0014FB7B
		public ExtendedPathFilter(string filter, long minSize, long maxSize) : base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x001519B7 File Offset: 0x0014FBB7
		public ExtendedPathFilter(string filter, DateTime minDate, DateTime maxDate) : base(filter)
		{
			this.MinDate = minDate;
			this.MaxDate = maxDate;
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x001519F4 File Offset: 0x0014FBF4
		public ExtendedPathFilter(string filter, long minSize, long maxSize, DateTime minDate, DateTime maxDate) : base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
			this.MinDate = minDate;
			this.MaxDate = maxDate;
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x00151A4C File Offset: 0x0014FC4C
		public override bool IsMatch(string name)
		{
			bool flag = base.IsMatch(name);
			if (flag)
			{
				FileInfo fileInfo = new FileInfo(name);
				flag = (this.MinSize <= fileInfo.Length && this.MaxSize >= fileInfo.Length && this.MinDate <= fileInfo.LastWriteTime && this.MaxDate >= fileInfo.LastWriteTime);
			}
			return flag;
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x00151AB0 File Offset: 0x0014FCB0
		// (set) Token: 0x06002E6F RID: 11887 RVA: 0x00151AB8 File Offset: 0x0014FCB8
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

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x00151ADA File Offset: 0x0014FCDA
		// (set) Token: 0x06002E71 RID: 11889 RVA: 0x00151AE2 File Offset: 0x0014FCE2
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

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x00151B04 File Offset: 0x0014FD04
		// (set) Token: 0x06002E73 RID: 11891 RVA: 0x00151B0C File Offset: 0x0014FD0C
		public DateTime MinDate
		{
			get
			{
				return this.minDate_;
			}
			set
			{
				if (value > this.maxDate_)
				{
					throw new ArgumentOutOfRangeException("value", "Exceeds MaxDate");
				}
				this.minDate_ = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x00151B33 File Offset: 0x0014FD33
		// (set) Token: 0x06002E75 RID: 11893 RVA: 0x00151B3B File Offset: 0x0014FD3B
		public DateTime MaxDate
		{
			get
			{
				return this.maxDate_;
			}
			set
			{
				if (this.minDate_ > value)
				{
					throw new ArgumentOutOfRangeException("value", "Exceeds MinDate");
				}
				this.maxDate_ = value;
			}
		}

		// Token: 0x040028E4 RID: 10468
		private long minSize_;

		// Token: 0x040028E5 RID: 10469
		private long maxSize_ = long.MaxValue;

		// Token: 0x040028E6 RID: 10470
		private DateTime minDate_ = DateTime.MinValue;

		// Token: 0x040028E7 RID: 10471
		private DateTime maxDate_ = DateTime.MaxValue;
	}
}
