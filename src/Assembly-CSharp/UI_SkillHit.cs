using UnityEngine;
using UnityEngine.UI;

public class UI_SkillHit : UI_ErrorHint
{
	private static UI_SkillHit ince;

	public static UI_SkillHit _ince
	{
		get
		{
			if ((Object)(object)ince == (Object)null)
			{
				ince = ((Component)Object.Instantiate<GameObject>(jsonData.instance.SkillHint).transform.Find("Text_error")).GetComponent<UI_SkillHit>();
				ince.text_content = ((Component)((Component)ince).transform.GetChild(1)).gameObject.GetComponent<Text>();
				ince.ani_control = ((Component)ince).gameObject.GetComponent<Animator>();
				ince.anmaitonName = "SkillHint";
			}
			return ince;
		}
	}

	private void Awake()
	{
		ince = this;
		text_content = ((Component)((Component)this).transform.Find("Text")).gameObject.GetComponent<Text>();
		ani_control = ((Component)this).gameObject.GetComponent<Animator>();
	}

	public override void errorShow(string str, int showType = 0)
	{
		delegateList.Add(delegate
		{
			text_content.text = str;
			ani_control.Play(anmaitonName);
		});
		if (delegateList.Count == 1)
		{
			delegateList[0](str);
		}
	}

	public override void animationFinsh()
	{
		delegateList.RemoveAt(0);
		if (delegateList.Count > 0)
		{
			delegateList[0]("");
		}
	}
}
