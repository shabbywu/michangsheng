using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200142A RID: 5162
	[CommandInfo("YSTask", "IsNTaskFinish", "判断任务是否在CD中", 0)]
	[AddComponentMenu("")]
	public class IsNTaskFinish : Command
	{
		// Token: 0x06007CF1 RID: 31985 RVA: 0x00054925 File Offset: 0x00052B25
		public override void OnEnter()
		{
			this.IsStart.Value = IsNTaskFinish.Do(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06007CF2 RID: 31986 RVA: 0x002C5D40 File Offset: 0x002C3F40
		public static bool Do(int NTaskID)
		{
			Avatar player = Tools.instance.getPlayer();
			return !player.NomelTaskJson.HasField(NTaskID.ToString()) || (player.NomelTaskJson[NTaskID.ToString()].HasField("IsFirstStart") && player.NomelTaskJson[NTaskID.ToString()]["IsFirstStart"].b && player.NomelTaskJson[NTaskID.ToString()].HasField("IsEnd") && player.NomelTaskJson[NTaskID.ToString()]["IsEnd"].b);
		}

		// Token: 0x06007CF3 RID: 31987 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AB6 RID: 27318
		[Tooltip("需要判断是否开始的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04006AB7 RID: 27319
		[Tooltip("将判断后的值保存到一个变量中")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable IsStart;
	}
}
