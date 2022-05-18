using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013CD RID: 5069
	[CommandInfo("YS", "CheckFuBenTime", "检测副本时间", 0)]
	[AddComponentMenu("")]
	public class CheckFuBenTime : Command
	{
		// Token: 0x06007B94 RID: 31636 RVA: 0x002C3EB8 File Offset: 0x002C20B8
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			bool value = false;
			string screenName = Tools.getScreenName();
			int num = 0;
			if (jsonData.instance.FuBenInfoJsonData.HasField(screenName))
			{
				num = player.fubenContorl[screenName].ResidueTimeDay;
			}
			int time = this.Time;
			if (this.CompareType == ItemCheck.CompareNum.GreaterThan)
			{
				if (num > time)
				{
					value = true;
				}
			}
			else if (this.CompareType == ItemCheck.CompareNum.LessThan)
			{
				if (num < time)
				{
					value = true;
				}
			}
			else if (this.CompareType == ItemCheck.CompareNum.equalTo && num == time)
			{
				value = true;
			}
			this.TempBool.Value = value;
			this.Continue();
		}

		// Token: 0x06007B95 RID: 31637 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B96 RID: 31638 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A10 RID: 27152
		[Tooltip("比较类型，大于 小于 等于")]
		[SerializeField]
		protected ItemCheck.CompareNum CompareType;

		// Token: 0x04006A11 RID: 27153
		[Tooltip("剩余时间：单位 /天")]
		[SerializeField]
		protected int Time;

		// Token: 0x04006A12 RID: 27154
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
