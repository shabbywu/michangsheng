using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x02000308 RID: 776
[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(VideoPlayer))]
public class VideoImage : MonoBehaviour
{
	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06001732 RID: 5938 RVA: 0x000147B4 File Offset: 0x000129B4
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

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06001733 RID: 5939 RVA: 0x000147EB File Offset: 0x000129EB
	public string TargetVideoFilePath
	{
		get
		{
			return this.TargetDirPath + "/" + this.TargetFileName + ".mp4";
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06001734 RID: 5940 RVA: 0x00014808 File Offset: 0x00012A08
	public string TargetVideoUrl
	{
		get
		{
			return "file://" + this.TargetVideoFilePath;
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06001735 RID: 5941 RVA: 0x0001481A File Offset: 0x00012A1A
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

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06001736 RID: 5942 RVA: 0x0001483C File Offset: 0x00012A3C
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

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06001737 RID: 5943 RVA: 0x0001485E File Offset: 0x00012A5E
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

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06001738 RID: 5944 RVA: 0x000CC78C File Offset: 0x000CA98C
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

	// Token: 0x06001739 RID: 5945 RVA: 0x0001487B File Offset: 0x00012A7B
	private void Awake()
	{
		this.InitComponent();
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x000CC87C File Offset: 0x000CAA7C
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

	// Token: 0x0600173C RID: 5948 RVA: 0x000CC96C File Offset: 0x000CAB6C
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

	// Token: 0x0600173D RID: 5949 RVA: 0x000CC9EC File Offset: 0x000CABEC
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

	// Token: 0x0600173E RID: 5950 RVA: 0x000CCB18 File Offset: 0x000CAD18
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

	// Token: 0x0600173F RID: 5951 RVA: 0x00014883 File Offset: 0x00012A83
	public void EndPlay(VideoPlayer player)
	{
		player.Stop();
		this.isPlaying = false;
		if (this.OnPlayFinshed != null)
		{
			this.OnPlayFinshed.Invoke();
		}
	}

	// Token: 0x04001284 RID: 4740
	[Header("视频渲染材质")]
	public RenderTexture RenderTexture;

	// Token: 0x04001285 RID: 4741
	[Header("后备图片")]
	public List<Sprite> FallbackSprites;

	// Token: 0x04001286 RID: 4742
	[Header("分组")]
	public string GroupName;

	// Token: 0x04001287 RID: 4743
	[Header("目标文件名")]
	public string TargetFileName;

	// Token: 0x04001288 RID: 4744
	[Header("图片间隔时间")]
	public float SpriteSpaceTime = 1f;

	// Token: 0x04001289 RID: 4745
	public UnityEvent OnPlayBegin;

	// Token: 0x0400128A RID: 4746
	public UnityEvent OnPlayFinshed;

	// Token: 0x0400128B RID: 4747
	private VideoPlayer videoPlayer;

	// Token: 0x0400128C RID: 4748
	private RawImage display;

	// Token: 0x0400128D RID: 4749
	private List<Texture2D> TargetTexture2Ds = new List<Texture2D>();

	// Token: 0x0400128E RID: 4750
	private VideoImageMode mode;

	// Token: 0x0400128F RID: 4751
	private bool isPlaying;

	// Token: 0x04001290 RID: 4752
	private int imagePlayIndex;

	// Token: 0x04001291 RID: 4753
	private float imagePlayCD;
}
