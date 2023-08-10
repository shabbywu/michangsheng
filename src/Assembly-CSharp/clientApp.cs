using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using KBEngine;
using UnityEngine;

public class clientApp : KBEMain
{
	public static clientApp Inst;

	public static string VersionStr;

	public static bool IsTestVersion;

	public static bool IsNewSaveVersion;

	public static string dataPath;

	public static string persistentDataPath;

	public static bool debugMode;

	private static string savePath;

	private static string zipOutpath;

	private static bool nowSaveing;

	private float debugWidth = 400f;

	private float debugHeight = 120f;

	private Rect winRect;

	private string logFileShow = "";

	private bool logFileExists = true;

	private float cd;

	private float GUIcd;

	private string flagStr1;

	private string flagStr2;

	private Queue<GameObject> waitToDestory = new Queue<GameObject>();

	private Avatar Player => Tools.instance.getPlayer();

	protected override void Awake()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		InitVersion();
		dataPath = Application.dataPath;
		persistentDataPath = Application.persistentDataPath;
		Debug.Log((object)$"clientApp.Awake:{DateTime.Now}");
		Inst = this;
		base.Awake();
		Application.logMessageReceived += new LogCallback(LogCallback);
		Application.focusChanged += OnFocusChanged;
		winRect = new Rect(((float)Screen.width - debugWidth) / 2f, ((float)Screen.height - debugHeight) / 2f, debugWidth, debugHeight);
		savePath = Paths.GetNewSavePath();
		debugMode = true;
	}

	public void OnFocusChanged(bool value)
	{
		MessageMag.Instance.Send(MessageName.MSG_APP_OnFocusChanged, new MessageData(value));
	}

	private static void InitVersion()
	{
		VersionStr = Application.version;
		try
		{
			string path = Application.streamingAssetsPath + "/Version";
			if (File.Exists(path))
			{
				string[] array = File.ReadAllLines(path);
				IsTestVersion = bool.Parse(array[0]);
				VersionStr = array[1];
				GameWindowTitle.Inst.SetTitle("觅长生 " + VersionStr);
				Debug.Log((object)("游戏版本:" + VersionStr));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("初始化版本信息时出错:" + ex.Message + "/n" + ex.StackTrace));
		}
	}

	private void OnGUI()
	{
		DebugGUI();
	}

	private void Update()
	{
		if (!ModVar.Enable && ModVar.closeNpcNaiYao)
		{
			ModVar.Enable = true;
		}
		if (Input.GetKey((KeyCode)306) && Input.GetKeyDown((KeyCode)116))
		{
			Debug.Log((object)UToolTip.debugData);
		}
		if (Input.GetKey((KeyCode)306) && Input.GetKeyDown((KeyCode)101))
		{
			PlayerEx.DeleteErrorItem();
		}
	}

	protected override void OnDestroy()
	{
	}

	private string GetVersionStr()
	{
		flagStr1 = (ModVar.Enable ? "M" : "");
		flagStr2 = (IsDataError() ? "E" : "");
		return flagStr1 + flagStr2 + " EA版本:" + VersionStr;
	}

	private void ShowVersion()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		GUI.Label(new Rect(10f, (float)(Screen.height - 30), 200f, 50f), GetVersionStr());
	}

	public void DebugGUI()
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (GUIcd < 0f)
		{
			if (IsTestVersion && Input.GetKey((KeyCode)308) && debugMode)
			{
				GUILayout.Window(11111, winRect, new WindowFunction(DebugWindow), "反馈bug时截图带上这里(修改档不要反馈bug)", Array.Empty<GUILayoutOption>());
			}
			else
			{
				ShowVersion();
			}
		}
		else
		{
			ShowVersion();
			GUIcd -= Time.deltaTime;
		}
		if (Mathf.Abs((float)Screen.height / (float)Screen.width - 0.5625f) > 0.001f)
		{
			GUI.Label(new Rect(10f, (float)(Screen.height - 50), 500f, 50f), $"当前分辨率({Screen.width}x{Screen.height})不为16:9，可能会导致UI错误，建议调整到16:9再进行游戏");
		}
	}

	public bool IsDataError()
	{
		bool result = false;
		if (Player != null)
		{
			if (Player.shengShi >= 1000)
			{
				result = true;
			}
			if (Player.dunSu >= 1000)
			{
				result = true;
			}
		}
		return result;
	}

	public void DebugWindow(int id)
	{
		GUILayout.Label(GetVersionStr() ?? "", Array.Empty<GUILayoutOption>());
		GUILayout.Label("Scene: " + SceneEx.NowSceneName, Array.Empty<GUILayoutOption>());
		GUILayout.Label($"SystemTime: {DateTime.Now}", Array.Empty<GUILayoutOption>());
		GUILayout.Label($"ScreenSize: {Screen.width}x{Screen.height}", Array.Empty<GUILayoutOption>());
		if (Player != null)
		{
			GUILayout.Label("WorldTime: " + Player.worldTimeMag.nowTime, Array.Empty<GUILayoutOption>());
			GUILayout.Label("PlayerName: " + Player.name, Array.Empty<GUILayoutOption>());
		}
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("打开存档文件夹", Array.Empty<GUILayoutOption>()))
		{
			GUIcd = 3f;
			OpenSaveDir();
		}
		if (nowSaveing)
		{
			GUILayout.Label("正在压缩存档...", Array.Empty<GUILayoutOption>());
		}
		else if (IsDataError())
		{
			GUILayout.Label("数据异常，无法导出存档", Array.Empty<GUILayoutOption>());
		}
		else if (GUILayout.Button("导出存档到桌面", Array.Empty<GUILayoutOption>()))
		{
			nowSaveing = true;
			string text = "";
			if (Player != null)
			{
				text = Player.name;
			}
			zipOutpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/觅长生存档-" + text + "-V" + VersionStr.Replace(".", "_") + "-" + DateTime.Now.ToString("yyyyMMddHHmm") + ".zip";
			Loom.RunAsync(ZipSaveToDesktop);
		}
		if (logFileExists)
		{
			if (GUILayout.Button("导出报错到桌面", Array.Empty<GUILayoutOption>()))
			{
				SaveLogToDesktop();
			}
		}
		else
		{
			GUILayout.Label(logFileShow, Array.Empty<GUILayoutOption>());
			cd -= Time.deltaTime;
			if (cd < 0f)
			{
				logFileExists = true;
			}
		}
		GUILayout.EndHorizontal();
	}

	public void OpenSaveDir()
	{
		Process.Start(Paths.GetNewSavePath());
	}

	public void ZipSaveToDesktop()
	{
		try
		{
			YSZip.ZipFile(savePath, zipOutpath);
		}
		catch
		{
		}
		nowSaveing = false;
	}

	public static void CreateZipFile(string filesPath, string zipFilePath)
	{
		if (!Directory.Exists(filesPath))
		{
			return;
		}
		ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(zipFilePath));
		zipOutputStream.SetLevel(1);
		byte[] array = new byte[4096];
		string[] files = Directory.GetFiles(filesPath, "*.*", SearchOption.AllDirectories);
		foreach (string obj in files)
		{
			zipOutputStream.PutNextEntry(new ZipEntry(obj.Replace(filesPath, ""))
			{
				DateTime = DateTime.Now
			});
			using FileStream fileStream = File.Open(obj, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			int num;
			do
			{
				num = fileStream.Read(array, 0, array.Length);
				zipOutputStream.Write(array, 0, num);
			}
			while (num > 0);
		}
		zipOutputStream.Finish();
		zipOutputStream.Close();
	}

	public void SaveLogToDesktop()
	{
		FileInfo fileInfo = new FileInfo(Paths.GetSavePath() + "/output_log.txt");
		if (fileInfo.Exists)
		{
			File.Copy(fileInfo.FullName, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/觅长生日志-V" + Application.version.Replace(".", "_") + "-" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt", overwrite: true);
			logFileShow = "日志已保存到桌面";
		}
		else
		{
			logFileShow = "暂未生成日志";
		}
		logFileExists = false;
		cd = 3f;
	}

	private void LogCallback(string condition, string stackTrace, LogType type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Invalid comparison between Unknown and I4
		if ((int)type == 0 || (int)type == 4)
		{
			string contents = $"[{DateTime.Now}]发起者:\n{stackTrace}内容:{condition}\n\n";
			if (!File.Exists(Paths.GetNewSavePath() + "/errorlog.log"))
			{
				File.WriteAllText(Paths.GetNewSavePath() + "/errorlog.log", "");
			}
			File.AppendAllText(Paths.GetNewSavePath() + "/errorlog.log", contents);
		}
	}

	public void DestoryTalk(GameObject obj)
	{
		waitToDestory.Enqueue(obj);
		((MonoBehaviour)this).Invoke("FDestory", 1f);
	}

	private void FDestory()
	{
		if (waitToDestory.Count > 0)
		{
			GameObject val = waitToDestory.Dequeue();
			if ((Object)(object)val != (Object)null)
			{
				Object.Destroy((Object)(object)val);
			}
		}
	}
}
