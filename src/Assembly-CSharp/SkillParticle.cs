using System;
using UnityEngine;
using YSGame;

// Token: 0x0200062F RID: 1583
public class SkillParticle : MonoBehaviour
{
	// Token: 0x06002752 RID: 10066 RVA: 0x0001F2E1 File Offset: 0x0001D4E1
	private void Start()
	{
		base.Invoke("playHit", this.hitTime);
		base.Invoke("removeThis", this.RemoveTime);
	}

	// Token: 0x06002753 RID: 10067 RVA: 0x000111B3 File Offset: 0x0000F3B3
	public void removeThis()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x000112BB File Offset: 0x0000F4BB
	public void playHit()
	{
		YSFuncList.Ints.Continue();
	}

	// Token: 0x06002755 RID: 10069 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0400215B RID: 8539
	public float RemoveTime = 1f;

	// Token: 0x0400215C RID: 8540
	public float hitTime = 0.1f;
}
