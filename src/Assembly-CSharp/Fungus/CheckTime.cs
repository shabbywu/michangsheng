using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013D4 RID: 5076
	[CommandInfo("YS", "CheckTime", "检测当前时间", 0)]
	[AddComponentMenu("")]
	public class CheckTime : Command
	{
		// Token: 0x06007BB0 RID: 31664 RVA: 0x002C40A4 File Offset: 0x002C22A4
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

		// Token: 0x06007BB1 RID: 31665 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BB2 RID: 31666 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A1C RID: 27164
		[Tooltip("比较类型，大于 小于 等于")]
		[SerializeField]
		protected ItemCheck.CompareNum CompareType;

		// Token: 0x04006A1D RID: 27165
		[Tooltip("获取到的修为值存放位置")]
		[SerializeField]
		protected string Time;

		// Token: 0x04006A1E RID: 27166
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
