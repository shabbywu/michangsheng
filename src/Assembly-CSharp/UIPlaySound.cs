using System;
using UnityEngine;
using YSGame;

// Token: 0x0200006D RID: 109
[AddComponentMenu("NGUI/Interaction/Play Sound")]
public class UIPlaySound : MonoBehaviour
{
	// Token: 0x17000088 RID: 136
	// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001D350 File Offset: 0x0001B550
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

	// Token: 0x0600055D RID: 1373 RVA: 0x0001D380 File Offset: 0x0001B580
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

	// Token: 0x0600055E RID: 1374 RVA: 0x0001D3D0 File Offset: 0x0001B5D0
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

	// Token: 0x0600055F RID: 1375 RVA: 0x0001D41D File Offset: 0x0001B61D
	private void OnClick()
	{
		if (this.canPlay && this.trigger == UIPlaySound.Trigger.OnClick)
		{
			this.Play();
		}
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0001D435 File Offset: 0x0001B635
	private void OnSelect(bool isSelected)
	{
		if (this.canPlay && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0001D451 File Offset: 0x0001B651
	public void Play()
	{
		MusicMag.instance.PlayEffectMusic(this.audioClip, 1f);
	}

	// Token: 0x0400037C RID: 892
	public AudioClip audioClip;

	// Token: 0x0400037D RID: 893
	public UIPlaySound.Trigger trigger;

	// Token: 0x0400037E RID: 894
	private bool mIsOver;

	// Token: 0x0400037F RID: 895
	[Range(0f, 1f)]
	public float volume = 1f;

	// Token: 0x04000380 RID: 896
	[Range(0f, 2f)]
	public float pitch = 1f;

	// Token: 0x020011E8 RID: 4584
	public enum Trigger
	{
		// Token: 0x040063EE RID: 25582
		OnClick,
		// Token: 0x040063EF RID: 25583
		OnMouseOver,
		// Token: 0x040063F0 RID: 25584
		OnMouseOut,
		// Token: 0x040063F1 RID: 25585
		OnPress,
		// Token: 0x040063F2 RID: 25586
		OnRelease,
		// Token: 0x040063F3 RID: 25587
		Custom
	}
}
