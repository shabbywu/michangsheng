using System;
using GUIPackage;
using JSONClass;

namespace Bag
{
	// Token: 0x020009AF RID: 2479
	[Serializable]
	public class OtherItem : BaseItem
	{
		// Token: 0x060044F6 RID: 17654 RVA: 0x001D5211 File Offset: 0x001D3411
		public override void Use()
		{
			if (_ItemJsonData.DataDict[this.Id].vagueType == 1)
			{
				new item(this.Id).gongneng(delegate
				{
					Tools.instance.getPlayer().removeItem(this.Id, 1);
					MessageMag.Instance.Send(MessageName.MSG_PLAYER_USE_ITEM, null);
				}, false);
			}
		}
	}
}
