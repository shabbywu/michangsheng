using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "ItemRemove", "移除物品", 0)]
[AddComponentMenu("")]
public class ItemRemove : Command
{
	[Tooltip("需要移除的物品ID")]
	[SerializeField]
	protected int ItemID;

	[Tooltip("需要移除的物品数量")]
	[SerializeField]
	protected int ItemRemoveNum;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		if (ItemID == 1 || ItemID == 117 || ItemID == 218 || ItemID == 304)
		{
			Tools.instance.RemoveTieJian(ItemID);
		}
		else
		{
			Tools.instance.RemoveItem(ItemID, ItemRemoveNum);
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
