using System;
using script.YarnEditor.Manager;
using Yarn.Unity;

namespace script.YarnEditor.Command
{
	// Token: 0x02000AB7 RID: 2743
	public class SetCommand
	{
		// Token: 0x06004628 RID: 17960 RVA: 0x000322FA File Offset: 0x000304FA
		[YarnCommand("SetTempValue")]
		public static void SetTempValue(string variableName, string value)
		{
			StoryManager.Inst.TempValueSystem.SetValue(variableName, value);
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x0003230D File Offset: 0x0003050D
		[YarnCommand("SetValue")]
		public static void SetValue(string variableName, string value)
		{
			StoryManager.Inst.SetGoalValue(variableName, value);
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x0003231B File Offset: 0x0003051B
		[YarnCommand("SetNextYarn")]
		public static void SetNextYarn(string talkName)
		{
			StoryManager.Inst.NextYarn = talkName;
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x00032328 File Offset: 0x00030528
		[YarnCommand("SetPlayerDie")]
		public static void SetPlayerDie()
		{
			UIDeath.Inst.Show(DeathType.身死道消);
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x00032335 File Offset: 0x00030535
		[YarnCommand("SetPlayerLingGen")]
		public static void SetPlayerLingGen(int id, int value)
		{
			Tools.instance.getPlayer().SetLingGen(id, value);
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x00032348 File Offset: 0x00030548
		[YarnCommand("SetPlayerMenPai")]
		public static void SetPlayerMenPai(int id)
		{
			Tools.instance.getPlayer().SetMenPai(id);
		}
	}
}
