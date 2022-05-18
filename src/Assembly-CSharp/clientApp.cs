using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using KBEngine;
using UnityEngine;

// Token: 0x020002B0 RID: 688
public class clientApp : KBEMain
{
	// Token: 0x060014E4 RID: 5348 RVA: 0x000BC778 File Offset: 0x000BA978
	protected override void Awake()
	{
		clientApp.InitVersion();
		Debug.Log(string.Format("clientApp.Awake:{0}", DateTime.Now));
		clientApp.Inst = this;
		base.Awake();
		File.WriteAllText(Paths.GetSavePath() + "/devlog.log", "");
		Application.logMessageReceived += new Application.LogCallback(this.LogCallback);
		Application.focusChanged += this.OnFocusChanged;
		this.winRect = new Rect(((float)Screen.width - this.debugWidth) / 2f, ((float)Screen.height - this.debugHeight) / 2f, this.debugWidth, this.debugHeight);
		clientApp.savePath = Paths.GetSavePath();
		clientApp.debugMode = true;
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x000132B3 File Offset: 0x000114B3
	public void OnFocusChanged(bool value)
	{
		MessageMag.Instance.Send(MessageName.MSG_APP_OnFocusChanged, new MessageData(value));
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x000BC838 File Offset: 0x000BAA38
	private static void InitVersion()
	{
		clientApp.VersionStr = Application.version;
		try
		{
			string path = Application.streamingAssetsPath + "/Version";
			if (File.Exists(path))
			{
				string[] array = File.ReadAllLines(path);
				clientApp.IsTestVersion = bool.Parse(array[0]);
				clientApp.VersionStr = array[1];
				GameWindowTitle.Inst.SetTitle("觅长生 " + clientApp.VersionStr);
				Debug.Log("游戏版本:" + clientApp.VersionStr);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("初始化版本信息时出错:" + ex.Message + "/n" + ex.StackTrace);
		}
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x060014E7 RID: 5351 RVA: 0x000132CA File Offset: 0x000114CA
	private Avatar Player
	{
		get
		{
			return Tools.instance.getPlayer();
		}
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x000132D6 File Offset: 0x000114D6
	private void OnGUI()
	{
		this.DebugGUI();
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x000132DE File Offset: 0x000114DE
	private void Update()
	{
		if (!ModVar.Enable && ModVar.closeNpcNaiYao)
		{
			ModVar.Enable = true;
		}
		if (Input.GetKey(306) && Input.GetKeyDown(116))
		{
			Debug.Log(UToolTip.debugData);
		}
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x000BC8E4 File Offset: 0x000BAAE4
	private string GetVersionStr()
	{
		this.flagStr1 = (ModVar.Enable ? "M" : "");
		this.flagStr2 = (this.IsDataError() ? "E" : "");
		return this.flagStr1 + this.flagStr2 + " EA版本:" + clientApp.VersionStr;
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x00013313 File Offset: 0x00011513
	private void ShowVersion()
	{
		GUI.Label(new Rect(10f, (float)(Screen.height - 30), 200f, 50f), this.GetVersionStr());
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x000BC940 File Offset: 0x000BAB40
	public void DebugGUI()
	{
		if (this.GUIcd < 0f)
		{
			if (clientApp.IsTestVersion && Input.GetKey(308) && clientApp.debugMode)
			{
				GUILayout.Window(11111, this.winRect, new GUI.WindowFunction(this.DebugWindow), "反馈bug时截图带上这里(修改档不要反馈bug)", Array.Empty<GUILayoutOption>());
			}
			else
			{
				this.ShowVersion();
			}
		}
		else
		{
			this.ShowVersion();
			this.GUIcd -= Time.deltaTime;
		}
		if (Mathf.Abs((float)Screen.height / (float)Screen.width - 0.5625f) > 0.001f)
		{
			GUI.Label(new Rect(10f, (float)(Screen.height - 50), 500f, 50f), string.Format("当前分辨率({0}x{1})不为16:9，可能会导致UI错误，建议调整到16:9再进行游戏", Screen.width, Screen.height));
		}
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x000BCA1C File Offset: 0x000BAC1C
	public bool IsDataError()
	{
		bool result = false;
		if (this.Player != null)
		{
			if (this.Player.shengShi >= 1000)
			{
				result = true;
			}
			if (this.Player.dunSu >= 1000)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x000BCA5C File Offset: 0x000BAC5C
	public void DebugWindow(int id)
	{
		GUILayout.Label(this.GetVersionStr() ?? "", Array.Empty<GUILayoutOption>());
		GUILayout.Label("Scene: " + SceneEx.NowSceneName, Array.Empty<GUILayoutOption>());
		GUILayout.Label(string.Format("SystemTime: {0}", DateTime.Now), Array.Empty<GUILayoutOption>());
		GUILayout.Label(string.Format("ScreenSize: {0}x{1}", Screen.width, Screen.height), Array.Empty<GUILayoutOption>());
		if (this.Player != null)
		{
			GUILayout.Label("WorldTime: " + this.Player.worldTimeMag.nowTime, Array.Empty<GUILayoutOption>());
			GUILayout.Label("PlayerName: " + this.Player.name, Array.Empty<GUILayoutOption>());
		}
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("打开存档文件夹", Array.Empty<GUILayoutOption>()))
		{
			this.GUIcd = 3f;
			this.OpenSaveDir();
		}
		if (clientApp.nowSaveing)
		{
			GUILayout.Label("正在压缩存档...", Array.Empty<GUILayoutOption>());
		}
		else if (this.IsDataError())
		{
			GUILayout.Label("数据异常，无法导出存档", Array.Empty<GUILayoutOption>());
		}
		else if (GUILayout.Button("导出存档到桌面", Array.Empty<GUILayoutOption>()))
		{
			clientApp.nowSaveing = true;
			string text = "";
			if (this.Player != null)
			{
				text = this.Player.name;
			}
			clientApp.zipOutpath = string.Concat(new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
				"/觅长生存档-",
				text,
				"-V",
				clientApp.VersionStr.Replace(".", "_"),
				"-",
				DateTime.Now.ToString("yyyyMMddHHmm"),
				".zip"
			});
			Loom.RunAsync(new Action(this.ZipSaveToDesktop));
		}
		if (this.logFileExists)
		{
			if (GUILayout.Button("导出报错到桌面", Array.Empty<GUILayoutOption>()))
			{
				this.SaveLogToDesktop();
			}
		}
		else
		{
			GUILayout.Label(this.logFileShow, Array.Empty<GUILayoutOption>());
			this.cd -= Time.deltaTime;
			if (this.cd < 0f)
			{
				this.logFileExists = true;
			}
		}
		GUILayout.EndHorizontal();
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x0001333D File Offset: 0x0001153D
	public void OpenSaveDir()
	{
		Process.Start(Paths.GetSavePath());
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000BCC9C File Offset: 0x000BAE9C
	public void ZipSaveToDesktop()
	{
		try
		{
			clientApp.CreateZipFile(clientApp.savePath, clientApp.zipOutpath);
		}
		catch
		{
		}
		clientApp.nowSaveing = false;
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000BCCD4 File Offset: 0x000BAED4
	public static void CreateZipFile(string filesPath, string zipFilePath)
	{
		if (!Directory.Exists(filesPath))
		{
			return;
		}
		ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(zipFilePath));
		zipOutputStream.SetLevel(1);
		byte[] array = new byte[4096];
		foreach (string text in Directory.GetFiles(filesPath, "*.*", SearchOption.AllDirectories))
		{
			zipOutputStream.PutNextEntry(new ZipEntry(text.Replace(filesPath, ""))
			{
				DateTime = DateTime.Now
			});
			using (FileStream fileStream = File.Open(text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				int num;
				do
				{
					num = fileStream.Read(array, 0, array.Length);
					zipOutputStream.Write(array, 0, num);
				}
				while (num > 0);
			}
		}
		zipOutputStream.Finish();
		zipOutputStream.Close();
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x000BCDA0 File Offset: 0x000BAFA0
	public void SaveLogToDesktop()
	{
		FileInfo fileInfo = new FileInfo(Paths.GetSavePath() + "/output_log.txt");
		if (fileInfo.Exists)
		{
			File.Copy(fileInfo.FullName, string.Concat(new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
				"/觅长生日志-V",
				Application.version.Replace(".", "_"),
				"-",
				DateTime.Now.ToString("yyyyMMddHHmm"),
				".txt"
			}), true);
			this.logFileShow = "日志已保存到桌面";
		}
		else
		{
			this.logFileShow = "暂未生成日志";
		}
		this.logFileExists = false;
		this.cd = 3f;
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000BCE58 File Offset: 0x000BB058
	public void ClearSaveKuoHao()
	{
		FileInfo[] files = new DirectoryInfo(Paths.GetSavePath()).GetFiles("*(*");
		for (int i = 0; i < files.Length; i++)
		{
			files[i].Delete();
		}
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x000BCE90 File Offset: 0x000BB090
	private void LogCallback(string condition, string stackTrace, LogType type)
	{
		if (type == null || type == 4)
		{
			string contents = string.Format("[{0}]发起者:\n{1}内容:{2}\n\n", DateTime.Now, stackTrace, condition);
			if (!File.Exists(Paths.GetSavePath() + "/errorlog.log"))
			{
				File.WriteAllText(Paths.GetSavePath() + "/errorlog.log", "");
			}
			File.AppendAllText(Paths.GetSavePath() + "/errorlog.log", contents);
		}
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x0001334A File Offset: 0x0001154A
	public void DestoryTalk(GameObject obj)
	{
		this.waitToDestory.Enqueue(obj);
		base.Invoke("FDestory", 1f);
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x000BCF00 File Offset: 0x000BB100
	private void FDestory()
	{
		if (this.waitToDestory.Count > 0)
		{
			GameObject gameObject = this.waitToDestory.Dequeue();
			if (gameObject != null)
			{
				Object.Destroy(gameObject);
			}
		}
	}

	// Token: 0x0400100D RID: 4109
	public static clientApp Inst;

	// Token: 0x0400100E RID: 4110
	public static string VersionStr;

	// Token: 0x0400100F RID: 4111
	public static bool IsTestVersion;

	// Token: 0x04001010 RID: 4112
	public static bool debugMode;

	// Token: 0x04001011 RID: 4113
	private static string savePath;

	// Token: 0x04001012 RID: 4114
	private static string zipOutpath;

	// Token: 0x04001013 RID: 4115
	private static bool nowSaveing;

	// Token: 0x04001014 RID: 4116
	private float debugWidth = 400f;

	// Token: 0x04001015 RID: 4117
	private float debugHeight = 120f;

	// Token: 0x04001016 RID: 4118
	private Rect winRect;

	// Token: 0x04001017 RID: 4119
	private string logFileShow = "";

	// Token: 0x04001018 RID: 4120
	private bool logFileExists = true;

	// Token: 0x04001019 RID: 4121
	private float cd;

	// Token: 0x0400101A RID: 4122
	private float GUIcd;

	// Token: 0x0400101B RID: 4123
	private string flagStr1;

	// Token: 0x0400101C RID: 4124
	private string flagStr2;

	// Token: 0x0400101D RID: 4125
	private Queue<GameObject> waitToDestory = new Queue<GameObject>();
}
