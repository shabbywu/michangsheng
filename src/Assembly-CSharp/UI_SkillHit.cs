using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000414 RID: 1044
public class UI_SkillHit : UI_ErrorHint
{
	// Token: 0x17000285 RID: 645
	// (get) Token: 0x060021A7 RID: 8615 RVA: 0x000E9670 File Offset: 0x000E7870
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

	// Token: 0x060021A8 RID: 8616 RVA: 0x000E9703 File Offset: 0x000E7903
	private void Awake()
	{
		UI_SkillHit.ince = this;
		this.text_content = base.transform.Find("Text").gameObject.GetComponent<Text>();
		this.ani_control = base.gameObject.GetComponent<Animator>();
	}

	// Token: 0x060021A9 RID: 8617 RVA: 0x000E973C File Offset: 0x000E793C
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

	// Token: 0x060021AA RID: 8618 RVA: 0x000E9799 File Offset: 0x000E7999
	public override void animationFinsh()
	{
		this.delegateList.RemoveAt(0);
		if (this.delegateList.Count > 0)
		{
			this.delegateList[0]("");
		}
	}

	// Token: 0x04001B20 RID: 6944
	private static UI_SkillHit ince;
}
