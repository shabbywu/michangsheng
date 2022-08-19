using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F13 RID: 3859
	[CommandInfo("YSFuBen", "LightFubenNode", "角色传送", 0)]
	[AddComponentMenu("")]
	public class LightFubenNode : Command
	{
		// Token: 0x06006D7F RID: 28031 RVA: 0x002A35C9 File Offset: 0x002A17C9
		public override void OnEnter()
		{
			Tools.instance.getPlayer().fubenContorl[this.ScenceName].addExploredNode(this.MapID.Value);
			WASDMove.Inst.IsMoved = true;
			this.Continue();
		}

		// Token: 0x06006D80 RID: 28032 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B2F RID: 23343
		[Tooltip("传送到的地点的ID")]
		[SerializeField]
		protected string ScenceName;

		// Token: 0x04005B30 RID: 23344
		[Tooltip("点亮的地点的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MapID;
	}
}
