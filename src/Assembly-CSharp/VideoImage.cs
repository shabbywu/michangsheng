using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x020001F4 RID: 500
[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(VideoPlayer))]
public class VideoImage : MonoBehaviour
{
	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06001488 RID: 5256 RVA: 0x000839C0 File Offset: 0x00081BC0
	public string TargetDirPath
	{
		get
		{
			return string.Concat(new string[]
			{
				Application.dataPath,
				"/Custom/",
				this.GroupName,
				"/",
				this.TargetFileName
			});
		}
	}

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06001489 RID: 5257 RVA: 0x000839F7 File Offset: 0x00081BF7
	public string TargetVideoFilePath
	{
		get
		{
			return this.TargetDirPath + "/" + this.TargetFileName + ".mp4";
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x0600148A RID: 5258 RVA: 0x00083A14 File Offset: 0x00081C14
	public string TargetVideoUrl
	{
		get
		{
			return "file://" + this.TargetVideoFilePath;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x0600148B RID: 5259 RVA: 0x00083A26 File Offset: 0x00081C26
	public VideoPlayer VideoPlayer
	{
		get
		{
			if (this.videoPlayer == null)
			{
				this.videoPlayer = base.GetComponent<VideoPlayer>();
			}
			return this.videoPlayer;
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x0600148C RID: 5260 RVA: 0x00083A48 File Offset: 0x00081C48
	public RawImage Display
	{
		get
		{
			if (this.display == null)
			{
				this.display = base.GetComponent<RawImage>();
			}
			return this.display;
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x0600148D RID: 5261 RVA: 0x00083A6A File Offset: 0x00081C6A
	private bool IsPlaying
	{
		get
		{
			if (this.mode == VideoImageMode.Video)
			{
				return this.videoPlayer.isPlaying;
			}
			return this.isPlaying;
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x0600148E RID: 5262 RVA: 0x00083A88 File Offset: 0x00081C88
	public float PlayProcess
	{
		get
		{
			float num = 1f;
			if (this.IsPlaying)
			{
				if (this.mode == VideoImageMode.Fallback && this.FallbackSprites.Count > 0)
				{
					num = ((float)this.imagePlayIndex - 1f) / (float)this.FallbackSprites.Count;
					num += (this.SpriteSpaceTime - this.imagePlayCD) / this.SpriteSpaceTime / (float)this.FallbackSprites.Count;
				}
				if (this.mode == VideoImageMode.Image && this.TargetTexture2Ds.Count > 0)
				{
					num = ((float)this.imagePlayIndex - 1f) / (float)this.TargetTexture2Ds.Count;
					num += (this.SpriteSpaceTime - this.imagePlayCD) / this.SpriteSpaceTime / (float)this.TargetTexture2Ds.Count;
				}
				if (this.mode == VideoImageMode.Video)
				{
					num = (float)this.VideoPlayer.frame / this.VideoPlayer.frameCount;
				}
			}
			return num;
		}
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x00083B76 File Offset: 0x00081D76
	private void Awake()
	{
		this.InitComponent();
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x00083B80 File Offset: 0x00081D80
	private void Update()
	{
		if (this.IsPlaying && this.mode != VideoImageMode.Video)
		{
			this.imagePlayCD -= Time.deltaTime;
			if (this.imagePlayCD < 0f)
			{
				int num = 0;
				if (this.mode == VideoImageMode.Fallback)
				{
					num = this.FallbackSprites.Count;
				}
				if (this.mode == VideoImageMode.Image)
				{
					num = this.TargetTexture2Ds.Count;
				}
				if (this.imagePlayIndex >= num)
				{
					this.EndPlay(this.VideoPlayer);
				}
				else
				{
					if (this.mode == VideoImageMode.Fallback)
					{
						this.display.texture = this.FallbackSprites[this.imagePlayIndex].texture;
					}
					if (this.mode == VideoImageMode.Image)
					{
						this.display.texture = this.TargetTexture2Ds[this.imagePlayIndex];
					}
					this.imagePlayIndex++;
				}
				this.imagePlayCD = this.SpriteSpaceTime;
			}
		}
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x00083C70 File Offset: 0x00081E70
	private void InitComponent()
	{
		if (this.RenderTexture == null)
		{
			Debug.LogError("VideoImage没有设置渲染材质，初始化失败");
			return;
		}
		this.Display.texture = null;
		this.VideoPlayer.playOnAwake = false;
		this.VideoPlayer.source = 1;
		this.VideoPlayer.renderMode = 2;
		this.VideoPlayer.targetTexture = this.RenderTexture;
		this.VideoPlayer.loopPointReached += new VideoPlayer.EventHandler(this.EndPlay);
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x00083CF0 File Offset: 0x00081EF0
	private void InitDisplayData()
	{
		this.mode = VideoImageMode.Fallback;
		if (new DirectoryInfo(this.TargetDirPath).Exists)
		{
			this.TargetTexture2Ds.Clear();
			for (int i = 0; i < 2147483647; i++)
			{
				FileInfo fileInfo = new FileInfo(string.Format("{0}/{1}_{2}.png", this.TargetDirPath, this.TargetFileName, i));
				FileInfo fileInfo2 = new FileInfo(string.Format("{0}/{1}_{2}.jpg", this.TargetDirPath, this.TargetFileName, i));
				bool flag = false;
				if (fileInfo.Exists)
				{
					Texture2D item;
					if (FileEx.LoadTex2D(fileInfo.FullName, out item))
					{
						this.TargetTexture2Ds.Add(item);
						flag = true;
					}
				}
				else
				{
					if (!fileInfo2.Exists)
					{
						break;
					}
					Texture2D item2;
					if (FileEx.LoadTex2D(fileInfo2.FullName, out item2))
					{
						this.TargetTexture2Ds.Add(item2);
						flag = true;
					}
				}
				if (!flag)
				{
					break;
				}
			}
			if (this.TargetTexture2Ds.Count > 0)
			{
				this.mode = VideoImageMode.Image;
			}
			if (File.Exists(this.TargetVideoFilePath))
			{
				this.videoPlayer.url = this.TargetVideoUrl;
				this.display.texture = this.RenderTexture;
				this.mode = VideoImageMode.Video;
			}
		}
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x00083E1C File Offset: 0x0008201C
	public void Play()
	{
		this.InitDisplayData();
		if (this.mode == VideoImageMode.Video)
		{
			this.videoPlayer.Play();
		}
		else
		{
			this.imagePlayCD = 0f;
			this.imagePlayIndex = 0;
			this.isPlaying = true;
		}
		if (this.OnPlayBegin != null)
		{
			this.OnPlayBegin.Invoke();
		}
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x00083E71 File Offset: 0x00082071
	public void EndPlay(VideoPlayer player)
	{
		player.Stop();
		this.isPlaying = false;
		if (this.OnPlayFinshed != null)
		{
			this.OnPlayFinshed.Invoke();
		}
	}

	// Token: 0x04000F42 RID: 3906
	[Header("视频渲染材质")]
	public RenderTexture RenderTexture;

	// Token: 0x04000F43 RID: 3907
	[Header("后备图片")]
	public List<Sprite> FallbackSprites;

	// Token: 0x04000F44 RID: 3908
	[Header("分组")]
	public string GroupName;

	// Token: 0x04000F45 RID: 3909
	[Header("目标文件名")]
	public string TargetFileName;

	// Token: 0x04000F46 RID: 3910
	[Header("图片间隔时间")]
	public float SpriteSpaceTime = 1f;

	// Token: 0x04000F47 RID: 3911
	public UnityEvent OnPlayBegin;

	// Token: 0x04000F48 RID: 3912
	public UnityEvent OnPlayFinshed;

	// Token: 0x04000F49 RID: 3913
	private VideoPlayer videoPlayer;

	// Token: 0x04000F4A RID: 3914
	private RawImage display;

	// Token: 0x04000F4B RID: 3915
	private List<Texture2D> TargetTexture2Ds = new List<Texture2D>();

	// Token: 0x04000F4C RID: 3916
	private VideoImageMode mode;

	// Token: 0x04000F4D RID: 3917
	private bool isPlaying;

	// Token: 0x04000F4E RID: 3918
	private int imagePlayIndex;

	// Token: 0x04000F4F RID: 3919
	private float imagePlayCD;
}
