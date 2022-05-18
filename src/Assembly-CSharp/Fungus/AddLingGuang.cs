using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013E5 RID: 5093
	[CommandInfo("YSNew/Add", "AddLingGuang", "增加灵光", 0)]
	[AddComponentMenu("")]
	public class AddLingGuang : Command
	{
		// Token: 0x06007BED RID: 31725 RVA: 0x00054504 File Offset: 0x00052704
		public override void OnEnter()
		{
			Tools.instance.getPlayer().wuDaoMag.AddLingGuangByJsonID(this.LingGuangID.Value);
			UIPopTip.Inst.Pop("获得新的感悟", PopTipIconType.感悟);
			this.Continue();
		}

		// Token: 0x06007BEE RID: 31726 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A45 RID: 27205
		[Tooltip("增加的灵光ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable LingGuangID;
	}
}
