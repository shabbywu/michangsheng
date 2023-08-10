using UnityEngine;

namespace Fungus;

[CommandInfo("Rigidbody2D", "StopMotion2D", "Stop velocity and angular velocity on a Rigidbody2D", 0)]
[AddComponentMenu("")]
public class StopMotionRigidBody2D : Command
{
	public enum Motion
	{
		Velocity,
		AngularVelocity,
		AngularAndLinearVelocity
	}

	[SerializeField]
	protected Rigidbody2DData rb;

	[SerializeField]
	protected Motion motionToStop = Motion.AngularAndLinearVelocity;

	public override void OnEnter()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		switch (motionToStop)
		{
		case Motion.Velocity:
			rb.Value.velocity = Vector2.zero;
			break;
		case Motion.AngularVelocity:
			rb.Value.angularVelocity = 0f;
			break;
		case Motion.AngularAndLinearVelocity:
			rb.Value.angularVelocity = 0f;
			rb.Value.velocity = Vector2.zero;
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		return motionToStop.ToString();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)rb.rigidbody2DRef == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}
}
