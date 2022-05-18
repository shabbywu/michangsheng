using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200140B RID: 5131
	[CommandInfo("YSNew/Get", "GetWuDaoLv", "获取悟道等级保存到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetWuDaoLv : Command
	{
		// Token: 0x06007C86 RID: 31878 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C87 RID: 31879 RVA: 0x002C515C File Offset: 0x002C335C
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

		// Token: 0x06007C88 RID: 31880 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C89 RID: 31881 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A87 RID: 27271
		[Tooltip("需要获取悟道属性的类型")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable WuDaoType;

		// Token: 0x04006A88 RID: 27272
		[Tooltip("保存到TempValue中")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TempValue;
	}
}
