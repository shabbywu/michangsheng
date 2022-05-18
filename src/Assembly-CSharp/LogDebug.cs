using System;
using System.Text;
using UnityEngine;

// Token: 0x020005D2 RID: 1490
public class LogDebug : MonoBehaviour
{
	// Token: 0x0600258E RID: 9614 RVA: 0x0001E132 File Offset: 0x0001C332
	private void Awake()
	{
		Application.logMessageReceived += new Application.LogCallback(this.HandleLog);
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x0001E145 File Offset: 0x0001C345
	private void Start()
	{
		Debug.LogError("Test...");
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x06002591 RID: 9617 RVA: 0x0001E151 File Offset: 0x0001C351
	private void OnGUI()
	{
		GUILayout.Label(this.logBuilder.ToString(), Array.Empty<GUILayoutOption>());
	}

	// Token: 0x06002592 RID: 9618 RVA: 0x0012B25C File Offset: 0x0012945C
	private void HandleLog(string condition, string stackTrace, LogType type)
	{
		if (type == 3 || type == null || type == 4)
		{
			string message = string.Format("condition = {0} \n stackTrace = {1} \n type = {2}", condition, stackTrace, type);
			this.SendLog(message);
		}
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x0001E168 File Offset: 0x0001C368
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

	// Token: 0x04002015 RID: 8213
	private StringBuilder logBuilder = new StringBuilder();

	// Token: 0x04002016 RID: 8214
	public int num;
}
