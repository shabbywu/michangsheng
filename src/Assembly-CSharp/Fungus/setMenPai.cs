using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "setMenPai", "设置门派id", 0)]
[AddComponentMenu("")]
public class setMenPai : Command
{
	[Tooltip("设置门派的ID")]
	[SerializeField]
	protected int MenPaiID;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		player.joinMenPai(MenPaiID);
		player.taskMag.SetChuanWenBlack(5);
		player.taskMag.SetChuanWenBlack(6);
		player.taskMag.SetChuanWenBlack(7);
		player.taskMag.SetChuanWenBlack(29);
		player.taskMag.SetChuanWenBlack(30);
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
