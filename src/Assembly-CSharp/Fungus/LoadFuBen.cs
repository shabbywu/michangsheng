using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("YSTools", "LoadFuBen", "加载副本", 0)]
[AddComponentMenu("")]
public class LoadFuBen : Command
{
	[Tooltip("副本的场景名称")]
	[SerializeField]
	protected StringData _sceneName = new StringData("");

	[Tooltip("首次进入时角色被传送到的位置")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable FirstPositon;

	[HideInInspector]
	[FormerlySerializedAs("sceneName")]
	public string sceneNameOLD = "";

	public override void OnEnter()
	{
		loadfuben(_sceneName, FirstPositon.Value);
		Continue();
	}

	public static void methodName()
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

	public static void loadfuben(string fubenName, int positon)
	{
		new Thread(methodName).Start();
		Tools.instance.getPlayer().fubenContorl[fubenName].setFirstIndex(positon);
		Tools.instance.getPlayer().zulinContorl.kezhanLastScence = Tools.getScreenName();
		Tools.instance.loadMapScenes(fubenName);
		Tools.instance.getPlayer().lastFuBenScence = Tools.getScreenName();
		Tools.instance.getPlayer().NowFuBen = fubenName;
	}

	public override string GetSummary()
	{
		if (_sceneName.Value.Length == 0)
		{
			return "Error: No scene name selected";
		}
		return _sceneName.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_sceneName.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if (sceneNameOLD != "")
		{
			_sceneName.Value = sceneNameOLD;
			sceneNameOLD = "";
		}
	}
}
