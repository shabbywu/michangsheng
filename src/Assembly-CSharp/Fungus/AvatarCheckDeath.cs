using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013CA RID: 5066
	[CommandInfo("YS", "CheckAvatarDeath", "检测英雄的死亡，将死亡状态赋值到你设置的IsDeath中设置的变量当中", 0)]
	[AddComponentMenu("")]
	public class AvatarCheckDeath : Command
	{
		// Token: 0x06007B87 RID: 31623 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007B88 RID: 31624 RVA: 0x000542CB File Offset: 0x000524CB
		public override void OnEnter()
		{
			this.TempBool.Value = jsonData.instance.MonstarIsDeath(this.MonstarID.Value);
			this.Continue();
		}

		// Token: 0x06007B89 RID: 31625 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B8A RID: 31626 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A0A RID: 27146
		[Tooltip("需要检测是否死亡的英雄ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MonstarID;

		// Token: 0x04006A0B RID: 27147
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
