using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "AddZiZhi", "增加资质", 0)]
[AddComponentMenu("")]
public class AddZiZhi : Command
{
	[Tooltip("增加资质的数量")]
	[SerializeField]
	protected int AddZiZhiNum;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().addZiZhi(AddZiZhiNum);
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
