using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddTime", "增加时间", 0)]
[AddComponentMenu("")]
public class AddTime : Command
{
	[Tooltip("年")]
	[SerializeField]
	public int Year;

	[Tooltip("月")]
	[SerializeField]
	public int Month;

	[Tooltip("日")]
	[SerializeField]
	public int Day;

	public bool IsNoJieSuan;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		NpcJieSuanManager.inst.IsNoJieSuan = IsNoJieSuan;
		player.AddTime(Day, Month, Year);
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
