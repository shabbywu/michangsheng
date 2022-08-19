using System;
using UnityEngine;

// Token: 0x020000EE RID: 238
[RequireComponent(typeof(CharacterSystem))]
public class AICharacterController : MonoBehaviour
{
	// Token: 0x06000B7B RID: 2939 RVA: 0x00045CB3 File Offset: 0x00043EB3
	private void Start()
	{
		this.character = base.gameObject.GetComponent<CharacterSystem>();
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00045CC8 File Offset: 0x00043EC8
	private void Update()
	{
		Vector3 dir = Vector3.zero;
		if (this.aiTime <= 0)
		{
			this.aiState = Random.Range(0, 4);
			this.aiTime = Random.Range(10, 100);
		}
		else
		{
			this.aiTime--;
		}
		if (this.ObjectTarget)
		{
			if (Vector3.Distance(this.ObjectTarget.transform.position, base.gameObject.transform.position) <= 2f)
			{
				base.transform.LookAt(this.ObjectTarget.transform.position);
				if (this.aiTime <= 0 && this.aiState == 1)
				{
					this.character.Attack();
					return;
				}
			}
			else if (this.aiState == 1)
			{
				base.transform.LookAt(this.ObjectTarget.transform.position);
				dir = base.transform.forward;
				dir.Normalize();
				this.character.Move(dir);
				return;
			}
		}
		else
		{
			this.ObjectTarget = GameObject.FindGameObjectWithTag(this.TargetTag);
		}
	}

	// Token: 0x040007B2 RID: 1970
	public GameObject ObjectTarget;

	// Token: 0x040007B3 RID: 1971
	public string TargetTag = "Player";

	// Token: 0x040007B4 RID: 1972
	private CharacterSystem character;

	// Token: 0x040007B5 RID: 1973
	private int aiTime;

	// Token: 0x040007B6 RID: 1974
	private int aiState;
}
