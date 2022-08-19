using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000160 RID: 352
public class MessageBoxExample : MonoBehaviour
{
	// Token: 0x06000F50 RID: 3920 RVA: 0x0005C074 File Offset: 0x0005A274
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

	// Token: 0x06000F51 RID: 3921 RVA: 0x0005C0B5 File Offset: 0x0005A2B5
	public void Test()
	{
		MessageBox.Show("Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, null, MessageBoxButtons.OK);
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x0005C0C5 File Offset: 0x0005A2C5
	public void TestWithTitle()
	{
		MessageBox.Show("This is a message with a title.", "Message Box Title", null, MessageBoxButtons.OK);
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x0005C0D9 File Offset: 0x0005A2D9
	public void TestWithCallback()
	{
		MessageBox.Show("This is a message with a callback.", "Message Box Callback Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.OK);
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x0005C10B File Offset: 0x0005A30B
	public void TestOKCancelButtons()
	{
		MessageBox.Show("Are you sure you wish to delete your save game?", "Delete Save", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.OKCancel);
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x0005C13D File Offset: 0x0005A33D
	public void TestRetryCancelButtons()
	{
		MessageBox.Show("This is a message with a set of buttons selected.", "Message Box Buttons Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.RetryCancel);
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x0005C16F File Offset: 0x0005A36F
	public void TestYesNoButtons()
	{
		MessageBox.Show("Give us five stars?", "Review Game", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.YesNo);
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x0005C1A1 File Offset: 0x0005A3A1
	public void TestYesNoCancelButtons()
	{
		MessageBox.Show("This is a message with a set of buttons selected.", "Message Box Buttons Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.YesNoCancel);
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x0005C1D3 File Offset: 0x0005A3D3
	public void TestAbortRetryIgnoreButtons()
	{
		MessageBox.Show("Not ready reading drive A", "Message Box Buttons Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
		}, MessageBoxButtons.AbortRetryIgnore);
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x0005C208 File Offset: 0x0005A408
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

	// Token: 0x06000F5A RID: 3930 RVA: 0x0005C300 File Offset: 0x0005A500
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

	// Token: 0x06000F5B RID: 3931 RVA: 0x0005C4BC File Offset: 0x0005A6BC
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

	// Token: 0x06000F5C RID: 3932 RVA: 0x0005C764 File Offset: 0x0005A964
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
