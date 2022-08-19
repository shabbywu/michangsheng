using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class UI2DSpriteAnimation : MonoBehaviour
{
	// Token: 0x060008D0 RID: 2256 RVA: 0x00033C68 File Offset: 0x00031E68
	private void Start()
	{
		this.mUnitySprite = base.GetComponent<SpriteRenderer>();
		this.mNguiSprite = base.GetComponent<UI2DSprite>();
		if (this.framerate > 0)
		{
			this.mUpdate = (this.ignoreTimeScale ? RealTime.time : Time.time) + 1f / (float)this.framerate;
		}
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00033CC0 File Offset: 0x00031EC0
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

	// Token: 0x04000560 RID: 1376
	public int framerate = 20;

	// Token: 0x04000561 RID: 1377
	public bool ignoreTimeScale = true;

	// Token: 0x04000562 RID: 1378
	public Sprite[] frames;

	// Token: 0x04000563 RID: 1379
	private SpriteRenderer mUnitySprite;

	// Token: 0x04000564 RID: 1380
	private UI2DSprite mNguiSprite;

	// Token: 0x04000565 RID: 1381
	private int mIndex;

	// Token: 0x04000566 RID: 1382
	private float mUpdate;
}
