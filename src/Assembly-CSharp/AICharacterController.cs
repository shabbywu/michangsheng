using UnityEngine;

[RequireComponent(typeof(CharacterSystem))]
public class AICharacterController : MonoBehaviour
{
	public GameObject ObjectTarget;

	public string TargetTag = "Player";

	private CharacterSystem character;

	private int aiTime;

	private int aiState;

	private void Start()
	{
		character = ((Component)this).gameObject.GetComponent<CharacterSystem>();
	}

	private void Update()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		Vector3 dir = Vector3.zero;
		if (aiTime <= 0)
		{
			aiState = Random.Range(0, 4);
			aiTime = Random.Range(10, 100);
		}
		else
		{
			aiTime--;
		}
		if (Object.op_Implicit((Object)(object)ObjectTarget))
		{
			if (Vector3.Distance(ObjectTarget.transform.position, ((Component)this).gameObject.transform.position) <= 2f)
			{
				((Component)this).transform.LookAt(ObjectTarget.transform.position);
				if (aiTime <= 0 && aiState == 1)
				{
					character.Attack();
				}
			}
			else if (aiState == 1)
			{
				((Component)this).transform.LookAt(ObjectTarget.transform.position);
				dir = ((Component)this).transform.forward;
				((Vector3)(ref dir)).Normalize();
				character.Move(dir);
			}
		}
		else
		{
			ObjectTarget = GameObject.FindGameObjectWithTag(TargetTag);
		}
	}
}
