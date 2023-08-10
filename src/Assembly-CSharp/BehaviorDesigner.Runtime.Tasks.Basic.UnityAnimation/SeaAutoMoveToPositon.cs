using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YS")]
[TaskDescription("无禁之海自动移动到目标点")]
public class SeaAutoMoveToPositon : Action
{
	private Avatar avatar;

	private SharedInt nowPositon;

	private SharedInt endPositon;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = (Avatar)((Task)this).gameObject.GetComponent<AvaterAddScript>().entity;
		nowPositon = ((Task)this).Owner.GetVariable("nowPositon") as SharedInt;
		endPositon = ((Task)this).Owner.GetVariable("endPositon") as SharedInt;
	}

	public override TaskStatus OnUpdate()
	{
		int nowMapIndex = avatar.NowMapIndex;
		_ = (MapSeaCompent)AllMapManage.instance.mapIndex[nowMapIndex];
		EndlessSeaMag.Inst.GetRoadXian(nowMapIndex, ((SharedVariable<int>)endPositon).Value);
		if (((SharedVariable<int>)nowPositon).Value != ((SharedVariable<int>)endPositon).Value)
		{
			return (TaskStatus)1;
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
