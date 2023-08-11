using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddItem", "增加物品", 0)]
[AddComponentMenu("")]
public class AddItem : Command
{
	[Tooltip("增加物品的ID")]
	[SerializeField]
	public int ItemID;

	[Tooltip("增加物品的数量")]
	[SerializeField]
	public int ItemNum;

	[Tooltip("是否显示增加物品弹框")]
	[SerializeField]
	public bool showText = true;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		if (ItemNum > 0)
		{
			player.addItem(ItemID, ItemNum, Tools.CreateItemSeid(ItemID), showText);
		}
		else
		{
			player.removeItem(ItemID, ItemNum);
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
	}
}
