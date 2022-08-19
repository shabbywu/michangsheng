using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F57 RID: 3927
	[CommandInfo("YS", "检测该功法等级是否达到目标等级", "检测该功法等级是否达到目标等级", 0)]
	[AddComponentMenu("")]
	public class CheckStaticSkillLevel : Command
	{
		// Token: 0x06006EA0 RID: 28320 RVA: 0x002A5308 File Offset: 0x002A3508
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

		// Token: 0x06006EA1 RID: 28321 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BB8 RID: 23480
		[Tooltip("需要进行检测的技能ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable SkillID;

		// Token: 0x04005BB9 RID: 23481
		[Tooltip("目标等级")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable Level;

		// Token: 0x04005BBA RID: 23482
		[Tooltip("获取到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempValue;
	}
}
