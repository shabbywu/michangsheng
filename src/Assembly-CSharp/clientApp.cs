using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using KBEngine;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public class clientApp : KBEMain
{
	// Token: 0x06001239 RID: 4665 RVA: 0x0006EAD0 File Offset: 0x0006CCD0
	protected override void Awake()
	{
		clientApp.InitVersion();
		clientApp.dataPath = Application.dataPath;
		clientApp.persistentDataPath = Application.persistentDataPath;
		Debug.Log(string.Format("clientApp.Awake:{0}", DateTime.Now));
		clientApp.Inst = this;
		base.Awake();
		Application.logMessageReceived += new Application.LogCallback(this.LogCallback);
		Application.focusChanged += this.OnFocusChanged;
		this.winRect = new Rect(((float)Screen.width - this.debugWidth) / 2f, ((float)Screen.height - this.debugHeight) / 2f, this.debugWidth, this.debugHeight);
		clientApp.savePath = Paths.GetNewSavePath();
		clientApp.debugMode = true;
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x0006EB8A File Offset: 0x0006CD8A
	public void OnFocusChanged(bool value)
	{
		MessageMag.Instance.Send(MessageName.MSG_APP_OnFocusChanged, new MessageData(value));
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x0006EBA4 File Offset: 0x0006CDA4
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

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x0600123C RID: 4668 RVA: 0x0006EC50 File Offset: 0x0006CE50
	private Avatar Player
	{
		get
		{
			return Tools.instance.getPlayer();
		}
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x0006EC5C File Offset: 0x0006CE5C
	private void OnGUI()
	{
		this.DebugGUI();
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x0006EC64 File Offset: 0x0006CE64
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
		if (Input.GetKey(306) && Input.GetKeyDown(101))
		{
			PlayerEx.DeleteErrorItem();
		}
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x00004095 File Offset: 0x00002295
	protected override void OnDestroy()
	{
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x0006ECC0 File Offset: 0x0006CEC0
	private string GetVersionStr()
	{
		this.flagStr1 = (ModVar.Enable ? "M" : "");
		this.flagStr2 = (this.IsDataError() ? "E" : "");
		return this.flagStr1 + this.flagStr2 + " EA版本:" + clientApp.VersionStr;
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x0006ED1B File Offset: 0x0006CF1B
	private void ShowVersion()
	{
		GUI.Label(new Rect(10f, (float)(Screen.height - 30), 200f, 50f), this.GetVersionStr());
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x0006ED48 File Offset: 0x0006CF48
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

	// Token: 0x06001243 RID: 4675 RVA: 0x0006EE24 File Offset: 0x0006D024
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

	// Token: 0x06001244 RID: 4676 RVA: 0x0006EE64 File Offset: 0x0006D064
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

	// Token: 0x06001245 RID: 4677 RVA: 0x0005F6D7 File Offset: 0x0005D8D7
	public void OpenSaveDir()
	{
		Process.Start(Paths.GetNewSavePath());
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x0006F0A4 File Offset: 0x0006D2A4
	public void ZipSaveToDesktop()
	{
		try
		{
			YSZip.ZipFile(clientApp.savePath, clientApp.zipOutpath);
		}
		catch
		{
		}
		clientApp.nowSaveing = false;
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x0006F0DC File Offset: 0x0006D2DC
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

	// Token: 0x06001248 RID: 4680 RVA: 0x0006F1A8 File Offset: 0x0006D3A8
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

	// Token: 0x06001249 RID: 4681 RVA: 0x0006F260 File Offset: 0x0006D460
	private void LogCallback(string condition, string stackTrace, LogType type)
	{
		if (type == null || type == 4)
		{
			string contents = string.Format("[{0}]发起者:\n{1}内容:{2}\n\n", DateTime.Now, stackTrace, condition);
			if (!File.Exists(Paths.GetNewSavePath() + "/errorlog.log"))
			{
				File.WriteAllText(Paths.GetNewSavePath() + "/errorlog.log", "");
			}
			File.AppendAllText(Paths.GetNewSavePath() + "/errorlog.log", contents);
		}
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x0006F2CF File Offset: 0x0006D4CF
	public void DestoryTalk(GameObject obj)
	{
		this.waitToDestory.Enqueue(obj);
		base.Invoke("FDestory", 1f);
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x0006F2F0 File Offset: 0x0006D4F0
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

	// Token: 0x04000CE2 RID: 3298
	public static clientApp Inst;

	// Token: 0x04000CE3 RID: 3299
	public static string VersionStr;

	// Token: 0x04000CE4 RID: 3300
	public static bool IsTestVersion;

	// Token: 0x04000CE5 RID: 3301
	public static bool IsNewSaveVersion;

	// Token: 0x04000CE6 RID: 3302
	public static string dataPath;

	// Token: 0x04000CE7 RID: 3303
	public static string persistentDataPath;

	// Token: 0x04000CE8 RID: 3304
	public static bool debugMode;

	// Token: 0x04000CE9 RID: 3305
	private static string savePath;

	// Token: 0x04000CEA RID: 3306
	private static string zipOutpath;

	// Token: 0x04000CEB RID: 3307
	private static bool nowSaveing;

	// Token: 0x04000CEC RID: 3308
	private float debugWidth = 400f;

	// Token: 0x04000CED RID: 3309
	private float debugHeight = 120f;

	// Token: 0x04000CEE RID: 3310
	private Rect winRect;

	// Token: 0x04000CEF RID: 3311
	private string logFileShow = "";

	// Token: 0x04000CF0 RID: 3312
	private bool logFileExists = true;

	// Token: 0x04000CF1 RID: 3313
	private float cd;

	// Token: 0x04000CF2 RID: 3314
	private float GUIcd;

	// Token: 0x04000CF3 RID: 3315
	private string flagStr1;

	// Token: 0x04000CF4 RID: 3316
	private string flagStr2;

	// Token: 0x04000CF5 RID: 3317
	private Queue<GameObject> waitToDestory = new Queue<GameObject>();
}
