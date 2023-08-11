using UnityEngine;

namespace Fungus;

[EventHandlerInfo("MonoBehaviour", "Collision2D", "The block will execute when a 2d physics collision matching some basic conditions is met.")]
[AddComponentMenu("")]
public class Collision2D : BasePhysicsEventHandler
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		ProcessCollider(PhysicsMessageType.Enter, ((Component)collision.collider).tag);
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		ProcessCollider(PhysicsMessageType.Stay, ((Component)collision.collider).tag);
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		ProcessCollider(PhysicsMessageType.Exit, ((Component)collision.collider).tag);
	}
}
