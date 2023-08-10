using UnityEngine;

namespace UltimateSurvival;

public class EntityEventHandler : MonoBehaviour
{
	public Value<float> Health = new Value<float>(100f);

	public Attempt<HealthEventData> ChangeHealth = new Attempt<HealthEventData>();

	public Value<bool> IsGrounded = new Value<bool>(initialValue: true);

	public Value<Vector3> Velocity = new Value<Vector3>(Vector3.zero);

	public Message<float> Land = new Message<float>();

	public Message Death = new Message();

	public Message Respawn = new Message();
}
