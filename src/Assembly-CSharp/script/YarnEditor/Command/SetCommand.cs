using System;
using script.YarnEditor.Manager;
using Yarn.Unity;

namespace script.YarnEditor.Command
{
	// Token: 0x020009CE RID: 2510
	public class SetCommand
	{
		// Token: 0x060045C9 RID: 17865 RVA: 0x001D9135 File Offset: 0x001D7335
		[YarnCommand("SetTempValue")]
		public static void SetTempValue(string variableName, string value)
		{
			StoryManager.Inst.TempValueSystem.SetValue(variableName, value);
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x001D9148 File Offset: 0x001D7348
		[YarnCommand("SetValue")]
		public static void SetValue(string variableName, string value)
		{
			StoryManager.Inst.SetGoalValue(variableName, value);
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x001D9156 File Offset: 0x001D7356
		[YarnCommand("SetNextYarn")]
		public static void SetNextYarn(string talkName)
		{
			StoryManager.Inst.NextYarn = talkName;
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x001D9163 File Offset: 0x001D7363
		[YarnCommand("SetPlayerDie")]
		public static void SetPlayerDie()
		{
			UIDeath.Inst.Show(DeathType.身死道消);
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x001D9170 File Offset: 0x001D7370
		[YarnCommand("SetPlayerLingGen")]
		public static void SetPlayerLingGen(int id, int value)
		{
			Tools.instance.getPlayer().SetLingGen(id, value);
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x001D9183 File Offset: 0x001D7383
		[YarnCommand("SetPlayerMenPai")]
		public static void SetPlayerMenPai(int id)
		{
			Tools.instance.getPlayer().SetMenPai(id);
		}
	}
}
