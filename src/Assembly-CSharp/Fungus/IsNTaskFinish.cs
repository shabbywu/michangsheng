using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F72 RID: 3954
	[CommandInfo("YSTask", "IsNTaskFinish", "判断任务是否在CD中", 0)]
	[AddComponentMenu("")]
	public class IsNTaskFinish : Command
	{
		// Token: 0x06006EFF RID: 28415 RVA: 0x002A5FFB File Offset: 0x002A41FB
		public override void OnEnter()
		{
			this.IsStart.Value = IsNTaskFinish.Do(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x002A6020 File Offset: 0x002A4220
		public static bool Do(int NTaskID)
		{
			Avatar player = Tools.instance.getPlayer();
			return !player.NomelTaskJson.HasField(NTaskID.ToString()) || (player.NomelTaskJson[NTaskID.ToString()].HasField("IsFirstStart") && player.NomelTaskJson[NTaskID.ToString()]["IsFirstStart"].b && player.NomelTaskJson[NTaskID.ToString()].HasField("IsEnd") && player.NomelTaskJson[NTaskID.ToString()]["IsEnd"].b);
		}

		// Token: 0x06006F01 RID: 28417 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BE0 RID: 23520
		[Tooltip("需要判断是否开始的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04005BE1 RID: 23521
		[Tooltip("将判断后的值保存到一个变量中")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable IsStart;
	}
}
