using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "AvatarTransfer", "角色传送", 0)]
[AddComponentMenu("")]
public class AvatarTransfer : Command
{
	[Tooltip("传送到的大地图ID")]
	[SerializeField]
	protected int MapID;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		Do(MapID);
		Continue();
	}

	public static void Do(int mapID)
	{
		Avatar player = Tools.instance.getPlayer();
		if ((Object)(object)AllMapManage.instance != (Object)null && AllMapManage.instance.mapIndex.ContainsKey(mapID))
		{
			if (AllMapManage.instance.mapIndex[mapID] is MapComponent)
			{
				(AllMapManage.instance.mapIndex[mapID] as MapComponent).AvatarMoveToThis();
			}
			else
			{
				AllMapManage.instance.mapIndex[mapID].AvatarMoveToThis();
			}
		}
		player.NowMapIndex = mapID;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
