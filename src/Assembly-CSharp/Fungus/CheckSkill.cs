using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F1E RID: 3870
	[CommandInfo("YS", "CheckSkill", "检测是否拥有该技能", 0)]
	[AddComponentMenu("")]
	public class CheckSkill : Command
	{
		// Token: 0x06006DBD RID: 28093 RVA: 0x002A3CFF File Offset: 0x002A1EFF
		public override void OnEnter()
		{
			this.TempValue.Value = PlayerEx.HasSkill(this.SkillID);
			this.Continue();
		}

		// Token: 0x06006DBE RID: 28094 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B4E RID: 23374
		[Tooltip("需要进行检测的技能ID")]
		[SerializeField]
		protected int SkillID;

		// Token: 0x04005B4F RID: 23375
		[Tooltip("获取到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempValue;
	}
}
