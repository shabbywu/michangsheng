using UnityEngine;

namespace Fungus;

[CommandInfo("YSFuBen", "FuBenAvatarFanHui", "角色返回之前的点", 0)]
[AddComponentMenu("")]
public class FuBenAvatarFanHui : Command
{
	[Tooltip("说明")]
	[SerializeField]
	protected string Desc = "角色返回到上一次点击的点";

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		Tools.instance.getPlayer();
		if ((Object)(object)AllMapManage.instance != (Object)null && AllMapManage.instance.mapIndex.ContainsKey(Tools.instance.fubenLastIndex))
		{
			AllMapManage.instance.mapIndex[Tools.instance.fubenLastIndex].AvatarMoveToThis();
		}
		Continue();
	}

	public void removeWait()
	{
		CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
		if ((Object)(object)component != (Object)null)
		{
			component.follwPlayer = false;
		}
	}

	public void wait()
	{
		CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
		if ((Object)(object)component != (Object)null)
		{
			component.follwPlayer = true;
		}
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
