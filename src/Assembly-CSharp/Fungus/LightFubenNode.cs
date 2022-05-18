using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013C6 RID: 5062
	[CommandInfo("YSFuBen", "LightFubenNode", "角色传送", 0)]
	[AddComponentMenu("")]
	public class LightFubenNode : Command
	{
		// Token: 0x06007B68 RID: 31592 RVA: 0x00054208 File Offset: 0x00052408
		public override void OnEnter()
		{
			Tools.instance.getPlayer().fubenContorl[this.ScenceName].addExploredNode(this.MapID.Value);
			WASDMove.Inst.IsMoved = true;
			this.Continue();
		}

		// Token: 0x06007B69 RID: 31593 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B6A RID: 31594 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x040069F6 RID: 27126
		[Tooltip("传送到的地点的ID")]
		[SerializeField]
		protected string ScenceName;

		// Token: 0x040069F7 RID: 27127
		[Tooltip("点亮的地点的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MapID;
	}
}
