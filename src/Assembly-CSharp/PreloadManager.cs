using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

// Token: 0x0200053D RID: 1341
public class PreloadManager : MonoBehaviour
{
	// Token: 0x06002239 RID: 8761 RVA: 0x0001C107 File Offset: 0x0001A307
	private void Awake()
	{
		PreloadManager.Inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x0600223A RID: 8762 RVA: 0x0001C11A File Offset: 0x0001A31A
	private void Start()
	{
		this.Init();
	}

	// Token: 0x0600223B RID: 8763 RVA: 0x0011AF8C File Offset: 0x0011918C
	private void Update()
	{
		if (!this.PreloadFinished)
		{
			bool flag = true;
			int i = 0;
			while (i < PreloadManager.preloadTasks.Count)
			{
				PreloadTask preloadTask = PreloadManager.preloadTasks[i];
				if (!preloadTask.IsDone)
				{
					flag = false;
					if (!preloadTask.IsStart)
					{
						preloadTask.IsStart = true;
						Debug.Log(string.Format("初始化任务{0} {1} 开始 {2}", i, preloadTask.Name, DateTime.Now));
						preloadTask.Start(i);
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			if (flag)
			{
				this.PreloadFinished = true;
				if (PreloadManager.IsException)
				{
					Debug.LogError("----------------------------------------");
					Debug.LogError("初始化时的错误:");
					Debug.LogError(PreloadManager.ExceptionData);
					Debug.LogError("----------------------------------------");
					return;
				}
				MessageMag.Instance.Send(MessageName.MSG_PreloadFinish, null);
			}
		}
	}

	// Token: 0x0600223C RID: 8764 RVA: 0x0011B060 File Offset: 0x00119260
	private void OnGUI()
	{
		if (PreloadManager.IsException)
		{
			GUI.contentColor = Color.red;
			GUILayout.Label("", Array.Empty<GUILayoutOption>());
			GUILayout.Label("游戏数据初始化时出现异常，请检查文件完整性，如安装了Mod，请检查Mod是否损坏或者有新的更新", Array.Empty<GUILayoutOption>());
			GUILayout.Label("异常信息:\n" + PreloadManager.ExceptionData, Array.Empty<GUILayoutOption>());
		}
	}

	// Token: 0x0600223D RID: 8765 RVA: 0x0001C122 File Offset: 0x0001A322
	public static void LogException(string log)
	{
		PreloadManager.IsException = true;
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(log);
		Console.ForegroundColor = ConsoleColor.White;
		PreloadManager.ExceptionData = PreloadManager.ExceptionData + log + "\n";
	}

	// Token: 0x0600223E RID: 8766 RVA: 0x0011B0B8 File Offset: 0x001192B8
	public void Init()
	{
		this.CreateTask("jsonData初始化", new Action<int>(jsonData.instance.Preload));
		this.CreateTask("SkillDatebase初始化", new Action<int>(SkillDatebase.instence.Preload));
		this.CreateTask("SkillStaticDatebase初始化", new Action<int>(SkillStaticDatebase.instence.Preload));
		this.CreateTask("ItemDatebase初始化", new Action<int>(ItemDatebase.Inst.Preload));
		this.CreateTask("数据粗略查错", new Action<int>(CheckData.CheckTask));
		this.CreateTask("ResManager初始化", new Action<int>(ResManager.inst.Preload));
	}

	// Token: 0x0600223F RID: 8767 RVA: 0x0011B164 File Offset: 0x00119364
	private void CreateTask(string taskName, Action<int> preload)
	{
		PreloadTask preloadTask = new PreloadTask();
		preloadTask.Name = taskName;
		preloadTask.Start = preload;
		PreloadManager.preloadTasks.Add(preloadTask);
	}

	// Token: 0x06002240 RID: 8768 RVA: 0x0011B190 File Offset: 0x00119390
	public void TaskDone(int taskID)
	{
		PreloadManager.preloadTasks[taskID].IsDone = true;
		Console.WriteLine(string.Format("初始化任务{0} {1} 完成 {2}", taskID, PreloadManager.preloadTasks[taskID].Name, DateTime.Now));
		Debug.Log(string.Format("初始化任务{0} {1} 完成 {2}", taskID, PreloadManager.preloadTasks[taskID].Name, DateTime.Now));
	}

	// Token: 0x04001D9B RID: 7579
	public static PreloadManager Inst;

	// Token: 0x04001D9C RID: 7580
	public static bool IsException;

	// Token: 0x04001D9D RID: 7581
	public static string ExceptionData = "";

	// Token: 0x04001D9E RID: 7582
	public static List<PreloadTask> preloadTasks = new List<PreloadTask>();

	// Token: 0x04001D9F RID: 7583
	public bool PreloadFinished;
}
