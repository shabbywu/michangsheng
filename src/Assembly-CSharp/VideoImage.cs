using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(VideoPlayer))]
public class VideoImage : MonoBehaviour
{
	[Header("视频渲染材质")]
	public RenderTexture RenderTexture;

	[Header("后备图片")]
	public List<Sprite> FallbackSprites;

	[Header("分组")]
	public string GroupName;

	[Header("目标文件名")]
	public string TargetFileName;

	[Header("图片间隔时间")]
	public float SpriteSpaceTime = 1f;

	public UnityEvent OnPlayBegin;

	public UnityEvent OnPlayFinshed;

	private VideoPlayer videoPlayer;

	private RawImage display;

	private List<Texture2D> TargetTexture2Ds = new List<Texture2D>();

	private VideoImageMode mode;

	private bool isPlaying;

	private int imagePlayIndex;

	private float imagePlayCD;

	public string TargetDirPath => Application.dataPath + "/Custom/" + GroupName + "/" + TargetFileName;

	public string TargetVideoFilePath => TargetDirPath + "/" + TargetFileName + ".mp4";

	public string TargetVideoUrl => "file://" + TargetVideoFilePath;

	public VideoPlayer VideoPlayer
	{
		get
		{
			if ((Object)(object)videoPlayer == (Object)null)
			{
				videoPlayer = ((Component)this).GetComponent<VideoPlayer>();
			}
			return videoPlayer;
		}
	}

	public RawImage Display
	{
		get
		{
			if ((Object)(object)display == (Object)null)
			{
				display = ((Component)this).GetComponent<RawImage>();
			}
			return display;
		}
	}

	private bool IsPlaying
	{
		get
		{
			if (mode == VideoImageMode.Video)
			{
				return videoPlayer.isPlaying;
			}
			return isPlaying;
		}
	}

	public float PlayProcess
	{
		get
		{
			float result = 1f;
			if (IsPlaying)
			{
				if (mode == VideoImageMode.Fallback && FallbackSprites.Count > 0)
				{
					result = ((float)imagePlayIndex - 1f) / (float)FallbackSprites.Count;
					result += (SpriteSpaceTime - imagePlayCD) / SpriteSpaceTime / (float)FallbackSprites.Count;
				}
				if (mode == VideoImageMode.Image && TargetTexture2Ds.Count > 0)
				{
					result = ((float)imagePlayIndex - 1f) / (float)TargetTexture2Ds.Count;
					result += (SpriteSpaceTime - imagePlayCD) / SpriteSpaceTime / (float)TargetTexture2Ds.Count;
				}
				if (mode == VideoImageMode.Video)
				{
					result = (float)VideoPlayer.frame / (float)VideoPlayer.frameCount;
				}
			}
			return result;
		}
	}

	private void Awake()
	{
		InitComponent();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!IsPlaying || mode == VideoImageMode.Video)
		{
			return;
		}
		imagePlayCD -= Time.deltaTime;
		if (!(imagePlayCD < 0f))
		{
			return;
		}
		int num = 0;
		if (mode == VideoImageMode.Fallback)
		{
			num = FallbackSprites.Count;
		}
		if (mode == VideoImageMode.Image)
		{
			num = TargetTexture2Ds.Count;
		}
		if (imagePlayIndex >= num)
		{
			EndPlay(VideoPlayer);
		}
		else
		{
			if (mode == VideoImageMode.Fallback)
			{
				display.texture = (Texture)(object)FallbackSprites[imagePlayIndex].texture;
			}
			if (mode == VideoImageMode.Image)
			{
				display.texture = (Texture)(object)TargetTexture2Ds[imagePlayIndex];
			}
			imagePlayIndex++;
		}
		imagePlayCD = SpriteSpaceTime;
	}

	private void InitComponent()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		if ((Object)(object)RenderTexture == (Object)null)
		{
			Debug.LogError((object)"VideoImage没有设置渲染材质，初始化失败");
			return;
		}
		Display.texture = null;
		VideoPlayer.playOnAwake = false;
		VideoPlayer.source = (VideoSource)1;
		VideoPlayer.renderMode = (VideoRenderMode)2;
		VideoPlayer.targetTexture = RenderTexture;
		VideoPlayer.loopPointReached += new EventHandler(EndPlay);
	}

	private void InitDisplayData()
	{
		mode = VideoImageMode.Fallback;
		if (!new DirectoryInfo(TargetDirPath).Exists)
		{
			return;
		}
		TargetTexture2Ds.Clear();
		for (int i = 0; i < int.MaxValue; i++)
		{
			FileInfo fileInfo = new FileInfo($"{TargetDirPath}/{TargetFileName}_{i}.png");
			FileInfo fileInfo2 = new FileInfo($"{TargetDirPath}/{TargetFileName}_{i}.jpg");
			bool flag = false;
			if (fileInfo.Exists)
			{
				if (FileEx.LoadTex2D(fileInfo.FullName, out var tex))
				{
					TargetTexture2Ds.Add(tex);
					flag = true;
				}
			}
			else
			{
				if (!fileInfo2.Exists)
				{
					break;
				}
				if (FileEx.LoadTex2D(fileInfo2.FullName, out var tex2))
				{
					TargetTexture2Ds.Add(tex2);
					flag = true;
				}
			}
			if (!flag)
			{
				break;
			}
		}
		if (TargetTexture2Ds.Count > 0)
		{
			mode = VideoImageMode.Image;
		}
		if (File.Exists(TargetVideoFilePath))
		{
			videoPlayer.url = TargetVideoUrl;
			display.texture = (Texture)(object)RenderTexture;
			mode = VideoImageMode.Video;
		}
	}

	public void Play()
	{
		InitDisplayData();
		if (mode == VideoImageMode.Video)
		{
			videoPlayer.Play();
		}
		else
		{
			imagePlayCD = 0f;
			imagePlayIndex = 0;
			isPlaying = true;
		}
		if (OnPlayBegin != null)
		{
			OnPlayBegin.Invoke();
		}
	}

	public void EndPlay(VideoPlayer player)
	{
		player.Stop();
		isPlaying = false;
		if (OnPlayFinshed != null)
		{
			OnPlayFinshed.Invoke();
		}
	}
}
