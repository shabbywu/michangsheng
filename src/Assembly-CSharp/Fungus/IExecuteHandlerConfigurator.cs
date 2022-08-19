using System;

namespace Fungus
{
	// Token: 0x02000F01 RID: 3841
	public interface IExecuteHandlerConfigurator
	{
		// Token: 0x170008E0 RID: 2272
		// (set) Token: 0x06006C26 RID: 27686
		int UpdateExecuteStartOnFrame { set; }

		// Token: 0x170008E1 RID: 2273
		// (set) Token: 0x06006C27 RID: 27687
		int UpdateExecuteRepeatFrequency { set; }

		// Token: 0x170008E2 RID: 2274
		// (set) Token: 0x06006C28 RID: 27688
		bool UpdateExecuteRepeat { set; }

		// Token: 0x170008E3 RID: 2275
		// (set) Token: 0x06006C29 RID: 27689
		float TimeExecuteStartAfter { set; }

		// Token: 0x170008E4 RID: 2276
		// (set) Token: 0x06006C2A RID: 27690
		float TimeExecuteRepeatFrequency { set; }

		// Token: 0x170008E5 RID: 2277
		// (set) Token: 0x06006C2B RID: 27691
		bool TimeExecuteRepeat { set; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06006C2C RID: 27692
		ExecuteHandler Component { get; }
	}
}
