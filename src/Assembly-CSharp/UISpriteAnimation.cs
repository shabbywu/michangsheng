using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011B RID: 283
[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x170001CC RID: 460
	// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0000D315 File Offset: 0x0000B515
	public int frames
	{
		get
		{
			return this.mSpriteNames.Count;
		}
	}

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0000D322 File Offset: 0x0000B522
	// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0000D32A File Offset: 0x0000B52A
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

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0000D333 File Offset: 0x0000B533
	// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0000D33B File Offset: 0x0000B53B
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

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0000D358 File Offset: 0x0000B558
	// (set) Token: 0x06000B30 RID: 2864 RVA: 0x0000D360 File Offset: 0x0000B560
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

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0000D369 File Offset: 0x0000B569
	public bool isPlaying
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x0000D371 File Offset: 0x0000B571
	protected virtual void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00090F60 File Offset: 0x0008F160
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

	// Token: 0x06000B34 RID: 2868 RVA: 0x00091058 File Offset: 0x0008F258
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

	// Token: 0x06000B35 RID: 2869 RVA: 0x00091114 File Offset: 0x0008F314
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

	// Token: 0x040007DB RID: 2011
	[HideInInspector]
	[SerializeField]
	protected int mFPS = 30;

	// Token: 0x040007DC RID: 2012
	[HideInInspector]
	[SerializeField]
	protected string mPrefix = "";

	// Token: 0x040007DD RID: 2013
	[HideInInspector]
	[SerializeField]
	protected bool mLoop = true;

	// Token: 0x040007DE RID: 2014
	[HideInInspector]
	[SerializeField]
	protected bool mSnap = true;

	// Token: 0x040007DF RID: 2015
	protected UISprite mSprite;

	// Token: 0x040007E0 RID: 2016
	protected float mDelta;

	// Token: 0x040007E1 RID: 2017
	protected int mIndex;

	// Token: 0x040007E2 RID: 2018
	protected bool mActive = true;

	// Token: 0x040007E3 RID: 2019
	protected List<string> mSpriteNames = new List<string>();
}
