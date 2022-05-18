using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013D3 RID: 5075
	[CommandInfo("YS", "CheckStaticSkill", "检测是否拥有该功法", 0)]
	[AddComponentMenu("")]
	public class CheckStaticSkill : Command
	{
		// Token: 0x06007BAB RID: 31659 RVA: 0x002C4054 File Offset: 0x002C2254
		public override void OnEnter()
		{
			if (Tools.instance.getPlayer().hasStaticSkillList.Find((SkillItem aa) => aa.itemId == this.SkillID) == null)
			{
				this.TempValue.Value = false;
			}
			else
			{
				this.TempValue.Value = true;
			}
			this.Continue();
		}

		// Token: 0x06007BAC RID: 31660 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BAD RID: 31661 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A1A RID: 27162
		[Tooltip("需要进行检测的功法ID")]
		[SerializeField]
		protected int SkillID;

		// Token: 0x04006A1B RID: 27163
		[Tooltip("获取到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempValue;
	}
}
