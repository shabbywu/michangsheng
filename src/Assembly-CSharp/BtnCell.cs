using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000320 RID: 800
public class BtnCell : LunDaoBtnBase
{
	// Token: 0x06001BA7 RID: 7079 RVA: 0x000C5184 File Offset: 0x000C3384
	private void Awake()
	{
		if (base.GetComponent<Image>() == null)
		{
			base.gameObject.AddComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
		}
		base.AddMouseUpCallBack(new UnityAction(this.MouseUp));
		base.AddMouseEnterCallBack(new UnityAction(this.MouseEnter));
		base.AddMouseExitCallBack(new UnityAction(this.MouseOut));
		base.AddMouseDownCallBack(new UnityAction(this.MouseDown));
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x000C5210 File Offset: 0x000C3410
	private void MouseUp()
	{
		if (!this.isStopSuoFang)
		{
			foreach (Image image in this.targetImageList)
			{
				image.transform.localScale = Vector3.one;
			}
			foreach (Text text in this.targetTextList)
			{
				text.transform.localScale = Vector3.one;
			}
			if (!this.isStopMusic)
			{
				if (this.audioClip != null)
				{
					MusicMag.instance.PlayEffectMusic(this.audioClip, 1f);
				}
				else
				{
					MusicMag.instance.PlayEffectMusic(1, 1f);
				}
			}
		}
		if (!this.IsIn)
		{
			return;
		}
		this.mouseUp.AddListener(new UnityAction(this.MouseOut));
		this.mouseUp.Invoke();
	}

	// Token: 0x06001BA9 RID: 7081 RVA: 0x000C5328 File Offset: 0x000C3528
	private void MouseDown()
	{
		if (!this.isStopSuoFang)
		{
			foreach (Image image in this.targetImageList)
			{
				image.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			}
			foreach (Text text in this.targetTextList)
			{
				text.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			}
		}
		if (!this.IsIn)
		{
			return;
		}
		this.mouseDown.Invoke();
	}

	// Token: 0x06001BAA RID: 7082 RVA: 0x000C5408 File Offset: 0x000C3608
	private void MouseEnter()
	{
		this.IsIn = true;
		if (!this.isStopBlack)
		{
			foreach (Image image in this.targetImageList)
			{
				image.color = new Color(0.5019608f, 0.5019608f, 0.5019608f);
			}
			foreach (Text text in this.targetTextList)
			{
				text.color = new Color(0.5019608f, 0.5019608f, 0.5019608f);
			}
		}
		this.mouseEnter.Invoke();
		MusicMag.instance.PlayEffectMusic(3, 1f);
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x000C54EC File Offset: 0x000C36EC
	private void MouseOut()
	{
		this.IsIn = false;
		if (!this.isStopBlack)
		{
			foreach (Image image in this.targetImageList)
			{
				image.color = new Color(1f, 1f, 1f);
			}
			foreach (Text text in this.targetTextList)
			{
				text.color = new Color(1f, 1f, 1f);
			}
		}
		this.mouseOut.Invoke();
	}

	// Token: 0x0400162C RID: 5676
	public bool isStopBlack;

	// Token: 0x0400162D RID: 5677
	public bool isStopSuoFang;

	// Token: 0x0400162E RID: 5678
	public bool isStopMusic;

	// Token: 0x0400162F RID: 5679
	public bool IsIn;

	// Token: 0x04001630 RID: 5680
	[SerializeField]
	private List<Image> targetImageList;

	// Token: 0x04001631 RID: 5681
	[SerializeField]
	private List<Text> targetTextList;

	// Token: 0x04001632 RID: 5682
	public AudioClip audioClip;

	// Token: 0x04001633 RID: 5683
	public UnityEvent mouseUp;

	// Token: 0x04001634 RID: 5684
	public UnityEvent mouseDown;

	// Token: 0x04001635 RID: 5685
	public UnityEvent mouseEnter;

	// Token: 0x04001636 RID: 5686
	public UnityEvent mouseOut;
}
