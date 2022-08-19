using System;
using UnityEngine;
using YSGame;

// Token: 0x02000472 RID: 1138
public class SkillParticle : MonoBehaviour
{
	// Token: 0x06002399 RID: 9113 RVA: 0x000F3AA8 File Offset: 0x000F1CA8
	private void Start()
	{
		base.Invoke("playHit", this.hitTime);
		base.Invoke("removeThis", this.RemoveTime);
	}

	// Token: 0x0600239A RID: 9114 RVA: 0x0005C928 File Offset: 0x0005AB28
	public void removeThis()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600239B RID: 9115 RVA: 0x000656B8 File Offset: 0x000638B8
	public void playHit()
	{
		YSFuncList.Ints.Continue();
	}

	// Token: 0x0600239C RID: 9116 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001C83 RID: 7299
	public float RemoveTime = 1f;

	// Token: 0x04001C84 RID: 7300
	public float hitTime = 0.1f;
}
