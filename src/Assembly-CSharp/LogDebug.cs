using System;
using System.Text;
using UnityEngine;

public class LogDebug : MonoBehaviour
{
	private StringBuilder logBuilder = new StringBuilder();

	public int num;

	private void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		Application.logMessageReceived += new LogCallback(HandleLog);
	}

	private void Start()
	{
		Debug.LogError((object)"Test...");
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
		GUILayout.Label(logBuilder.ToString(), Array.Empty<GUILayoutOption>());
	}

	private void HandleLog(string condition, string stackTrace, LogType type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Invalid comparison between Unknown and I4
		if ((int)type == 3 || (int)type == 0 || (int)type == 4)
		{
			string message = $"condition = {condition} \n stackTrace = {stackTrace} \n type = {type}";
			SendLog(message);
		}
	}

	private void SendLog(string message)
	{
		if (num > 5)
		{
			logBuilder.Clear();
			num = 0;
		}
		num++;
		logBuilder.Append(message);
	}
}
