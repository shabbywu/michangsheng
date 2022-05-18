using System;

namespace Fungus
{
	// Token: 0x020013A6 RID: 5030
	public interface IExecuteHandlerConfigurator
	{
		// Token: 0x17000B83 RID: 2947
		// (set) Token: 0x060079DA RID: 31194
		int UpdateExecuteStartOnFrame { set; }

		// Token: 0x17000B84 RID: 2948
		// (set) Token: 0x060079DB RID: 31195
		int UpdateExecuteRepeatFrequency { set; }

		// Token: 0x17000B85 RID: 2949
		// (set) Token: 0x060079DC RID: 31196
		bool UpdateExecuteRepeat { set; }

		// Token: 0x17000B86 RID: 2950
		// (set) Token: 0x060079DD RID: 31197
		float TimeExecuteStartAfter { set; }

		// Token: 0x17000B87 RID: 2951
		// (set) Token: 0x060079DE RID: 31198
		float TimeExecuteRepeatFrequency { set; }

		// Token: 0x17000B88 RID: 2952
		// (set) Token: 0x060079DF RID: 31199
		bool TimeExecuteRepeat { set; }

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060079E0 RID: 31200
		ExecuteHandler Component { get; }
	}
}
