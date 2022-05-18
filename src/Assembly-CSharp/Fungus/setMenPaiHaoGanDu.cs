using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001414 RID: 5140
	[CommandInfo("YSNew/Set", "setMenPaiHaoGanDu", "设置门派id", 0)]
	[AddComponentMenu("")]
	public class setMenPaiHaoGanDu : Command
	{
		// Token: 0x06007CA9 RID: 31913 RVA: 0x002C53A8 File Offset: 0x002C35A8
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			if (this.Type == setMenPaiHaoGanDu.SetType.set)
			{
				player.MenPaiHaoGanDu.SetField(this.MenPaiID.ToString(), this.Value);
			}
			else if (this.Type == setMenPaiHaoGanDu.SetType.add)
			{
				int num = player.MenPaiHaoGanDu.HasField(this.MenPaiID.ToString()) ? player.MenPaiHaoGanDu[this.MenPaiID.ToString()].I : 0;
				player.MenPaiHaoGanDu.SetField(this.MenPaiID.ToString(), num + this.Value);
				if (this.Value > 0)
				{
					UIPopTip.Inst.Pop(string.Concat(new object[]
					{
						"你在",
						ShiLiHaoGanDuName.DataDict[this.MenPaiID.Value].ChinaText,
						"的声望提升了",
						this.Value
					}), PopTipIconType.上箭头);
				}
				else if (this.Value < 0)
				{
					UIPopTip.Inst.Pop(string.Concat(new object[]
					{
						"你在",
						ShiLiHaoGanDuName.DataDict[this.MenPaiID.Value].ChinaText,
						"的声望降低了",
						Mathf.Abs(this.Value)
					}), PopTipIconType.下箭头);
				}
			}
			this.Continue();
		}

		// Token: 0x06007CAA RID: 31914 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A94 RID: 27284
		[Tooltip("设置的方式set表示将值设置为该值，add表示在原有的值的基础上进行加减")]
		[SerializeField]
		protected setMenPaiHaoGanDu.SetType Type;

		// Token: 0x04006A95 RID: 27285
		[Tooltip("设置门派的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MenPaiID;

		// Token: 0x04006A96 RID: 27286
		[Tooltip("设置的值")]
		[SerializeField]
		protected int Value;

		// Token: 0x02001415 RID: 5141
		public enum SetType
		{
			// Token: 0x04006A98 RID: 27288
			set,
			// Token: 0x04006A99 RID: 27289
			add
		}
	}
}
