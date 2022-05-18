using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000830 RID: 2096
	public class ExtendedPathFilter : PathFilter
	{
		// Token: 0x060036E0 RID: 14048 RVA: 0x00027E42 File Offset: 0x00026042
		public ExtendedPathFilter(string filter, long minSize, long maxSize) : base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x00027E7E File Offset: 0x0002607E
		public ExtendedPathFilter(string filter, DateTime minDate, DateTime maxDate) : base(filter)
		{
			this.MinDate = minDate;
			this.MaxDate = maxDate;
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x0019C7B0 File Offset: 0x0019A9B0
		public ExtendedPathFilter(string filter, long minSize, long maxSize, DateTime minDate, DateTime maxDate) : base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
			this.MinDate = minDate;
			this.MaxDate = maxDate;
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x0019C808 File Offset: 0x0019AA08
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

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x00027EBA File Offset: 0x000260BA
		// (set) Token: 0x060036E5 RID: 14053 RVA: 0x00027EC2 File Offset: 0x000260C2
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

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060036E6 RID: 14054 RVA: 0x00027EE4 File Offset: 0x000260E4
		// (set) Token: 0x060036E7 RID: 14055 RVA: 0x00027EEC File Offset: 0x000260EC
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

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060036E8 RID: 14056 RVA: 0x00027F0E File Offset: 0x0002610E
		// (set) Token: 0x060036E9 RID: 14057 RVA: 0x00027F16 File Offset: 0x00026116
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

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x00027F3D File Offset: 0x0002613D
		// (set) Token: 0x060036EB RID: 14059 RVA: 0x00027F45 File Offset: 0x00026145
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

		// Token: 0x04003123 RID: 12579
		private long minSize_;

		// Token: 0x04003124 RID: 12580
		private long maxSize_ = long.MaxValue;

		// Token: 0x04003125 RID: 12581
		private DateTime minDate_ = DateTime.MinValue;

		// Token: 0x04003126 RID: 12582
		private DateTime maxDate_ = DateTime.MaxValue;
	}
}
