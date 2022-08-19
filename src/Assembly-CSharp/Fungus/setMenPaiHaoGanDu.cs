using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F5E RID: 3934
	[CommandInfo("YSNew/Set", "setMenPaiHaoGanDu", "设置门派id", 0)]
	[AddComponentMenu("")]
	public class setMenPaiHaoGanDu : Command
	{
		// Token: 0x06006EB9 RID: 28345 RVA: 0x002A5520 File Offset: 0x002A3720
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

		// Token: 0x06006EBA RID: 28346 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BC2 RID: 23490
		[Tooltip("设置的方式set表示将值设置为该值，add表示在原有的值的基础上进行加减")]
		[SerializeField]
		protected setMenPaiHaoGanDu.SetType Type;

		// Token: 0x04005BC3 RID: 23491
		[Tooltip("设置门派的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MenPaiID;

		// Token: 0x04005BC4 RID: 23492
		[Tooltip("设置的值")]
		[SerializeField]
		protected int Value;

		// Token: 0x02001727 RID: 5927
		public enum SetType
		{
			// Token: 0x04007524 RID: 29988
			set,
			// Token: 0x04007525 RID: 29989
			add
		}
	}
}
