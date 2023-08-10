using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

public class PreloadManager : MonoBehaviour
{
	public static PreloadManager Inst;

	public static bool IsException;

	public static string ExceptionData = "";

	public static List<PreloadTask> preloadTasks = new List<PreloadTask>();

	public bool PreloadFinished;

	private void Awake()
	{
		Inst = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
	}

	private void Start()
	{
		Init();
	}

	private void Update()
	{
		if (PreloadFinished)
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < preloadTasks.Count; i++)
		{
			PreloadTask preloadTask = preloadTasks[i];
			if (!preloadTask.IsDone)
			{
				flag = false;
				if (!preloadTask.IsStart)
				{
					preloadTask.IsStart = true;
					Debug.Log((object)$"初始化任务{i} {preloadTask.Name} 开始 {DateTime.Now}");
					preloadTask.Start(i);
				}
				break;
			}
		}
		if (flag)
		{
			PreloadFinished = true;
			if (IsException)
			{
				Debug.LogError((object)"----------------------------------------");
				Debug.LogError((object)"初始化时的错误:");
				Debug.LogError((object)ExceptionData);
				Debug.LogError((object)"----------------------------------------");
				MessageMag.Instance.Send(MessageName.MSG_PreloadFinish);
			}
			else
			{
				MessageMag.Instance.Send(MessageName.MSG_PreloadFinish);
			}
		}
	}

	private void OnGUI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if (IsException)
		{
			GUI.contentColor = Color.red;
			GUILayout.Label("", Array.Empty<GUILayoutOption>());
			GUILayout.Label("游戏数据初始化时出现异常，请检查文件完整性，如安装了Mod，请检查Mod是否损坏或者有新的更新", Array.Empty<GUILayoutOption>());
			GUILayout.Label("异常信息:\n" + ExceptionData, Array.Empty<GUILayoutOption>());
		}
	}

	public static void LogException(string log)
	{
		IsException = true;
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(log);
		Console.ForegroundColor = ConsoleColor.White;
		ExceptionData = ExceptionData + log + "\n";
	}

	public void Init()
	{
		CreateTask("jsonData初始化", jsonData.instance.Preload);
		CreateTask("SkillDatebase初始化", SkillDatebase.instence.Preload);
		CreateTask("SkillStaticDatebase初始化", SkillStaticDatebase.instence.Preload);
		CreateTask("ItemDatebase初始化", ItemDatebase.Inst.Preload);
		CreateTask("数据粗略查错", CheckData.CheckTask);
		CreateTask("ResManager初始化", ResManager.inst.Preload);
	}

	private void CreateTask(string taskName, Action<int> preload)
	{
		PreloadTask preloadTask = new PreloadTask();
		preloadTask.Name = taskName;
		preloadTask.Start = preload;
		preloadTasks.Add(preloadTask);
	}

	public void TaskDone(int taskID)
	{
		preloadTasks[taskID].IsDone = true;
		Console.WriteLine($"初始化任务{taskID} {preloadTasks[taskID].Name} 完成 {DateTime.Now}");
		Debug.Log((object)$"初始化任务{taskID} {preloadTasks[taskID].Name} 完成 {DateTime.Now}");
	}
}
