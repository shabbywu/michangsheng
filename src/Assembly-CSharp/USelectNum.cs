using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class USelectNum : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__18_0;

		public static UnityAction _003C_003E9__18_2;

		public static UnityAction _003C_003E9__18_3;

		internal void _003CUpdate_003Eb__18_0()
		{
			Close();
		}

		internal void _003CUpdate_003Eb__18_2()
		{
			Close();
		}

		internal void _003CUpdate_003Eb__18_3()
		{
			if (CancelAction != null)
			{
				CancelAction.Invoke();
			}
		}
	}

	private static USelectNum inst;

	private static string prefabPath = "USelectNum";

	private static RectTransform myRT;

	private static bool needShow;

	private static string Decs;

	private static int MinNum;

	private static int MaxNum;

	private static UnityAction<int> OKAction;

	private static UnityAction CancelAction;

	public static bool IsShow;

	public Text DescText;

	public Slider NumSlider;

	public Button SubBtn;

	public Button AddBtn;

	public Button OkBtn;

	public Button CancelBtn;

	private int NowNum;

	private void Awake()
	{
		if ((Object)(object)inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)((Component)this).transform.parent).gameObject);
		}
		inst = this;
		Transform transform = ((Component)this).transform;
		myRT = (RectTransform)(object)((transform is RectTransform) ? transform : null);
		Object.DontDestroyOnLoad((Object)(object)((Component)((Component)this).transform.parent).gameObject);
	}

	private void Update()
	{
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		if (!needShow)
		{
			return;
		}
		IsShow = true;
		needShow = false;
		((Component)((Transform)myRT).GetChild(0)).gameObject.SetActive(true);
		RefreshText(1f);
		NumSlider.maxValue = MaxNum;
		NumSlider.minValue = MinNum;
		((UnityEventBase)NumSlider.onValueChanged).RemoveAllListeners();
		NumSlider.value = 1f;
		((UnityEvent<float>)(object)NumSlider.onValueChanged).AddListener((UnityAction<float>)RefreshText);
		((UnityEventBase)OkBtn.onClick).RemoveAllListeners();
		ButtonClickedEvent onClick = OkBtn.onClick;
		object obj = _003C_003Ec._003C_003E9__18_0;
		if (obj == null)
		{
			UnityAction val = delegate
			{
				Close();
			};
			_003C_003Ec._003C_003E9__18_0 = val;
			obj = (object)val;
		}
		((UnityEvent)onClick).AddListener((UnityAction)obj);
		((UnityEvent)OkBtn.onClick).AddListener((UnityAction)delegate
		{
			if (OKAction != null)
			{
				OKAction.Invoke(NowNum);
			}
		});
		((UnityEventBase)CancelBtn.onClick).RemoveAllListeners();
		ButtonClickedEvent onClick2 = CancelBtn.onClick;
		object obj2 = _003C_003Ec._003C_003E9__18_2;
		if (obj2 == null)
		{
			UnityAction val2 = delegate
			{
				Close();
			};
			_003C_003Ec._003C_003E9__18_2 = val2;
			obj2 = (object)val2;
		}
		((UnityEvent)onClick2).AddListener((UnityAction)obj2);
		ButtonClickedEvent onClick3 = CancelBtn.onClick;
		object obj3 = _003C_003Ec._003C_003E9__18_3;
		if (obj3 == null)
		{
			UnityAction val3 = delegate
			{
				if (CancelAction != null)
				{
					CancelAction.Invoke();
				}
			};
			_003C_003Ec._003C_003E9__18_3 = val3;
			obj3 = (object)val3;
		}
		((UnityEvent)onClick3).AddListener((UnityAction)obj3);
		((UnityEventBase)AddBtn.onClick).RemoveAllListeners();
		((UnityEvent)AddBtn.onClick).AddListener((UnityAction)delegate
		{
			Slider numSlider2 = NumSlider;
			float value2 = numSlider2.value;
			numSlider2.value = value2 + 1f;
		});
		((UnityEventBase)SubBtn.onClick).RemoveAllListeners();
		((UnityEvent)SubBtn.onClick).AddListener((UnityAction)delegate
		{
			Slider numSlider = NumSlider;
			float value = numSlider.value;
			numSlider.value = value - 1f;
		});
	}

	public static void Close()
	{
		IsShow = false;
		if ((Object)(object)myRT != (Object)null)
		{
			((Component)((Transform)myRT).GetChild(0)).gameObject.SetActive(false);
		}
	}

	private void RefreshText(float v)
	{
		NowNum = (int)v;
		DescText.text = Decs.Replace("{num}", NowNum.ToString());
	}

	public static void Show(string desc, int minNum, int maxNum, UnityAction<int> OK = null, UnityAction Cancel = null)
	{
		if ((Object)(object)inst == (Object)null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(prefabPath));
		}
		Decs = desc;
		MinNum = minNum;
		MaxNum = maxNum;
		OKAction = OK;
		CancelAction = Cancel;
		needShow = true;
	}
}
