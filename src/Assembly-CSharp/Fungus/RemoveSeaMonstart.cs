using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSFuBen", "RemoveSeaMonstart", "移除无尽之海NPC", 0)]
[AddComponentMenu("")]
public class RemoveSeaMonstart : Command
{
	[Tooltip("说明")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable UUID;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (SeaAvatarObjBase monstar in EndlessSeaMag.Inst.MonstarList)
		{
			if (monstar.UUID == UUID.Value)
			{
				Object.Destroy((Object)(object)((Component)monstar).gameObject);
				EndlessSeaMag.Inst.MonstarList.Remove(monstar);
				break;
			}
		}
		player.seaNodeMag.RemoveSeaMonstar(UUID.Value);
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
