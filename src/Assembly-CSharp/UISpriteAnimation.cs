using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AF RID: 175
[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0003E6A7 File Offset: 0x0003C8A7
	public int frames
	{
		get
		{
			return this.mSpriteNames.Count;
		}
	}

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0003E6B4 File Offset: 0x0003C8B4
	// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0003E6BC File Offset: 0x0003C8BC
	public int framesPerSecond
	{
		get
		{
			return this.mFPS;
		}
		set
		{
			this.mFPS = value;
		}
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0003E6C5 File Offset: 0x0003C8C5
	// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0003E6CD File Offset: 0x0003C8CD
	public string namePrefix
	{
		get
		{
			return this.mPrefix;
		}
		set
		{
			if (this.mPrefix != value)
			{
				this.mPrefix = value;
				this.RebuildSpriteList();
			}
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0003E6EA File Offset: 0x0003C8EA
	// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0003E6F2 File Offset: 0x0003C8F2
	public bool loop
	{
		get
		{
			return this.mLoop;
		}
		set
		{
			this.mLoop = value;
		}
	}

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0003E6FB File Offset: 0x0003C8FB
	public bool isPlaying
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0003E703 File Offset: 0x0003C903
	protected virtual void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0003E70C File Offset: 0x0003C90C
	protected virtual void Update()
	{
		if (this.mActive && this.mSpriteNames.Count > 1 && Application.isPlaying && (float)this.mFPS > 0f)
		{
			this.mDelta += RealTime.deltaTime;
			float num = 1f / (float)this.mFPS;
			if (num < this.mDelta)
			{
				this.mDelta = ((num > 0f) ? (this.mDelta - num) : 0f);
				int num2 = this.mIndex + 1;
				this.mIndex = num2;
				if (num2 >= this.mSpriteNames.Count)
				{
					this.mIndex = 0;
					this.mActive = this.loop;
				}
				if (this.mActive)
				{
					this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
					if (this.mSnap)
					{
						this.mSprite.MakePixelPerfect();
					}
				}
			}
		}
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0003E804 File Offset: 0x0003CA04
	public void RebuildSpriteList()
	{
		if (this.mSprite == null)
		{
			this.mSprite = base.GetComponent<UISprite>();
		}
		this.mSpriteNames.Clear();
		if (this.mSprite != null && this.mSprite.atlas != null)
		{
			List<UISpriteData> spriteList = this.mSprite.atlas.spriteList;
			int i = 0;
			int count = spriteList.Count;
			while (i < count)
			{
				UISpriteData uispriteData = spriteList[i];
				if (string.IsNullOrEmpty(this.mPrefix) || uispriteData.name.StartsWith(this.mPrefix))
				{
					this.mSpriteNames.Add(uispriteData.name);
				}
				i++;
			}
			this.mSpriteNames.Sort();
		}
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0003E8C0 File Offset: 0x0003CAC0
	public void Reset()
	{
		this.mActive = true;
		this.mIndex = 0;
		if (this.mSprite != null && this.mSpriteNames.Count > 0)
		{
			this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
			if (this.mSnap)
			{
				this.mSprite.MakePixelPerfect();
			}
		}
	}

	// Token: 0x04000644 RID: 1604
	[HideInInspector]
	[SerializeField]
	protected int mFPS = 30;

	// Token: 0x04000645 RID: 1605
	[HideInInspector]
	[SerializeField]
	protected string mPrefix = "";

	// Token: 0x04000646 RID: 1606
	[HideInInspector]
	[SerializeField]
	protected bool mLoop = true;

	// Token: 0x04000647 RID: 1607
	[HideInInspector]
	[SerializeField]
	protected bool mSnap = true;

	// Token: 0x04000648 RID: 1608
	protected UISprite mSprite;

	// Token: 0x04000649 RID: 1609
	protected float mDelta;

	// Token: 0x0400064A RID: 1610
	protected int mIndex;

	// Token: 0x0400064B RID: 1611
	protected bool mActive = true;

	// Token: 0x0400064C RID: 1612
	protected List<string> mSpriteNames = new List<string>();
}
