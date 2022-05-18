using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200140A RID: 5130
	[CommandInfo("YSNew/Get", "GetTianFu", "获取是否选择该天赋", 0)]
	[AddComponentMenu("")]
	public class GetTianFu : Command
	{
		// Token: 0x06007C80 RID: 31872 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C81 RID: 31873 RVA: 0x002C5114 File Offset: 0x002C3314
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempBool.Value = (player.SelectTianFuID.list.Find((JSONObject aa) => (int)aa.n == this.TianFuID) != null);
			this.Continue();
		}

		// Token: 0x06007C82 RID: 31874 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C83 RID: 31875 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A85 RID: 27269
		[Tooltip("天赋的ID")]
		[SerializeField]
		protected int TianFuID;

		// Token: 0x04006A86 RID: 27270
		[Tooltip("返回是否拥有的值")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
