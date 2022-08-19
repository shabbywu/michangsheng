using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F55 RID: 3925
	[CommandInfo("YSNew/Get", "GetWuDaoLv", "获取悟道等级保存到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetWuDaoLv : Command
	{
		// Token: 0x06006E98 RID: 28312 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E99 RID: 28313 RVA: 0x002A525C File Offset: 0x002A345C
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			try
			{
				this.TempValue.Value = player.wuDaoMag.getWuDaoLevelByType(this.WuDaoType.Value);
			}
			catch (Exception)
			{
				Debug.LogError("尚未获得" + this.WuDaoType.Value + "该类型悟道值");
			}
			this.Continue();
		}

		// Token: 0x06006E9A RID: 28314 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E9B RID: 28315 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BB6 RID: 23478
		[Tooltip("需要获取悟道属性的类型")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable WuDaoType;

		// Token: 0x04005BB7 RID: 23479
		[Tooltip("保存到TempValue中")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TempValue;
	}
}
