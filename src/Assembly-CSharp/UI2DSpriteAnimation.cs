using System;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class UI2DSpriteAnimation : MonoBehaviour
{
	// Token: 0x06000988 RID: 2440 RVA: 0x000878C8 File Offset: 0x00085AC8
	private void Start()
	{
		this.mUnitySprite = base.GetComponent<SpriteRenderer>();
		this.mNguiSprite = base.GetComponent<UI2DSprite>();
		if (this.framerate > 0)
		{
			this.mUpdate = (this.ignoreTimeScale ? RealTime.time : Time.time) + 1f / (float)this.framerate;
		}
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x00087920 File Offset: 0x00085B20
	private void Update()
	{
		if (this.framerate != 0 && this.frames != null && this.frames.Length != 0)
		{
			float num = this.ignoreTimeScale ? RealTime.time : Time.time;
			if (this.mUpdate < num)
			{
				this.mUpdate = num;
				this.mIndex = NGUIMath.RepeatIndex((this.framerate > 0) ? (this.mIndex + 1) : (this.mIndex - 1), this.frames.Length);
				this.mUpdate = num + Mathf.Abs(1f / (float)this.framerate);
				if (this.mUnitySprite != null)
				{
					this.mUnitySprite.sprite = this.frames[this.mIndex];
					return;
				}
				if (this.mNguiSprite != null)
				{
					this.mNguiSprite.nextSprite = this.frames[this.mIndex];
				}
			}
		}
	}

	// Token: 0x0400068E RID: 1678
	public int framerate = 20;

	// Token: 0x0400068F RID: 1679
	public bool ignoreTimeScale = true;

	// Token: 0x04000690 RID: 1680
	public Sprite[] frames;

	// Token: 0x04000691 RID: 1681
	private SpriteRenderer mUnitySprite;

	// Token: 0x04000692 RID: 1682
	private UI2DSprite mNguiSprite;

	// Token: 0x04000693 RID: 1683
	private int mIndex;

	// Token: 0x04000694 RID: 1684
	private float mUpdate;
}
