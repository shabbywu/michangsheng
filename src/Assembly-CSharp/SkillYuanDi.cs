using System;
using UnityEngine;
using YSGame;

// Token: 0x02000245 RID: 581
public class SkillYuanDi : MonoBehaviour
{
	// Token: 0x060011DE RID: 4574 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x000111B3 File Offset: 0x0000F3B3
	public void removeSelf()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x000ACE7C File Offset: 0x000AB07C
	public void removeParent()
	{
		try
		{
			if (base.transform.parent != null)
			{
				Object.Destroy(base.transform.parent.gameObject);
			}
			else
			{
				Debug.LogError("特效移除父物体时出错:" + base.gameObject.name + "父物体为空");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"特效移除父物体时出现异常:",
				ex.Message,
				"\n",
				ex.StackTrace,
				"\n"
			}));
		}
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x000112BB File Offset: 0x0000F4BB
	public void PlayHit()
	{
		YSFuncList.Ints.Continue();
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x000ACF24 File Offset: 0x000AB124
	public void ScreenShake()
	{
		FightScreenShake fightScreenShake = Object.FindObjectOfType<FightScreenShake>();
		if (fightScreenShake != null)
		{
			fightScreenShake.Shake();
		}
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x000ACF48 File Offset: 0x000AB148
	public void FlashBlack()
	{
		FightFlashBlack fightFlashBlack = Object.FindObjectOfType<FightFlashBlack>();
		if (fightFlashBlack != null)
		{
			fightFlashBlack.Flash();
		}
	}
}
