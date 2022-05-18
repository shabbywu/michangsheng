using System;
using UnityEngine;
using YSGame;

// Token: 0x02000091 RID: 145
[AddComponentMenu("NGUI/Interaction/Play Sound")]
public class UIPlaySound : MonoBehaviour
{
	// Token: 0x17000096 RID: 150
	// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00073570 File Offset: 0x00071770
	private bool canPlay
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			UIButton component = base.GetComponent<UIButton>();
			return component == null || component.isEnabled;
		}
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x000735A0 File Offset: 0x000717A0
	private void OnHover(bool isOver)
	{
		if (this.trigger == UIPlaySound.Trigger.OnMouseOver)
		{
			if (this.mIsOver == isOver)
			{
				return;
			}
			this.mIsOver = isOver;
		}
		if (this.canPlay && ((isOver && this.trigger == UIPlaySound.Trigger.OnMouseOver) || (!isOver && this.trigger == UIPlaySound.Trigger.OnMouseOut)))
		{
			this.Play();
		}
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x000735F0 File Offset: 0x000717F0
	private void OnPress(bool isPressed)
	{
		if (this.trigger == UIPlaySound.Trigger.OnPress)
		{
			if (this.mIsOver == isPressed)
			{
				return;
			}
			this.mIsOver = isPressed;
		}
		if (this.canPlay && ((isPressed && this.trigger == UIPlaySound.Trigger.OnPress) || (!isPressed && this.trigger == UIPlaySound.Trigger.OnRelease)))
		{
			this.Play();
		}
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0000935C File Offset: 0x0000755C
	private void OnClick()
	{
		if (this.canPlay && this.trigger == UIPlaySound.Trigger.OnClick)
		{
			this.Play();
		}
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00009374 File Offset: 0x00007574
	private void OnSelect(bool isSelected)
	{
		if (this.canPlay && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x00009390 File Offset: 0x00007590
	public void Play()
	{
		MusicMag.instance.PlayEffectMusic(this.audioClip, 1f);
	}

	// Token: 0x0400041A RID: 1050
	public AudioClip audioClip;

	// Token: 0x0400041B RID: 1051
	public UIPlaySound.Trigger trigger;

	// Token: 0x0400041C RID: 1052
	private bool mIsOver;

	// Token: 0x0400041D RID: 1053
	[Range(0f, 1f)]
	public float volume = 1f;

	// Token: 0x0400041E RID: 1054
	[Range(0f, 2f)]
	public float pitch = 1f;

	// Token: 0x02000092 RID: 146
	public enum Trigger
	{
		// Token: 0x04000420 RID: 1056
		OnClick,
		// Token: 0x04000421 RID: 1057
		OnMouseOver,
		// Token: 0x04000422 RID: 1058
		OnMouseOut,
		// Token: 0x04000423 RID: 1059
		OnPress,
		// Token: 0x04000424 RID: 1060
		OnRelease,
		// Token: 0x04000425 RID: 1061
		Custom
	}
}
