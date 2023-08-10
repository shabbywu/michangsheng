using UnityEngine;

[RequireComponent(typeof(CharacterSystem))]
public class PlayerCharacterController : MonoBehaviour
{
	private CharacterSystem character;

	private void Start()
	{
		character = ((Component)this).gameObject.GetComponent<CharacterSystem>();
		Screen.lockCursor = true;
	}

	private void Update()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = Vector3.zero;
		Vector3 val2 = Quaternion.AngleAxis(-90f, Vector3.up) * ((Component)Camera.main).transform.right;
		if (Screen.lockCursor)
		{
			if (Input.GetKey((KeyCode)119))
			{
				val += val2;
			}
			if (Input.GetKey((KeyCode)115))
			{
				val -= val2;
			}
			if (Input.GetKey((KeyCode)97))
			{
				val -= ((Component)Camera.main).transform.right;
			}
			if (Input.GetKey((KeyCode)100))
			{
				val += ((Component)Camera.main).transform.right;
			}
			if (Input.GetMouseButtonDown(0))
			{
				character.Attack();
			}
			Orbit component = ((Component)Camera.main).gameObject.GetComponent<Orbit>();
			if ((Object)(object)component != (Object)null)
			{
				component.Data.Azimuth += Input.GetAxis("Mouse X") / 100f;
				component.Data.Zenith += Input.GetAxis("Mouse Y") / 100f;
				component.Data.Zenith = Mathf.Clamp(component.Data.Zenith, -0.8f, 0f);
				component.Data.Length += (-6f - component.Data.Length) / 10f;
			}
			if (Input.GetMouseButtonDown(1))
			{
				character.Attack();
				CharacterSkillDeployer component2 = ((Component)this).gameObject.GetComponent<CharacterSkillDeployer>();
				if ((Object)(object)component2 != (Object)null)
				{
					component2.DeployWithAttacking();
				}
			}
		}
		((Vector3)(ref val)).Normalize();
		character.Move(val);
	}
}
