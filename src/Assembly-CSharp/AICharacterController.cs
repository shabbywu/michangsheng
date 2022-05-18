using System;
using UnityEngine;

// Token: 0x02000165 RID: 357
[RequireComponent(typeof(CharacterSystem))]
public class AICharacterController : MonoBehaviour
{
	// Token: 0x06000C6A RID: 3178 RVA: 0x0000E5B6 File Offset: 0x0000C7B6
	private void Start()
	{
		this.character = base.gameObject.GetComponent<CharacterSystem>();
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x00097794 File Offset: 0x00095994
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

	// Token: 0x0400098D RID: 2445
	public GameObject ObjectTarget;

	// Token: 0x0400098E RID: 2446
	public string TargetTag = "Player";

	// Token: 0x0400098F RID: 2447
	private CharacterSystem character;

	// Token: 0x04000990 RID: 2448
	private int aiTime;

	// Token: 0x04000991 RID: 2449
	private int aiState;
}
