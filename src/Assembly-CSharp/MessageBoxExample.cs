using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class MessageBoxExample : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static Action<DialogResult> _003C_003E9__3_0;

		public static Action<DialogResult> _003C_003E9__4_0;

		public static Action<DialogResult> _003C_003E9__5_0;

		public static Action<DialogResult> _003C_003E9__6_0;

		public static Action<DialogResult> _003C_003E9__7_0;

		public static Action<DialogResult> _003C_003E9__8_0;

		public static UnityAction _003C_003E9__9_0;

		public static UnityAction _003C_003E9__9_1;

		public static UnityAction _003C_003E9__9_2;

		public static UnityAction _003C_003E9__9_3;

		public static UnityAction _003C_003E9__9_4;

		public static UnityAction _003C_003E9__10_0;

		public static UnityAction _003C_003E9__10_1;

		public static UnityAction _003C_003E9__10_2;

		public static UnityAction _003C_003E9__10_3;

		public static UnityAction _003C_003E9__10_4;

		public static UnityAction _003C_003E9__10_5;

		public static UnityAction _003C_003E9__10_6;

		public static UnityAction _003C_003E9__10_7;

		public static UnityAction _003C_003E9__10_8;

		public static UnityAction _003C_003E9__11_0;

		public static UnityAction _003C_003E9__11_1;

		public static UnityAction _003C_003E9__11_2;

		public static UnityAction _003C_003E9__11_3;

		public static UnityAction _003C_003E9__11_4;

		public static UnityAction _003C_003E9__11_5;

		public static UnityAction _003C_003E9__11_6;

		public static UnityAction _003C_003E9__11_7;

		public static UnityAction _003C_003E9__11_8;

		public static UnityAction _003C_003E9__11_9;

		public static UnityAction _003C_003E9__11_10;

		public static UnityAction _003C_003E9__11_11;

		public static UnityAction _003C_003E9__11_12;

		public static UnityAction _003C_003E9__11_13;

		public static UnityAction _003C_003E9__11_14;

		public static Func<string, string> _003C_003E9__12_0;

		internal void _003CTestWithCallback_003Eb__3_0(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}

		internal void _003CTestOKCancelButtons_003Eb__4_0(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}

		internal void _003CTestRetryCancelButtons_003Eb__5_0(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}

		internal void _003CTestYesNoButtons_003Eb__6_0(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}

		internal void _003CTestYesNoCancelButtons_003Eb__7_0(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}

		internal void _003CTestAbortRetryIgnoreButtons_003Eb__8_0(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}

		internal void _003CTestMenu5_003Eb__9_0()
		{
			MessageBox.Show("You clicked on Option 1");
		}

		internal void _003CTestMenu5_003Eb__9_1()
		{
			MessageBox.Show("You clicked on Option 2");
		}

		internal void _003CTestMenu5_003Eb__9_2()
		{
			MessageBox.Show("You clicked on Option 3");
		}

		internal void _003CTestMenu5_003Eb__9_3()
		{
			MessageBox.Show("You clicked on Option 4");
		}

		internal void _003CTestMenu5_003Eb__9_4()
		{
			MessageBox.Show("You clicked on Option 5");
		}

		internal void _003CTestMenu10_003Eb__10_0()
		{
			MessageBox.Show("You clicked on Option 1");
		}

		internal void _003CTestMenu10_003Eb__10_1()
		{
			MessageBox.Show("You clicked on Option 2");
		}

		internal void _003CTestMenu10_003Eb__10_2()
		{
			MessageBox.Show("You clicked on Option 3");
		}

		internal void _003CTestMenu10_003Eb__10_3()
		{
			MessageBox.Show("You clicked on Option 4");
		}

		internal void _003CTestMenu10_003Eb__10_4()
		{
			MessageBox.Show("You clicked on Option 5");
		}

		internal void _003CTestMenu10_003Eb__10_5()
		{
			MessageBox.Show("You clicked on Option 6");
		}

		internal void _003CTestMenu10_003Eb__10_6()
		{
			MessageBox.Show("You clicked on Option 7");
		}

		internal void _003CTestMenu10_003Eb__10_7()
		{
			MessageBox.Show("You clicked on Option 8");
		}

		internal void _003CTestMenu10_003Eb__10_8()
		{
			MessageBox.Show("You clicked on Option 9");
		}

		internal void _003CTestMenu15_003Eb__11_0()
		{
			MessageBox.Show("You clicked on Option 1");
		}

		internal void _003CTestMenu15_003Eb__11_1()
		{
			MessageBox.Show("You clicked on Option 2");
		}

		internal void _003CTestMenu15_003Eb__11_2()
		{
			MessageBox.Show("You clicked on Option 3");
		}

		internal void _003CTestMenu15_003Eb__11_3()
		{
			MessageBox.Show("You clicked on Option 4");
		}

		internal void _003CTestMenu15_003Eb__11_4()
		{
			MessageBox.Show("You clicked on Option 5");
		}

		internal void _003CTestMenu15_003Eb__11_5()
		{
			MessageBox.Show("You clicked on Option 6");
		}

		internal void _003CTestMenu15_003Eb__11_6()
		{
			MessageBox.Show("You clicked on Option 7");
		}

		internal void _003CTestMenu15_003Eb__11_7()
		{
			MessageBox.Show("You clicked on Option 8");
		}

		internal void _003CTestMenu15_003Eb__11_8()
		{
			MessageBox.Show("You clicked on Option 9");
		}

		internal void _003CTestMenu15_003Eb__11_9()
		{
			MessageBox.Show("You clicked on Option 10");
		}

		internal void _003CTestMenu15_003Eb__11_10()
		{
			MessageBox.Show("You clicked on Option 11");
		}

		internal void _003CTestMenu15_003Eb__11_11()
		{
			MessageBox.Show("You clicked on Option 12");
		}

		internal void _003CTestMenu15_003Eb__11_12()
		{
			MessageBox.Show("You clicked on Option 13");
		}

		internal void _003CTestMenu15_003Eb__11_13()
		{
			MessageBox.Show("You clicked on Option 14");
		}

		internal void _003CTestMenu15_003Eb__11_14()
		{
			MessageBox.Show("You clicked on Option 15");
		}

		internal string _003CTestLocalization_003Eb__12_0(string originalString)
		{
			return new string('X', originalString.Length);
		}
	}

	private void Start()
	{
		GameObject val = GameObject.Find("Message Box");
		if ((Object)(object)val != (Object)null)
		{
			Object.Destroy((Object)(object)val);
		}
		GameObject val2 = GameObject.Find("Menu Box");
		if ((Object)(object)val2 != (Object)null)
		{
			Object.Destroy((Object)(object)val2);
		}
	}

	public void Test()
	{
		MessageBox.Show("Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
	}

	public void TestWithTitle()
	{
		MessageBox.Show("This is a message with a title.", "Message Box Title");
	}

	public void TestWithCallback()
	{
		MessageBox.Show("This is a message with a callback.", "Message Box Callback Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		});
	}

	public void TestOKCancelButtons()
	{
		MessageBox.Show("Are you sure you wish to delete your save game?", "Delete Save", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}, MessageBoxButtons.OKCancel);
	}

	public void TestRetryCancelButtons()
	{
		MessageBox.Show("This is a message with a set of buttons selected.", "Message Box Buttons Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}, MessageBoxButtons.RetryCancel);
	}

	public void TestYesNoButtons()
	{
		MessageBox.Show("Give us five stars?", "Review Game", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}, MessageBoxButtons.YesNo);
	}

	public void TestYesNoCancelButtons()
	{
		MessageBox.Show("This is a message with a set of buttons selected.", "Message Box Buttons Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}, MessageBoxButtons.YesNoCancel);
	}

	public void TestAbortRetryIgnoreButtons()
	{
		MessageBox.Show("Not ready reading drive A", "Message Box Buttons Example", delegate(DialogResult result)
		{
			MessageBox.Show("You Clicked " + result, "Dialog Result");
		}, MessageBoxButtons.AbortRetryIgnore);
	}

	public void TestMenu5()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		string[] options = new string[5] { "Option 1\nOption description can go here.", "Option 2\nTwo", "Option 3\nThree", "Option 4\nFour", "Option 5\nFive of Nine?" };
		UnityAction[] array = new UnityAction[5];
		object obj = _003C_003Ec._003C_003E9__9_0;
		if (obj == null)
		{
			UnityAction val = delegate
			{
				MessageBox.Show("You clicked on Option 1");
			};
			_003C_003Ec._003C_003E9__9_0 = val;
			obj = (object)val;
		}
		array[0] = (UnityAction)obj;
		object obj2 = _003C_003Ec._003C_003E9__9_1;
		if (obj2 == null)
		{
			UnityAction val2 = delegate
			{
				MessageBox.Show("You clicked on Option 2");
			};
			_003C_003Ec._003C_003E9__9_1 = val2;
			obj2 = (object)val2;
		}
		array[1] = (UnityAction)obj2;
		object obj3 = _003C_003Ec._003C_003E9__9_2;
		if (obj3 == null)
		{
			UnityAction val3 = delegate
			{
				MessageBox.Show("You clicked on Option 3");
			};
			_003C_003Ec._003C_003E9__9_2 = val3;
			obj3 = (object)val3;
		}
		array[2] = (UnityAction)obj3;
		object obj4 = _003C_003Ec._003C_003E9__9_3;
		if (obj4 == null)
		{
			UnityAction val4 = delegate
			{
				MessageBox.Show("You clicked on Option 4");
			};
			_003C_003Ec._003C_003E9__9_3 = val4;
			obj4 = (object)val4;
		}
		array[3] = (UnityAction)obj4;
		object obj5 = _003C_003Ec._003C_003E9__9_4;
		if (obj5 == null)
		{
			UnityAction val5 = delegate
			{
				MessageBox.Show("You clicked on Option 5");
			};
			_003C_003Ec._003C_003E9__9_4 = val5;
			obj5 = (object)val5;
		}
		array[4] = (UnityAction)obj5;
		MenuBox.Show(options, (IEnumerable<UnityAction>)(object)array);
	}

	public void TestMenu10()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		string[] options = new string[10] { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5", "Option 6", "Option 7", "Option 8", "Option 9", "Show an even bigger menu!" };
		UnityAction[] array = new UnityAction[10];
		object obj = _003C_003Ec._003C_003E9__10_0;
		if (obj == null)
		{
			UnityAction val = delegate
			{
				MessageBox.Show("You clicked on Option 1");
			};
			_003C_003Ec._003C_003E9__10_0 = val;
			obj = (object)val;
		}
		array[0] = (UnityAction)obj;
		object obj2 = _003C_003Ec._003C_003E9__10_1;
		if (obj2 == null)
		{
			UnityAction val2 = delegate
			{
				MessageBox.Show("You clicked on Option 2");
			};
			_003C_003Ec._003C_003E9__10_1 = val2;
			obj2 = (object)val2;
		}
		array[1] = (UnityAction)obj2;
		object obj3 = _003C_003Ec._003C_003E9__10_2;
		if (obj3 == null)
		{
			UnityAction val3 = delegate
			{
				MessageBox.Show("You clicked on Option 3");
			};
			_003C_003Ec._003C_003E9__10_2 = val3;
			obj3 = (object)val3;
		}
		array[2] = (UnityAction)obj3;
		object obj4 = _003C_003Ec._003C_003E9__10_3;
		if (obj4 == null)
		{
			UnityAction val4 = delegate
			{
				MessageBox.Show("You clicked on Option 4");
			};
			_003C_003Ec._003C_003E9__10_3 = val4;
			obj4 = (object)val4;
		}
		array[3] = (UnityAction)obj4;
		object obj5 = _003C_003Ec._003C_003E9__10_4;
		if (obj5 == null)
		{
			UnityAction val5 = delegate
			{
				MessageBox.Show("You clicked on Option 5");
			};
			_003C_003Ec._003C_003E9__10_4 = val5;
			obj5 = (object)val5;
		}
		array[4] = (UnityAction)obj5;
		object obj6 = _003C_003Ec._003C_003E9__10_5;
		if (obj6 == null)
		{
			UnityAction val6 = delegate
			{
				MessageBox.Show("You clicked on Option 6");
			};
			_003C_003Ec._003C_003E9__10_5 = val6;
			obj6 = (object)val6;
		}
		array[5] = (UnityAction)obj6;
		object obj7 = _003C_003Ec._003C_003E9__10_6;
		if (obj7 == null)
		{
			UnityAction val7 = delegate
			{
				MessageBox.Show("You clicked on Option 7");
			};
			_003C_003Ec._003C_003E9__10_6 = val7;
			obj7 = (object)val7;
		}
		array[6] = (UnityAction)obj7;
		object obj8 = _003C_003Ec._003C_003E9__10_7;
		if (obj8 == null)
		{
			UnityAction val8 = delegate
			{
				MessageBox.Show("You clicked on Option 8");
			};
			_003C_003Ec._003C_003E9__10_7 = val8;
			obj8 = (object)val8;
		}
		array[7] = (UnityAction)obj8;
		object obj9 = _003C_003Ec._003C_003E9__10_8;
		if (obj9 == null)
		{
			UnityAction val9 = delegate
			{
				MessageBox.Show("You clicked on Option 9");
			};
			_003C_003Ec._003C_003E9__10_8 = val9;
			obj9 = (object)val9;
		}
		array[8] = (UnityAction)obj9;
		array[9] = TestMenu15;
		MenuBox.Show(options, (IEnumerable<UnityAction>)(object)array, "Ten Item Test Menu");
	}

	public void TestMenu15()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Expected O, but got Unknown
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		string[] options = new string[15]
		{
			"Option 1", "Option 2", "Option 3", "Option 4", "Option 5", "Option 6", "Option 7", "Option 8", "Option 9", "Option 10",
			"Option 11", "Option 12", "Option 13", "Option 14", "Option 15"
		};
		UnityAction[] array = new UnityAction[15];
		object obj = _003C_003Ec._003C_003E9__11_0;
		if (obj == null)
		{
			UnityAction val = delegate
			{
				MessageBox.Show("You clicked on Option 1");
			};
			_003C_003Ec._003C_003E9__11_0 = val;
			obj = (object)val;
		}
		array[0] = (UnityAction)obj;
		object obj2 = _003C_003Ec._003C_003E9__11_1;
		if (obj2 == null)
		{
			UnityAction val2 = delegate
			{
				MessageBox.Show("You clicked on Option 2");
			};
			_003C_003Ec._003C_003E9__11_1 = val2;
			obj2 = (object)val2;
		}
		array[1] = (UnityAction)obj2;
		object obj3 = _003C_003Ec._003C_003E9__11_2;
		if (obj3 == null)
		{
			UnityAction val3 = delegate
			{
				MessageBox.Show("You clicked on Option 3");
			};
			_003C_003Ec._003C_003E9__11_2 = val3;
			obj3 = (object)val3;
		}
		array[2] = (UnityAction)obj3;
		object obj4 = _003C_003Ec._003C_003E9__11_3;
		if (obj4 == null)
		{
			UnityAction val4 = delegate
			{
				MessageBox.Show("You clicked on Option 4");
			};
			_003C_003Ec._003C_003E9__11_3 = val4;
			obj4 = (object)val4;
		}
		array[3] = (UnityAction)obj4;
		object obj5 = _003C_003Ec._003C_003E9__11_4;
		if (obj5 == null)
		{
			UnityAction val5 = delegate
			{
				MessageBox.Show("You clicked on Option 5");
			};
			_003C_003Ec._003C_003E9__11_4 = val5;
			obj5 = (object)val5;
		}
		array[4] = (UnityAction)obj5;
		object obj6 = _003C_003Ec._003C_003E9__11_5;
		if (obj6 == null)
		{
			UnityAction val6 = delegate
			{
				MessageBox.Show("You clicked on Option 6");
			};
			_003C_003Ec._003C_003E9__11_5 = val6;
			obj6 = (object)val6;
		}
		array[5] = (UnityAction)obj6;
		object obj7 = _003C_003Ec._003C_003E9__11_6;
		if (obj7 == null)
		{
			UnityAction val7 = delegate
			{
				MessageBox.Show("You clicked on Option 7");
			};
			_003C_003Ec._003C_003E9__11_6 = val7;
			obj7 = (object)val7;
		}
		array[6] = (UnityAction)obj7;
		object obj8 = _003C_003Ec._003C_003E9__11_7;
		if (obj8 == null)
		{
			UnityAction val8 = delegate
			{
				MessageBox.Show("You clicked on Option 8");
			};
			_003C_003Ec._003C_003E9__11_7 = val8;
			obj8 = (object)val8;
		}
		array[7] = (UnityAction)obj8;
		object obj9 = _003C_003Ec._003C_003E9__11_8;
		if (obj9 == null)
		{
			UnityAction val9 = delegate
			{
				MessageBox.Show("You clicked on Option 9");
			};
			_003C_003Ec._003C_003E9__11_8 = val9;
			obj9 = (object)val9;
		}
		array[8] = (UnityAction)obj9;
		object obj10 = _003C_003Ec._003C_003E9__11_9;
		if (obj10 == null)
		{
			UnityAction val10 = delegate
			{
				MessageBox.Show("You clicked on Option 10");
			};
			_003C_003Ec._003C_003E9__11_9 = val10;
			obj10 = (object)val10;
		}
		array[9] = (UnityAction)obj10;
		object obj11 = _003C_003Ec._003C_003E9__11_10;
		if (obj11 == null)
		{
			UnityAction val11 = delegate
			{
				MessageBox.Show("You clicked on Option 11");
			};
			_003C_003Ec._003C_003E9__11_10 = val11;
			obj11 = (object)val11;
		}
		array[10] = (UnityAction)obj11;
		object obj12 = _003C_003Ec._003C_003E9__11_11;
		if (obj12 == null)
		{
			UnityAction val12 = delegate
			{
				MessageBox.Show("You clicked on Option 12");
			};
			_003C_003Ec._003C_003E9__11_11 = val12;
			obj12 = (object)val12;
		}
		array[11] = (UnityAction)obj12;
		object obj13 = _003C_003Ec._003C_003E9__11_12;
		if (obj13 == null)
		{
			UnityAction val13 = delegate
			{
				MessageBox.Show("You clicked on Option 13");
			};
			_003C_003Ec._003C_003E9__11_12 = val13;
			obj13 = (object)val13;
		}
		array[12] = (UnityAction)obj13;
		object obj14 = _003C_003Ec._003C_003E9__11_13;
		if (obj14 == null)
		{
			UnityAction val14 = delegate
			{
				MessageBox.Show("You clicked on Option 14");
			};
			_003C_003Ec._003C_003E9__11_13 = val14;
			obj14 = (object)val14;
		}
		array[13] = (UnityAction)obj14;
		object obj15 = _003C_003Ec._003C_003E9__11_14;
		if (obj15 == null)
		{
			UnityAction val15 = delegate
			{
				MessageBox.Show("You clicked on Option 15");
			};
			_003C_003Ec._003C_003E9__11_14 = val15;
			obj15 = (object)val15;
		}
		array[14] = (UnityAction)obj15;
		MenuBox.Show(options, (IEnumerable<UnityAction>)(object)array, "Fifteen Item Test Menu");
	}

	public void TestLocalization()
	{
		Func<string, string> originalLocalizeFunction = MessageBox.Localize;
		MessageBox.Localize = (string originalString) => new string('X', originalString.Length);
		MessageBox.Show("Button Localization Test", delegate
		{
			MessageBox.Localize = originalLocalizeFunction;
		}, MessageBoxButtons.AbortRetryIgnore);
	}
}
