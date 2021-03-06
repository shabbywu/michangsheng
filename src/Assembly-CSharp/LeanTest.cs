using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class LeanTest
{
	// Token: 0x06000078 RID: 120 RVA: 0x00004419 File Offset: 0x00002619
	public static void debug(string name, bool didPass, string failExplaination = null)
	{
		LeanTest.expect(didPass, name, failExplaination);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0005E950 File Offset: 0x0005CB50
	public static void expect(bool didPass, string definition, string failExplaination = null)
	{
		float num = LeanTest.printOutLength(definition);
		int totalWidth = 40 - (int)(num * 1.05f);
		string text = "".PadRight(totalWidth, "_"[0]);
		string text2 = string.Concat(new string[]
		{
			LeanTest.formatB(definition),
			" ",
			text,
			" [ ",
			didPass ? LeanTest.formatC("pass", "green") : LeanTest.formatC("fail", "red"),
			" ]"
		});
		if (!didPass && failExplaination != null)
		{
			text2 = text2 + " - " + failExplaination;
		}
		Debug.Log(text2);
		if (didPass)
		{
			LeanTest.passes++;
		}
		LeanTest.tests++;
		if (LeanTest.tests == LeanTest.expected && !LeanTest.testsFinished)
		{
			LeanTest.overview();
		}
		else if (LeanTest.tests > LeanTest.expected)
		{
			Debug.Log(LeanTest.formatB("Too many tests for a final report!") + " set LeanTest.expected = " + LeanTest.tests);
		}
		if (!LeanTest.timeoutStarted)
		{
			LeanTest.timeoutStarted = true;
			GameObject gameObject = new GameObject();
			gameObject.name = "~LeanTest";
			(gameObject.AddComponent(typeof(LeanTester)) as LeanTester).timeout = LeanTest.timeout;
			gameObject.hideFlags = 61;
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x0005EAA0 File Offset: 0x0005CCA0
	public static string padRight(int len)
	{
		string text = "";
		for (int i = 0; i < len; i++)
		{
			text += "_";
		}
		return text;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0005EACC File Offset: 0x0005CCCC
	public static float printOutLength(string str)
	{
		float num = 0f;
		for (int i = 0; i < str.Length; i++)
		{
			if (str[i] == "I"[0])
			{
				num += 0.5f;
			}
			else if (str[i] == "J"[0])
			{
				num += 0.85f;
			}
			else
			{
				num += 1f;
			}
		}
		return num;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00004423 File Offset: 0x00002623
	public static string formatBC(string str, string color)
	{
		return LeanTest.formatC(LeanTest.formatB(str), color);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00004431 File Offset: 0x00002631
	public static string formatB(string str)
	{
		return "<b>" + str + "</b>";
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00004443 File Offset: 0x00002643
	public static string formatC(string str, string color)
	{
		return string.Concat(new string[]
		{
			"<color=",
			color,
			">",
			str,
			"</color>"
		});
	}

	// Token: 0x0600007F RID: 127 RVA: 0x0005EB38 File Offset: 0x0005CD38
	public static void overview()
	{
		LeanTest.testsFinished = true;
		int num = LeanTest.expected - LeanTest.passes;
		string text = (num > 0) ? LeanTest.formatBC(string.Concat(num), "red") : string.Concat(num);
		Debug.Log(string.Concat(new string[]
		{
			LeanTest.formatB("Final Report:"),
			" _____________________ PASSED: ",
			LeanTest.formatBC(string.Concat(LeanTest.passes), "green"),
			" FAILED: ",
			text,
			" "
		}));
	}

	// Token: 0x04000074 RID: 116
	public static int expected = 0;

	// Token: 0x04000075 RID: 117
	private static int tests = 0;

	// Token: 0x04000076 RID: 118
	private static int passes = 0;

	// Token: 0x04000077 RID: 119
	public static float timeout = 15f;

	// Token: 0x04000078 RID: 120
	public static bool timeoutStarted = false;

	// Token: 0x04000079 RID: 121
	public static bool testsFinished = false;
}
