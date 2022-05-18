using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005BB RID: 1467
public class UI_ErrorHint : MonoBehaviour
{
	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06002502 RID: 9474 RVA: 0x00129EE4 File Offset: 0x001280E4
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

	// Token: 0x06002503 RID: 9475 RVA: 0x0001DB7C File Offset: 0x0001BD7C
	private void Awake()
	{
		UI_ErrorHint.instance = this;
		this.ani_control = base.gameObject.GetComponent<Animator>();
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x0001DB95 File Offset: 0x0001BD95
	private void OnDestroy()
	{
		UI_ErrorHint.instance = null;
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x00129F44 File Offset: 0x00128144
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

	// Token: 0x06002506 RID: 9478 RVA: 0x0001DB9D File Offset: 0x0001BD9D
	public virtual void animationFinsh()
	{
		this.isShowing = false;
		this.delegateList.RemoveAt(0);
		if (this.delegateList.Count > 0)
		{
			this.delegateList[0]("");
		}
	}

	// Token: 0x04001F9C RID: 8092
	private static UI_ErrorHint instance;

	// Token: 0x04001F9D RID: 8093
	protected List<UI_ErrorHint.showNextDelegate> delegateList = new List<UI_ErrorHint.showNextDelegate>();

	// Token: 0x04001F9E RID: 8094
	protected bool closequen;

	// Token: 0x04001F9F RID: 8095
	public List<Sprite> spriteList;

	// Token: 0x04001FA0 RID: 8096
	public Image spritUI;

	// Token: 0x04001FA1 RID: 8097
	public bool isShowing;

	// Token: 0x04001FA2 RID: 8098
	public Text text_content;

	// Token: 0x04001FA3 RID: 8099
	protected Animator ani_control;

	// Token: 0x04001FA4 RID: 8100
	public string anmaitonName = "Error";

	// Token: 0x020005BC RID: 1468
	// (Invoke) Token: 0x06002509 RID: 9481
	protected delegate void showNextDelegate(string aa);
}
