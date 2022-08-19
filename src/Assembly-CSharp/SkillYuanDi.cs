using System;
using UnityEngine;
using YSGame;

// Token: 0x02000169 RID: 361
public class SkillYuanDi : MonoBehaviour
{
	// Token: 0x06000F80 RID: 3968 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x0005C928 File Offset: 0x0005AB28
	public void removeSelf()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x0005D2B8 File Offset: 0x0005B4B8
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

	// Token: 0x06000F83 RID: 3971 RVA: 0x0005D360 File Offset: 0x0005B560
	public void PlayHit()
	{
		Debug.Log(string.Format("[{0}] {1} 触发了playhit", Time.frameCount, base.gameObject.GetPath()));
		MessageMag.Instance.Send(MessageName.MSG_Fight_Effect_Special, null);
		YSFuncList.Ints.Continue();
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x0005D3A0 File Offset: 0x0005B5A0
	public void ScreenShake()
	{
		FightScreenShake fightScreenShake = Object.FindObjectOfType<FightScreenShake>();
		if (fightScreenShake != null)
		{
			fightScreenShake.Shake();
		}
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x0005D3C4 File Offset: 0x0005B5C4
	public void FlashBlack()
	{
		FightFlashBlack fightFlashBlack = Object.FindObjectOfType<FightFlashBlack>();
		if (fightFlashBlack != null)
		{
			fightFlashBlack.Flash();
		}
	}
}
