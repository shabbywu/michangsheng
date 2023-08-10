using UnityEngine;

namespace Fungus;

[EventHandlerInfo("MonoBehaviour", "Collision", "The block will execute when a 3d physics collision matching some basic conditions is met.")]
[AddComponentMenu("")]
public class Collision : BasePhysicsEventHandler
{
	private void OnCollisionEnter(Collision collision)
	{
		ProcessCollider(PhysicsMessageType.Enter, ((Component)collision.collider).tag);
	}

	private void OnCollisionStay(Collision collision)
	{
		ProcessCollider(PhysicsMessageType.Stay, ((Component)collision.collider).tag);
	}

	private void OnCollisionExit(Collision collision)
	{
		ProcessCollider(PhysicsMessageType.Exit, ((Component)collision.collider).tag);
	}
}
