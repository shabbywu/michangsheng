using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200040B RID: 1035
public class UI_ErrorHint : MonoBehaviour
{
	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06002150 RID: 8528 RVA: 0x000E840C File Offset: 0x000E660C
	public static UI_ErrorHint _instance
	{
		get
		{
			if (UI_ErrorHint.instance == null)
			{
				UI_ErrorHint.instance = Object.Instantiate<GameObject>(jsonData.instance.TextError).transform.Find("Text_error").GetComponent<UI_ErrorHint>();
				UI_ErrorHint.instance.ani_control = UI_ErrorHint.instance.gameObject.GetComponent<Animator>();
			}
			return UI_ErrorHint.instance;
		}
	}

	// Token: 0x06002151 RID: 8529 RVA: 0x000E846C File Offset: 0x000E666C
	private void Awake()
	{
		UI_ErrorHint.instance = this;
		this.ani_control = base.gameObject.GetComponent<Animator>();
	}

	// Token: 0x06002152 RID: 8530 RVA: 0x000E8485 File Offset: 0x000E6685
	private void OnDestroy()
	{
		UI_ErrorHint.instance = null;
	}

	// Token: 0x06002153 RID: 8531 RVA: 0x000E8490 File Offset: 0x000E6690
	public virtual void errorShow(string str, int showType = 0)
	{
		this.delegateList.Add(delegate(string aa)
		{
			this.isShowing = true;
			this.text_content.text = str;
			if (this.spritUI != null && this.spriteList != null)
			{
				this.spritUI.sprite = this.spriteList[showType];
			}
			this.ani_control.SetTrigger(this.anmaitonName);
		});
		if (this.delegateList.Count == 1)
		{
			this.delegateList[0](str);
		}
	}

	// Token: 0x06002154 RID: 8532 RVA: 0x000E84F4 File Offset: 0x000E66F4
	public virtual void animationFinsh()
	{
		this.isShowing = false;
		this.delegateList.RemoveAt(0);
		if (this.delegateList.Count > 0)
		{
			this.delegateList[0]("");
		}
	}

	// Token: 0x04001AE0 RID: 6880
	private static UI_ErrorHint instance;

	// Token: 0x04001AE1 RID: 6881
	protected List<UI_ErrorHint.showNextDelegate> delegateList = new List<UI_ErrorHint.showNextDelegate>();

	// Token: 0x04001AE2 RID: 6882
	protected bool closequen;

	// Token: 0x04001AE3 RID: 6883
	public List<Sprite> spriteList;

	// Token: 0x04001AE4 RID: 6884
	public Image spritUI;

	// Token: 0x04001AE5 RID: 6885
	public bool isShowing;

	// Token: 0x04001AE6 RID: 6886
	public Text text_content;

	// Token: 0x04001AE7 RID: 6887
	protected Animator ani_control;

	// Token: 0x04001AE8 RID: 6888
	public string anmaitonName = "Error";

	// Token: 0x02001394 RID: 5012
	// (Invoke) Token: 0x06007C59 RID: 31833
	protected delegate void showNextDelegate(string aa);
}
