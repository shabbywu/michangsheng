using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F86 RID: 3974
	[CommandInfo("YSTools", "Talk是否结束", "Talk是否结束", 0)]
	[AddComponentMenu("")]
	public class IsEnd : Command, INoCommand
	{
		// Token: 0x06006F49 RID: 28489 RVA: 0x002A6B49 File Offset: 0x002A4D49
		public override void OnEnter()
		{
			this.result.Value = Tools.instance.getPlayer().StreamData.FungusSaveMgr.LastIsEnd;
			this.Continue();
		}

		// Token: 0x04005C01 RID: 23553
		[Tooltip("结果")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable result;
	}
}
