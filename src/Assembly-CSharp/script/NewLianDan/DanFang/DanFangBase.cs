using System;
using System.Collections.Generic;

namespace script.NewLianDan.DanFang
{
	// Token: 0x020009FF RID: 2559
	public class DanFangBase
	{
		// Token: 0x04004830 RID: 18480
		public Dictionary<int, int> ZhuYao1 = new Dictionary<int, int>();

		// Token: 0x04004831 RID: 18481
		public Dictionary<int, int> ZhuYao2 = new Dictionary<int, int>();

		// Token: 0x04004832 RID: 18482
		public Dictionary<int, int> FuYao1 = new Dictionary<int, int>();

		// Token: 0x04004833 RID: 18483
		public Dictionary<int, int> FuYao2 = new Dictionary<int, int>();

		// Token: 0x04004834 RID: 18484
		public Dictionary<int, int> YaoYin = new Dictionary<int, int>();

		// Token: 0x04004835 RID: 18485
		public int ZhuYaoYaoXin1;

		// Token: 0x04004836 RID: 18486
		public int ZhuYaoYaoXin2;

		// Token: 0x04004837 RID: 18487
		public int FuYaoYaoXin1;

		// Token: 0x04004838 RID: 18488
		public int FuYaoYaoXin2;

		// Token: 0x04004839 RID: 18489
		public JSONObject Json;
	}
}
