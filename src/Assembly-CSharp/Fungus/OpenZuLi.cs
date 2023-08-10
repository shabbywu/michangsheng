using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "OpenZuLi", "打开租赁界面", 0)]
[AddComponentMenu("")]
public class OpenZuLi : Command
{
	[Tooltip("租赁每个月的时间所消耗的灵石")]
	[SerializeField]
	protected int Price = 1;

	[Tooltip("关联的房间名称")]
	[SerializeField]
	protected string ScreenName = "";

	public override void OnEnter()
	{
		if ((int)Tools.instance.getPlayer().money < Price)
		{
			UIPopTip.Inst.Pop("灵石不足");
		}
		else
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("KFTimeSelect"));
			KeFangSelectTime.inst.price = Price;
			KeFangSelectTime.inst.screenName = ScreenName;
			KeFangSelectTime.inst.Init();
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
