using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class FrontMover : MonoBehaviour
{
	// Token: 0x06000040 RID: 64 RVA: 0x0000414A File Offset: 0x0000234A
	private void Start()
	{
		base.InvokeRepeating("StartAgain", 0f, this.repeatingTime);
		this.effect.Play();
		this.startSpeed = this.speed;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00004179 File Offset: 0x00002379
	private void StartAgain()
	{
		this.startSpeed = this.speed;
		base.transform.position = this.pivot.position;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x0005DA58 File Offset: 0x0005BC58
	private void Update()
	{
		this.startSpeed *= this.drug;
		base.transform.position += base.transform.forward * (this.startSpeed * Time.deltaTime);
	}

	// Token: 0x0400002A RID: 42
	public Transform pivot;

	// Token: 0x0400002B RID: 43
	public ParticleSystem effect;

	// Token: 0x0400002C RID: 44
	public float speed = 15f;

	// Token: 0x0400002D RID: 45
	public float drug = 1f;

	// Token: 0x0400002E RID: 46
	public float repeatingTime = 1f;

	// Token: 0x0400002F RID: 47
	private float startSpeed;
}
