using Fungus;
using KBEngine;
using UnityEngine;

[CommandInfo("YSSea", "关闭当前海域机制", "关闭当前海域机制", 0)]
[AddComponentMenu("")]
public class CmdCloseSeaJiZhi : Command
{
	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		int result = 0;
		if (int.TryParse(Tools.getScreenName().Replace("Sea", ""), out result))
		{
			player.EndlessSeaBoss[result.ToString()].SetField("Close", val: true);
			MapSeaCompent[] array = Object.FindObjectsOfType<MapSeaCompent>();
			foreach (MapSeaCompent obj in array)
			{
				obj.WhetherHasJiZhi = obj.HasBoss();
				obj.Refresh();
			}
		}
		Continue();
	}
}
