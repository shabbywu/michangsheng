using System;
using JSONClass;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F3D RID: 3901
	[CommandInfo("YSNew/Get", "GetEquipLingZhouLV", "获取装备的灵舟等级", 0)]
	[AddComponentMenu("")]
	public class GetEquipLingZhouLV : Command
	{
		// Token: 0x06006E37 RID: 28215 RVA: 0x002A4870 File Offset: 0x002A2A70
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

		// Token: 0x06006E38 RID: 28216 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B8B RID: 23435
		[Tooltip("获取到的灵舟等级")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable LV;
	}
}
