using System;
using GUIPackage;
using JSONClass;
using UnityEngine.Events;

namespace Bag;

[Serializable]
public class OtherItem : BaseItem
{
	public override void Use()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		if (_ItemJsonData.DataDict[Id].vagueType == 1)
		{
			new item(Id).gongneng((UnityAction)delegate
			{
				Tools.instance.getPlayer().removeItem(Id, 1);
				MessageMag.Instance.Send(MessageName.MSG_PLAYER_USE_ITEM);
			});
		}
	}
}
