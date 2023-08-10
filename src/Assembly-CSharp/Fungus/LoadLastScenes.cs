using System;
using System.Threading;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "LoadLastScenes", "加载上一个场景", 0)]
[AddComponentMenu("")]
public class LoadLastScenes : Command
{
	[Tooltip("说明")]
	[SerializeField]
	protected string desc = "加载上一个场景";

	public void methodName()
	{
		try
		{
			Tools.instance.getPlayer().ResetAllEndlessNode();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public override void OnEnter()
	{
		new Thread(methodName).Start();
		if (Tools.instance.getPlayer().lastScence.Equals("LoadingScreen") || Tools.instance.getPlayer().lastScence.Equals("") || Tools.instance.getPlayer().lastScence.Equals("MainMenu"))
		{
			Tools.instance.getPlayer().lastScence = "AllMaps";
		}
		Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastScence);
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
