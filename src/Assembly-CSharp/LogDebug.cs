using System;
using System.Text;
using UnityEngine;

// Token: 0x0200041E RID: 1054
public class LogDebug : MonoBehaviour
{
	// Token: 0x060021D4 RID: 8660 RVA: 0x000E9CB0 File Offset: 0x000E7EB0
	private void Awake()
	{
		Application.logMessageReceived += new Application.LogCallback(this.HandleLog);
	}

	// Token: 0x060021D5 RID: 8661 RVA: 0x000E9CC3 File Offset: 0x000E7EC3
	private void Start()
	{
		Debug.LogError("Test...");
	}

	// Token: 0x060021D6 RID: 8662 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x060021D7 RID: 8663 RVA: 0x000E9CCF File Offset: 0x000E7ECF
	private void OnGUI()
	{
		GUILayout.Label(this.logBuilder.ToString(), Array.Empty<GUILayoutOption>());
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x000E9CE8 File Offset: 0x000E7EE8
	private void HandleLog(string condition, string stackTrace, LogType type)
	{
		if (type == 3 || type == null || type == 4)
		{
			string message = string.Format("condition = {0} \n stackTrace = {1} \n type = {2}", condition, stackTrace, type);
			this.SendLog(message);
		}
	}

	// Token: 0x060021D9 RID: 8665 RVA: 0x000E9D1A File Offset: 0x000E7F1A
	private void SendLog(string message)
	{
		if (this.num > 5)
		{
			this.logBuilder.Clear();
			this.num = 0;
		}
		this.num++;
		this.logBuilder.Append(message);
	}

	// Token: 0x04001B4F RID: 6991
	private StringBuilder logBuilder = new StringBuilder();

	// Token: 0x04001B50 RID: 6992
	public int num;
}
