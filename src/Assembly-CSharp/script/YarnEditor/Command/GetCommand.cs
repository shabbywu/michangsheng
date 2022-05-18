using System;
using script.YarnEditor.Manager;
using Yarn.Unity;

namespace script.YarnEditor.Command
{
	// Token: 0x02000AB6 RID: 2742
	public class GetCommand
	{
		// Token: 0x06004625 RID: 17957 RVA: 0x000322D9 File Offset: 0x000304D9
		[YarnFunction("GetValue")]
		public static string GetValue(string key)
		{
			return StoryManager.Inst.GetGoalValue(key);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x000322E6 File Offset: 0x000304E6
		[YarnFunction("GetRandomNpcId")]
		public static int GetRandomNpcId(int level, int liuPai, int sex)
		{
			return FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(liuPai, level, sex);
		}
	}
}
