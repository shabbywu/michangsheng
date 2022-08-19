using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F16 RID: 3862
	[CommandInfo("YS", "CheckAvatarDeath", "检测英雄的死亡，将死亡状态赋值到你设置的IsDeath中设置的变量当中", 0)]
	[AddComponentMenu("")]
	public class AvatarCheckDeath : Command
	{
		// Token: 0x06006D9C RID: 28060 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006D9D RID: 28061 RVA: 0x002A3AC3 File Offset: 0x002A1CC3
		public override void OnEnter()
		{
			this.TempBool.Value = jsonData.instance.MonstarIsDeath(this.MonstarID.Value);
			this.Continue();
		}

		// Token: 0x06006D9E RID: 28062 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006D9F RID: 28063 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B41 RID: 23361
		[Tooltip("需要检测是否死亡的英雄ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MonstarID;

		// Token: 0x04005B42 RID: 23362
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
