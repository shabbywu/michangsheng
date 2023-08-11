using UnityEngine;

public class FrontMover : MonoBehaviour
{
	public Transform pivot;

	public ParticleSystem effect;

	public float speed = 15f;

	public float drug = 1f;

	public float repeatingTime = 1f;

	private float startSpeed;

	private void Start()
	{
		((MonoBehaviour)this).InvokeRepeating("StartAgain", 0f, repeatingTime);
		effect.Play();
		startSpeed = speed;
	}

	private void StartAgain()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		startSpeed = speed;
		((Component)this).transform.position = pivot.position;
	}

	private void Update()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		startSpeed *= drug;
		Transform transform = ((Component)this).transform;
		transform.position += ((Component)this).transform.forward * (startSpeed * Time.deltaTime);
	}
}
