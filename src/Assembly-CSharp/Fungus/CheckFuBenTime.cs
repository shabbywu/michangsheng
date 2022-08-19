using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F19 RID: 3865
	[CommandInfo("YS", "CheckFuBenTime", "检测副本时间", 0)]
	[AddComponentMenu("")]
	public class CheckFuBenTime : Command
	{
		// Token: 0x06006DA9 RID: 28073 RVA: 0x002A3B50 File Offset: 0x002A1D50
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

		// Token: 0x06006DAA RID: 28074 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DAB RID: 28075 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B46 RID: 23366
		[Tooltip("比较类型，大于 小于 等于")]
		[SerializeField]
		protected ItemCheck.CompareNum CompareType;

		// Token: 0x04005B47 RID: 23367
		[Tooltip("剩余时间：单位 /天")]
		[SerializeField]
		protected int Time;

		// Token: 0x04005B48 RID: 23368
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
