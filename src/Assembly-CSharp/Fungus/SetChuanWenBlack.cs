using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F5B RID: 3931
	[CommandInfo("YSNew/Set", "将传闻置灰", "将传闻置灰", 0)]
	[AddComponentMenu("")]
	public class SetChuanWenBlack : Command
	{
		// Token: 0x06006EAF RID: 28335 RVA: 0x002A543B File Offset: 0x002A363B
		public override void OnEnter()
		{
			Tools.instance.getPlayer().taskMag.SetChuanWenBlack(this.TaskID.Value);
			this.Continue();
		}

		// Token: 0x04005BBE RID: 23486
		[Tooltip("需要置灰的任务的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskID;
	}
}
