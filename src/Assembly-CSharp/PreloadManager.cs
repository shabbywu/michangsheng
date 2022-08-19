using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

// Token: 0x020003B2 RID: 946
public class PreloadManager : MonoBehaviour
{
	// Token: 0x06001EB6 RID: 7862 RVA: 0x000D7B02 File Offset: 0x000D5D02
	private void Awake()
	{
		PreloadManager.Inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06001EB7 RID: 7863 RVA: 0x000D7B15 File Offset: 0x000D5D15
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x000D7B20 File Offset: 0x000D5D20
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
					MessageMag.Instance.Send(MessageName.MSG_PreloadFinish, null);
					return;
				}
				MessageMag.Instance.Send(MessageName.MSG_PreloadFinish, null);
			}
		}
	}

	// Token: 0x06001EB9 RID: 7865 RVA: 0x000D7C04 File Offset: 0x000D5E04
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

	// Token: 0x06001EBA RID: 7866 RVA: 0x000D7C59 File Offset: 0x000D5E59
	public static void LogException(string log)
	{
		PreloadManager.IsException = true;
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(log);
		Console.ForegroundColor = ConsoleColor.White;
		PreloadManager.ExceptionData = PreloadManager.ExceptionData + log + "\n";
	}

	// Token: 0x06001EBB RID: 7867 RVA: 0x000D7C8C File Offset: 0x000D5E8C
	public void Init()
	{
		this.CreateTask("jsonData初始化", new Action<int>(jsonData.instance.Preload));
		this.CreateTask("SkillDatebase初始化", new Action<int>(SkillDatebase.instence.Preload));
		this.CreateTask("SkillStaticDatebase初始化", new Action<int>(SkillStaticDatebase.instence.Preload));
		this.CreateTask("ItemDatebase初始化", new Action<int>(ItemDatebase.Inst.Preload));
		this.CreateTask("数据粗略查错", new Action<int>(CheckData.CheckTask));
		this.CreateTask("ResManager初始化", new Action<int>(ResManager.inst.Preload));
	}

	// Token: 0x06001EBC RID: 7868 RVA: 0x000D7D38 File Offset: 0x000D5F38
	private void CreateTask(string taskName, Action<int> preload)
	{
		PreloadTask preloadTask = new PreloadTask();
		preloadTask.Name = taskName;
		preloadTask.Start = preload;
		PreloadManager.preloadTasks.Add(preloadTask);
	}

	// Token: 0x06001EBD RID: 7869 RVA: 0x000D7D64 File Offset: 0x000D5F64
	public void TaskDone(int taskID)
	{
		PreloadManager.preloadTasks[taskID].IsDone = true;
		Console.WriteLine(string.Format("初始化任务{0} {1} 完成 {2}", taskID, PreloadManager.preloadTasks[taskID].Name, DateTime.Now));
		Debug.Log(string.Format("初始化任务{0} {1} 完成 {2}", taskID, PreloadManager.preloadTasks[taskID].Name, DateTime.Now));
	}

	// Token: 0x04001927 RID: 6439
	public static PreloadManager Inst;

	// Token: 0x04001928 RID: 6440
	public static bool IsException;

	// Token: 0x04001929 RID: 6441
	public static string ExceptionData = "";

	// Token: 0x0400192A RID: 6442
	public static List<PreloadTask> preloadTasks = new List<PreloadTask>();

	// Token: 0x0400192B RID: 6443
	public bool PreloadFinished;
}
