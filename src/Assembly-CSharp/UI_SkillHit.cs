using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005C6 RID: 1478
public class UI_SkillHit : UI_ErrorHint
{
	// Token: 0x170002CF RID: 719
	// (get) Token: 0x0600255F RID: 9567 RVA: 0x0012ADB4 File Offset: 0x00128FB4
	public static UI_SkillHit _ince
	{
		get
		{
			if (UI_SkillHit.ince == null)
			{
				UI_SkillHit.ince = Object.Instantiate<GameObject>(jsonData.instance.SkillHint).transform.Find("Text_error").GetComponent<UI_SkillHit>();
				UI_SkillHit.ince.text_content = UI_SkillHit.ince.transform.GetChild(1).gameObject.GetComponent<Text>();
				UI_SkillHit.ince.ani_control = UI_SkillHit.ince.gameObject.GetComponent<Animator>();
				UI_SkillHit.ince.anmaitonName = "SkillHint";
			}
			return UI_SkillHit.ince;
		}
	}

	// Token: 0x06002560 RID: 9568 RVA: 0x0001DF62 File Offset: 0x0001C162
	private void Awake()
	{
		UI_SkillHit.ince = this;
		this.text_content = base.transform.Find("Text").gameObject.GetComponent<Text>();
		this.ani_control = base.gameObject.GetComponent<Animator>();
	}

	// Token: 0x06002561 RID: 9569 RVA: 0x0012AE48 File Offset: 0x00129048
	public override void errorShow(string str, int showType = 0)
	{
		this.delegateList.Add(delegate(string aa)
		{
			this.text_content.text = str;
			this.ani_control.Play(this.anmaitonName);
		});
		if (this.delegateList.Count == 1)
		{
			this.delegateList[0](str);
		}
	}

	// Token: 0x06002562 RID: 9570 RVA: 0x0001DF9B File Offset: 0x0001C19B
	public override void animationFinsh()
	{
		this.delegateList.RemoveAt(0);
		if (this.delegateList.Count > 0)
		{
			this.delegateList[0]("");
		}
	}

	// Token: 0x04001FDF RID: 8159
	private static UI_SkillHit ince;
}
