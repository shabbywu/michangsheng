using System;
using UnityEngine;

// Token: 0x02000166 RID: 358
[RequireComponent(typeof(CharacterSystem))]
public class PlayerCharacterController : MonoBehaviour
{
	// Token: 0x06000C6D RID: 3181 RVA: 0x0000E5DC File Offset: 0x0000C7DC
	private void Start()
	{
		this.character = base.gameObject.GetComponent<CharacterSystem>();
		Screen.lockCursor = true;
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x000978A8 File Offset: 0x00095AA8
	private void Update()
	{
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Quaternion.AngleAxis(-90f, Vector3.up) * Camera.main.transform.right;
		if (Screen.lockCursor)
		{
			if (Input.GetKey(119))
			{
				vector += vector2;
			}
			if (Input.GetKey(115))
			{
				vector -= vector2;
			}
			if (Input.GetKey(97))
			{
				vector -= Camera.main.transform.right;
			}
			if (Input.GetKey(100))
			{
				vector += Camera.main.transform.right;
			}
			if (Input.GetMouseButtonDown(0))
			{
				this.character.Attack();
			}
			Orbit component = Camera.main.gameObject.GetComponent<Orbit>();
			if (component != null)
			{
				Orbit orbit = component;
				orbit.Data.Azimuth = orbit.Data.Azimuth + Input.GetAxis("Mouse X") / 100f;
				Orbit orbit2 = component;
				orbit2.Data.Zenith = orbit2.Data.Zenith + Input.GetAxis("Mouse Y") / 100f;
				component.Data.Zenith = Mathf.Clamp(component.Data.Zenith, -0.8f, 0f);
				Orbit orbit3 = component;
				orbit3.Data.Length = orbit3.Data.Length + (-6f - component.Data.Length) / 10f;
			}
			if (Input.GetMouseButtonDown(1))
			{
				this.character.Attack();
				CharacterSkillDeployer component2 = base.gameObject.GetComponent<CharacterSkillDeployer>();
				if (component2 != null)
				{
					component2.DeployWithAttacking();
				}
			}
		}
		vector.Normalize();
		this.character.Move(vector);
	}

	// Token: 0x04000992 RID: 2450
	private CharacterSystem character;
}
