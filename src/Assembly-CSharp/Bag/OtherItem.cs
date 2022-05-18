using System;
using GUIPackage;
using JSONClass;

namespace Bag
{
	// Token: 0x02000D37 RID: 3383
	[Serializable]
	public class OtherItem : BaseItem
	{
		// Token: 0x06005059 RID: 20569 RVA: 0x00039E2D File Offset: 0x0003802D
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
