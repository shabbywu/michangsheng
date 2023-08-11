using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddItemByVar", "增加物品", 0)]
[AddComponentMenu("")]
public class AddItemByVar : Command
{
	[Tooltip("增加物品的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable ItemID;

	[Tooltip("增加物品的数量")]
	[SerializeField]
	protected int ItemNum;

	[Tooltip("增加物品的数量(如果为Null则使用数字)")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable ItemNumVar;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		int num = ItemNum;
		if ((Object)(object)ItemNumVar != (Object)null)
		{
			num = ItemNumVar.Value;
		}
		if (num > 0)
		{
			player.addItem(ItemID.Value, num, Tools.CreateItemSeid(ItemID.Value), ShowText: true);
		}
		else
		{
			player.removeItem(ItemID.Value, Mathf.Abs(num));
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
