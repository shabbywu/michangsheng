using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200142F RID: 5167
	[CommandInfo("YSTools", "AvatarDeath", "设置英雄死亡", 0)]
	[AddComponentMenu("")]
	public class AvatarDeath : Command
	{
		// Token: 0x06007D05 RID: 32005 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007D06 RID: 32006 RVA: 0x000549AA File Offset: 0x00052BAA
		public override void OnEnter()
		{
			jsonData.instance.setMonstarDeath(this.MonstarID.Value, true);
			this.Continue();
		}

		// Token: 0x06007D07 RID: 32007 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D08 RID: 32008 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006ABE RID: 27326
		[Tooltip("死亡的英雄ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MonstarID;
	}
}
