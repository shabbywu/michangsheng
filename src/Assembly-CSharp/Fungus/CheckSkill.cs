using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013D2 RID: 5074
	[CommandInfo("YS", "CheckSkill", "检测是否拥有该技能", 0)]
	[AddComponentMenu("")]
	public class CheckSkill : Command
	{
		// Token: 0x06007BA8 RID: 31656 RVA: 0x00054338 File Offset: 0x00052538
		public override void OnEnter()
		{
			this.TempValue.Value = PlayerEx.HasSkill(this.SkillID);
			this.Continue();
		}

		// Token: 0x06007BA9 RID: 31657 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A18 RID: 27160
		[Tooltip("需要进行检测的技能ID")]
		[SerializeField]
		protected int SkillID;

		// Token: 0x04006A19 RID: 27161
		[Tooltip("获取到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempValue;
	}
}
