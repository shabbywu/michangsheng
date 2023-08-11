using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNetwork;

public class IsClient : Conditional
{
	public override TaskStatus OnUpdate()
	{
		if (NetworkClient.active)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}
}
