using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F78 RID: 3960
	[CommandInfo("YSTools", "AvatarDeath", "设置英雄死亡", 0)]
	[AddComponentMenu("")]
	public class AvatarDeath : Command
	{
		// Token: 0x06006F15 RID: 28437 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006F16 RID: 28438 RVA: 0x002A645A File Offset: 0x002A465A
		public override void OnEnter()
		{
			jsonData.instance.setMonstarDeath(this.MonstarID.Value, true);
			this.Continue();
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F18 RID: 28440 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BEA RID: 23530
		[Tooltip("死亡的英雄ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MonstarID;
	}
}
