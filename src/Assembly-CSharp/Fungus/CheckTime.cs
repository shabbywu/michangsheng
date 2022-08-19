using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F20 RID: 3872
	[CommandInfo("YS", "CheckTime", "检测当前时间", 0)]
	[AddComponentMenu("")]
	public class CheckTime : Command
	{
		// Token: 0x06006DC5 RID: 28101 RVA: 0x002A3D84 File Offset: 0x002A1F84
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			bool value = false;
			DateTime dateTime = DateTime.Parse(this.Time);
			DateTime nowTime = player.worldTimeMag.getNowTime();
			if (this.CompareType == ItemCheck.CompareNum.GreaterThan)
			{
				if (dateTime > nowTime)
				{
					value = true;
				}
			}
			else if (this.CompareType == ItemCheck.CompareNum.LessThan)
			{
				if (dateTime < nowTime)
				{
					value = true;
				}
			}
			else if (this.CompareType == ItemCheck.CompareNum.equalTo && dateTime == nowTime)
			{
				value = true;
			}
			this.TempBool.Value = value;
			this.Continue();
		}

		// Token: 0x06006DC6 RID: 28102 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DC7 RID: 28103 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B52 RID: 23378
		[Tooltip("比较类型，大于 小于 等于")]
		[SerializeField]
		protected ItemCheck.CompareNum CompareType;

		// Token: 0x04005B53 RID: 23379
		[Tooltip("获取到的修为值存放位置")]
		[SerializeField]
		protected string Time;

		// Token: 0x04005B54 RID: 23380
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
