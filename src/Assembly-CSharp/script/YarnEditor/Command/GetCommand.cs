using System;
using script.YarnEditor.Manager;
using Yarn.Unity;

namespace script.YarnEditor.Command
{
	// Token: 0x020009CD RID: 2509
	public class GetCommand
	{
		// Token: 0x060045C6 RID: 17862 RVA: 0x001D9114 File Offset: 0x001D7314
		[YarnFunction("GetValue")]
		public static string GetValue(string key)
		{
			return StoryManager.Inst.GetGoalValue(key);
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x001D9121 File Offset: 0x001D7321
		[YarnFunction("GetRandomNpcId")]
		public static int GetRandomNpcId(int level, int liuPai, int sex)
		{
			return FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(liuPai, level, sex);
		}
	}
}
