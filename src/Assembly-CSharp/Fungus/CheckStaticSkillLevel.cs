using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200140D RID: 5133
	[CommandInfo("YS", "检测该功法等级是否达到目标等级", "检测该功法等级是否达到目标等级", 0)]
	[AddComponentMenu("")]
	public class CheckStaticSkillLevel : Command
	{
		// Token: 0x06007C90 RID: 31888 RVA: 0x002C5208 File Offset: 0x002C3408
		public override void OnEnter()
		{
			this.TempValue.Value = false;
			foreach (SkillItem skillItem in Tools.instance.getPlayer().hasStaticSkillList)
			{
				if (skillItem.itemId == this.SkillID.Value && skillItem.level == this.Level.Value)
				{
					this.TempValue.Value = true;
					break;
				}
			}
			this.Continue();
		}

		// Token: 0x06007C91 RID: 31889 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A8A RID: 27274
		[Tooltip("需要进行检测的技能ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable SkillID;

		// Token: 0x04006A8B RID: 27275
		[Tooltip("目标等级")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable Level;

		// Token: 0x04006A8C RID: 27276
		[Tooltip("获取到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempValue;
	}
}
