using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000239 RID: 569
public class MessageBoxExample : MonoBehaviour
{
	// Token: 0x06001182 RID: 4482 RVA: 0x000ABE78 File Offset: 0x000AA078
	private void Start()
	{
		GameObject gameObject = GameObject.Find("Message Box");
		if (gameObject != null)
		{
			Object.Destroy(gameObject);
		}
		GameObject gameObject2 = GameObject.Find("Menu Box");
		if (gameObject2 != null)
		{
			Object.Destroy(gameObject2);
		}
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x00010F25 File Offset: 0x0000F125
	public void Test()
	{
		MessageBox.Show("Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, null, MessageBoxButtons.OK);
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x00010F35 File Offset: 0x0000F135
	public void TestWithTitle()
	{
		MessageBox.Show("This is a message with a title.", "Message Box Title", null, MessageBoxButtons.OK);
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x00010F49 File Offset: 0x0000F149
	public void TestWithCallback()
	{
		MessageBox.Show("This is a message with a callback.", "Message Box Callback Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.OK);
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x00010F7B File Offset: 0x0000F17B
	public void TestOKCancelButtons()
	{
		MessageBox.Show("Are you sure you wish to delete your save game?", "Delete Save", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.OKCancel);
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x00010FAD File Offset: 0x0000F1AD
	public void TestRetryCancelButtons()
	{
		MessageBox.Show("This is a message with a set of buttons selected.", "Message Box Buttons Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.RetryCancel);
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x00010FDF File Offset: 0x0000F1DF
	public void TestYesNoButtons()
	{
		MessageBox.Show("Give us five stars?", "Review Game", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.YesNo);
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x00011011 File Offset: 0x0000F211
	public void TestYesNoCancelButtons()
	{
		MessageBox.Show("This is a message with a set of buttons selected.", "Message Box Buttons Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.YesNoCancel);
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x00011043 File Offset: 0x0000F243
	public void TestAbortRetryIgnoreButtons()
	{
		MessageBox.Show("Not ready reading drive A", "Message Box Buttons Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.AbortRetryIgnore);
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x000ABEBC File Offset: 0x000AA0BC
	public void TestMenu5()
	{
		IEnumerable<string> options = new string[]
		{
			"Option 1\nOption description can go here.",
			"Option 2\nTwo",
			"Option 3\nThree",
			"Option 4\nFour",
			"Option 5\nFive of Nine?"
		};
		UnityAction[] array = new UnityAction[5];
		array[0] = delegate()
		{
			MessageBox.Show("You clicked on Option 1", null, null, MessageBoxButtons.OK);
		};
		array[1] = delegate()
		{
			MessageBox.Show("You clicked on Option 2", null, null, MessageBoxButtons.OK);
		};
		array[2] = delegate()
		{
			MessageBox.Show("You clicked on Option 3", null, null, MessageBoxButtons.OK);
		};
		array[3] = delegate()
		{
			MessageBox.Show("You clicked on Option 4", null, null, MessageBoxButtons.OK);
		};
		array[4] = delegate()
		{
			MessageBox.Show("You clicked on Option 5", null, null, MessageBoxButtons.OK);
		};
		MenuBox.Show(options, array, "");
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x000ABFB4 File Offset: 0x000AA1B4
	public void TestMenu10()
	{
		IEnumerable<string> options = new string[]
		{
			"Option 1",
			"Option 2",
			"Option 3",
			"Option 4",
			"Option 5",
			"Option 6",
			"Option 7",
			"Option 8",
			"Option 9",
			"Show an even bigger menu!"
		};
		UnityAction[] array = new UnityAction[10];
		array[0] = delegate()
		{
			MessageBox.Show("You clicked on Option 1", null, null, MessageBoxButtons.OK);
		};
		array[1] = delegate()
		{
			MessageBox.Show("You clicked on Option 2", null, null, MessageBoxButtons.OK);
		};
		array[2] = delegate()
		{
			MessageBox.Show("You clicked on Option 3", null, null, MessageBoxButtons.OK);
		};
		array[3] = delegate()
		{
			MessageBox.Show("You clicked on Option 4", null, null, MessageBoxButtons.OK);
		};
		array[4] = delegate()
		{
			MessageBox.Show("You clicked on Option 5", null, null, MessageBoxButtons.OK);
		};
		array[5] = delegate()
		{
			MessageBox.Show("You clicked on Option 6", null, null, MessageBoxButtons.OK);
		};
		array[6] = delegate()
		{
			MessageBox.Show("You clicked on Option 7", null, null, MessageBoxButtons.OK);
		};
		array[7] = delegate()
		{
			MessageBox.Show("You clicked on Option 8", null, null, MessageBoxButtons.OK);
		};
		array[8] = delegate()
		{
			MessageBox.Show("You clicked on Option 9", null, null, MessageBoxButtons.OK);
		};
		array[9] = new UnityAction(this.TestMenu15);
		MenuBox.Show(options, array, "Ten Item Test Menu");
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x000AC170 File Offset: 0x000AA370
	public void TestMenu15()
	{
		IEnumerable<string> options = new string[]
		{
			"Option 1",
			"Option 2",
			"Option 3",
			"Option 4",
			"Option 5",
			"Option 6",
			"Option 7",
			"Option 8",
			"Option 9",
			"Option 10",
			"Option 11",
			"Option 12",
			"Option 13",
			"Option 14",
			"Option 15"
		};
		UnityAction[] array = new UnityAction[15];
		array[0] = delegate()
		{
			MessageBox.Show("You clicked on Option 1", null, null, MessageBoxButtons.OK);
		};
		array[1] = delegate()
		{
			MessageBox.Show("You clicked on Option 2", null, null, MessageBoxButtons.OK);
		};
		array[2] = delegate()
		{
			MessageBox.Show("You clicked on Option 3", null, null, MessageBoxButtons.OK);
		};
		array[3] = delegate()
		{
			MessageBox.Show("You clicked on Option 4", null, null, MessageBoxButtons.OK);
		};
		array[4] = delegate()
		{
			MessageBox.Show("You clicked on Option 5", null, null, MessageBoxButtons.OK);
		};
		array[5] = delegate()
		{
			MessageBox.Show("You clicked on Option 6", null, null, MessageBoxButtons.OK);
		};
		array[6] = delegate()
		{
			MessageBox.Show("You clicked on Option 7", null, null, MessageBoxButtons.OK);
		};
		array[7] = delegate()
		{
			MessageBox.Show("You clicked on Option 8", null, null, MessageBoxButtons.OK);
		};
		array[8] = delegate()
		{
			MessageBox.Show("You clicked on Option 9", null, null, MessageBoxButtons.OK);
		};
		array[9] = delegate()
		{
			MessageBox.Show("You clicked on Option 10", null, null, MessageBoxButtons.OK);
		};
		array[10] = delegate()
		{
			MessageBox.Show("You clicked on Option 11", null, null, MessageBoxButtons.OK);
		};
		array[11] = delegate()
		{
			MessageBox.Show("You clicked on Option 12", null, null, MessageBoxButtons.OK);
		};
		array[12] = delegate()
		{
			MessageBox.Show("You clicked on Option 13", null, null, MessageBoxButtons.OK);
		};
		array[13] = delegate()
		{
			MessageBox.Show("You clicked on Option 14", null, null, MessageBoxButtons.OK);
		};
		array[14] = delegate()
		{
			MessageBox.Show("You clicked on Option 15", null, null, MessageBoxButtons.OK);
		};
		MenuBox.Show(options, array, "Fifteen Item Test Menu");
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x000AC418 File Offset: 0x000AA618
	public void TestLocalization()
	{
		Func<string, string> originalLocalizeFunction = MessageBox.Localize;
		MessageBox.Localize = ((string originalString) => new string('X', originalString.Length));
		MessageBox.Show("Button Localization Test", delegate(DialogResult result)
		{
			MessageBox.Localize = originalLocalizeFunction;
		}, MessageBoxButtons.AbortRetryIgnore);
	}
}
