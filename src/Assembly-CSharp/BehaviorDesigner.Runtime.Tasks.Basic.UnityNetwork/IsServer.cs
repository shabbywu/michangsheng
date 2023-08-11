using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNetwork;

public class IsServer : Conditional
{
	public override TaskStatus OnUpdate()
	{
		if (NetworkServer.active)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}
}
