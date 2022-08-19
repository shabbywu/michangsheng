using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class FrontMover : MonoBehaviour
{
	// Token: 0x06000040 RID: 64 RVA: 0x000031A5 File Offset: 0x000013A5
	private void Start()
	{
		base.InvokeRepeating("StartAgain", 0f, this.repeatingTime);
		this.effect.Play();
		this.startSpeed = this.speed;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000031D4 File Offset: 0x000013D4
	private void StartAgain()
	{
		this.startSpeed = this.speed;
		base.transform.position = this.pivot.position;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000031F8 File Offset: 0x000013F8
	private void Update()
	{
		this.startSpeed *= this.drug;
		base.transform.position += base.transform.forward * (this.startSpeed * Time.deltaTime);
	}

	// Token: 0x04000027 RID: 39
	public Transform pivot;

	// Token: 0x04000028 RID: 40
	public ParticleSystem effect;

	// Token: 0x04000029 RID: 41
	public float speed = 15f;

	// Token: 0x0400002A RID: 42
	public float drug = 1f;

	// Token: 0x0400002B RID: 43
	public float repeatingTime = 1f;

	// Token: 0x0400002C RID: 44
	private float startSpeed;
}
