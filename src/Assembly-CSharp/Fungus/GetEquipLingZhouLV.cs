using System;
using JSONClass;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013F3 RID: 5107
	[CommandInfo("YSNew/Get", "GetEquipLingZhouLV", "获取装备的灵舟等级", 0)]
	[AddComponentMenu("")]
	public class GetEquipLingZhouLV : Command
	{
		// Token: 0x06007C22 RID: 31778 RVA: 0x002C48A0 File Offset: 0x002C2AA0
		public override void OnEnter()
		{
			_ItemJsonData equipLingZhouData = Tools.instance.getPlayer().GetEquipLingZhouData();
			if (equipLingZhouData != null)
			{
				this.LV.Value = equipLingZhouData.quality;
			}
			else
			{
				this.LV.Value = 0;
			}
			this.Continue();
		}

		// Token: 0x06007C23 RID: 31779 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A5D RID: 27229
		[Tooltip("获取到的灵舟等级")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable LV;
	}
}
