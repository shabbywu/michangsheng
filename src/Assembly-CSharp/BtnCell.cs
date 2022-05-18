using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000485 RID: 1157
public class BtnCell : LunDaoBtnBase
{
	// Token: 0x06001EE7 RID: 7911 RVA: 0x0010A42C File Offset: 0x0010862C
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

	// Token: 0x06001EE8 RID: 7912 RVA: 0x0010A4B8 File Offset: 0x001086B8
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

	// Token: 0x06001EE9 RID: 7913 RVA: 0x0010A5D0 File Offset: 0x001087D0
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

	// Token: 0x06001EEA RID: 7914 RVA: 0x0010A6B0 File Offset: 0x001088B0
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

	// Token: 0x06001EEB RID: 7915 RVA: 0x0010A794 File Offset: 0x00108994
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

	// Token: 0x04001A51 RID: 6737
	public bool isStopBlack;

	// Token: 0x04001A52 RID: 6738
	public bool isStopSuoFang;

	// Token: 0x04001A53 RID: 6739
	public bool isStopMusic;

	// Token: 0x04001A54 RID: 6740
	public bool IsIn;

	// Token: 0x04001A55 RID: 6741
	[SerializeField]
	private List<Image> targetImageList;

	// Token: 0x04001A56 RID: 6742
	[SerializeField]
	private List<Text> targetTextList;

	// Token: 0x04001A57 RID: 6743
	public AudioClip audioClip;

	// Token: 0x04001A58 RID: 6744
	public UnityEvent mouseUp;

	// Token: 0x04001A59 RID: 6745
	public UnityEvent mouseDown;

	// Token: 0x04001A5A RID: 6746
	public UnityEvent mouseEnter;

	// Token: 0x04001A5B RID: 6747
	public UnityEvent mouseOut;
}
