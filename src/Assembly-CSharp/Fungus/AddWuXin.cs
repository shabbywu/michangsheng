using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddWuXin", "增加悟性", 0)]
[AddComponentMenu("")]
public class AddWuXin : Command
{
	[Tooltip("增加悟性的数量")]
	[SerializeField]
	protected int AddWuXinNum;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().addWuXin(AddWuXinNum);
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
