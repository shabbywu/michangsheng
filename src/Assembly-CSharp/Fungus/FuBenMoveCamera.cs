using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSFuBen", "FuBenMoveCamera", "移动摄像机位置", 0)]
[AddComponentMenu("")]
public class FuBenMoveCamera : Command
{
	[Tooltip("场景名称")]
	[SerializeField]
	protected string ScenceName;

	[Tooltip("摄像机移动地点的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable MapID;

	[VariableProperty(new Type[] { typeof(FloatVariable) })]
	[Tooltip("摄像机移动速度")]
	[SerializeField]
	protected FloatVariable speed;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		Tools.instance.getPlayer();
		if ((Object)(object)AllMapManage.instance != (Object)null && AllMapManage.instance.mapIndex.ContainsKey(MapID.Value))
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			wait();
			Vector2 val = Vector2.op_Implicit(((Component)AllMapManage.instance.mapIndex[MapID.Value]).transform.position);
			((Tween)TweenSettingsExtensions.SetSpeedBased<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove(((Component)component).transform, Vector2.op_Implicit(val), 30f, false))).onComplete = (TweenCallback)delegate
			{
				removeWait();
			};
		}
		Continue();
	}

	public void wait()
	{
		CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
		if ((Object)(object)component != (Object)null)
		{
			component.follwPlayer = true;
		}
	}

	public void removeWait()
	{
		CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
		if ((Object)(object)component != (Object)null)
		{
			component.follwPlayer = false;
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
