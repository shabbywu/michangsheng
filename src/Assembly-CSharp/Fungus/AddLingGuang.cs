using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F2F RID: 3887
	[CommandInfo("YSNew/Add", "AddLingGuang", "增加灵光", 0)]
	[AddComponentMenu("")]
	public class AddLingGuang : Command
	{
		// Token: 0x06006E02 RID: 28162 RVA: 0x002A422A File Offset: 0x002A242A
		public override void OnEnter()
		{
			Tools.instance.getPlayer().wuDaoMag.AddLingGuangByJsonID(this.LingGuangID.Value);
			UIPopTip.Inst.Pop("获得新的感悟", PopTipIconType.感悟);
			this.Continue();
		}

		// Token: 0x06006E03 RID: 28163 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B73 RID: 23411
		[Tooltip("增加的灵光ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable LingGuangID;
	}
}
