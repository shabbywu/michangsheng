using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F1F RID: 3871
	[CommandInfo("YS", "CheckStaticSkill", "检测是否拥有该功法", 0)]
	[AddComponentMenu("")]
	public class CheckStaticSkill : Command
	{
		// Token: 0x06006DC0 RID: 28096 RVA: 0x002A3D20 File Offset: 0x002A1F20
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

		// Token: 0x06006DC1 RID: 28097 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DC2 RID: 28098 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B50 RID: 23376
		[Tooltip("需要进行检测的功法ID")]
		[SerializeField]
		protected int SkillID;

		// Token: 0x04005B51 RID: 23377
		[Tooltip("获取到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempValue;
	}
}
