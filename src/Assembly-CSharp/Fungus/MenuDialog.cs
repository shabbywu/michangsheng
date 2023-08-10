using System;
using System.Collections;
using System.Linq;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus;

public class MenuDialog : MonoBehaviour
{
	[Tooltip("Automatically select the first interactable button when the menu is shown.")]
	[SerializeField]
	protected bool autoSelectFirstButton;

	protected Button[] cachedButtons;

	protected Slider cachedSlider;

	private int nextOptionIndex;

	public int NowOption => nextOptionIndex - 1;

	public static MenuDialog ActiveMenuDialog { get; set; }

	public virtual Button[] CachedButtons => cachedButtons;

	public virtual Slider CachedSlider => cachedSlider;

	public virtual int DisplayedOptionsCount
	{
		get
		{
			int num = 0;
			for (int i = 0; i < cachedButtons.Length; i++)
			{
				if (((Component)cachedButtons[i]).gameObject.activeSelf)
				{
					num++;
				}
			}
			return num;
		}
	}

	public virtual void SetActive(bool state)
	{
		((Component)this).gameObject.SetActive(state);
	}

	public static MenuDialog GetMenuDialog()
	{
		if ((Object)(object)ActiveMenuDialog == (Object)null)
		{
			MenuDialog menuDialog = Object.FindObjectOfType<MenuDialog>();
			if ((Object)(object)menuDialog != (Object)null)
			{
				ActiveMenuDialog = menuDialog;
			}
			if ((Object)(object)ActiveMenuDialog == (Object)null)
			{
				GameObject val = Resources.Load<GameObject>("Prefabs/MenuDialog");
				if ((Object)(object)val != (Object)null)
				{
					GameObject obj = Object.Instantiate<GameObject>(val);
					obj.SetActive(false);
					((Object)obj).name = "MenuDialog";
					ActiveMenuDialog = obj.GetComponent<MenuDialog>();
				}
			}
		}
		return ActiveMenuDialog;
	}

	protected virtual void Awake()
	{
		Button[] componentsInChildren = ((Component)this).GetComponentsInChildren<Button>();
		cachedButtons = componentsInChildren;
		Slider componentInChildren = ((Component)this).GetComponentInChildren<Slider>();
		cachedSlider = componentInChildren;
		if (Application.isPlaying)
		{
			Clear();
			CanClickManager.Inst.MenuDialogCache.Add(this);
		}
		CheckEventSystem();
	}

	protected virtual void CheckEventSystem()
	{
		if ((Object)(object)Object.FindObjectOfType<EventSystem>() == (Object)null)
		{
			GameObject val = Resources.Load<GameObject>("Prefabs/EventSystem");
			if ((Object)(object)val != (Object)null)
			{
				((Object)Object.Instantiate<GameObject>(val)).name = "EventSystem";
			}
		}
	}

	protected virtual void OnEnable()
	{
		Canvas.ForceUpdateCanvases();
	}

	protected virtual IEnumerator WaitForTimeout(float timeoutDuration, Block targetBlock)
	{
		float elapsedTime = 0f;
		Slider timeoutSlider = CachedSlider;
		while (elapsedTime < timeoutDuration)
		{
			if ((Object)(object)timeoutSlider != (Object)null)
			{
				float value = 1f - elapsedTime / timeoutDuration;
				timeoutSlider.value = value;
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		Clear();
		((Component)this).gameObject.SetActive(false);
		HideSayDialog();
		if ((Object)(object)targetBlock != (Object)null)
		{
			targetBlock.StartExecution();
		}
	}

	protected IEnumerator CallBlock(Block block)
	{
		yield return (object)new WaitForEndOfFrame();
		block.StartExecution();
	}

	protected IEnumerator CallLuaClosure(LuaEnvironment luaEnv, Closure callback)
	{
		yield return (object)new WaitForEndOfFrame();
		if (callback != null)
		{
			luaEnv.RunLuaFunction(callback, runAsCoroutine: true);
		}
	}

	public virtual void Clear()
	{
		((MonoBehaviour)this).StopAllCoroutines();
		if (nextOptionIndex != 0)
		{
			MenuSignals.DoMenuEnd(this);
		}
		nextOptionIndex = 0;
		Button[] array = CachedButtons;
		for (int i = 0; i < array.Length; i++)
		{
			((UnityEventBase)array[i].onClick).RemoveAllListeners();
		}
		for (int j = 0; j < array.Length; j++)
		{
			Button val = array[j];
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).transform.SetSiblingIndex(j);
				((Component)val).gameObject.SetActive(false);
			}
		}
		Slider val2 = CachedSlider;
		if ((Object)(object)val2 != (Object)null)
		{
			((Component)val2).gameObject.SetActive(false);
		}
	}

	public virtual void HideSayDialog()
	{
		SayDialog sayDialog = SayDialog.GetSayDialog();
		if ((Object)(object)sayDialog != (Object)null)
		{
			sayDialog.FadeWhenDone = true;
		}
	}

	public virtual bool AddOption(string text, bool interactable, bool hideOption, Block targetBlock)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		UnityAction action = (UnityAction)delegate
		{
			EventSystem.current.SetSelectedGameObject((GameObject)null);
			((MonoBehaviour)this).StopAllCoroutines();
			Clear();
			HideSayDialog();
			if ((Object)(object)targetBlock != (Object)null)
			{
				Flowchart flowchart = targetBlock.GetFlowchart();
				((Component)this).gameObject.SetActive(false);
				((MonoBehaviour)flowchart).StartCoroutine(CallBlock(targetBlock));
			}
		};
		return AddOption(text, interactable, hideOption, action);
	}

	public virtual bool AddOption(string text, bool interactable, LuaEnvironment luaEnv, Closure callBack)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		if (!((Component)this).gameObject.activeSelf)
		{
			((Component)this).gameObject.SetActive(true);
		}
		LuaEnvironment env = luaEnv;
		Closure call = callBack;
		UnityAction action = (UnityAction)delegate
		{
			((MonoBehaviour)this).StopAllCoroutines();
			Clear();
			HideSayDialog();
			((MonoBehaviour)this).StartCoroutine(CallLuaClosure(env, call));
		};
		return AddOption(text, interactable, hideOption: false, action);
	}

	private bool AddOption(string text, bool interactable, bool hideOption, UnityAction action)
	{
		if (nextOptionIndex >= CachedButtons.Length)
		{
			return false;
		}
		if (nextOptionIndex == 0)
		{
			MenuSignals.DoMenuStart(this);
		}
		Button val = cachedButtons[nextOptionIndex];
		nextOptionIndex++;
		if (hideOption)
		{
			return true;
		}
		((Component)val).gameObject.SetActive(true);
		((Selectable)val).interactable = interactable;
		if (interactable && autoSelectFirstButton && !cachedButtons.Select((Button x) => ((Component)x).gameObject).Contains(EventSystem.current.currentSelectedGameObject))
		{
			EventSystem.current.SetSelectedGameObject(((Component)val).gameObject);
		}
		TextAdapter textAdapter = new TextAdapter();
		textAdapter.InitFromGameObject(((Component)val).gameObject, includeChildren: true);
		if (textAdapter.HasTextObject())
		{
			text = TextVariationHandler.SelectVariations(text);
			if (text.Length >= 18)
			{
				textAdapter.SetTextSize(28);
			}
			else
			{
				textAdapter.SetTextSize(32);
			}
			textAdapter.Text = text;
		}
		((UnityEvent)val.onClick).AddListener(action);
		return true;
	}

	public virtual void ShowTimer(float duration, Block targetBlock)
	{
		if ((Object)(object)cachedSlider != (Object)null)
		{
			((Component)cachedSlider).gameObject.SetActive(true);
			((Component)this).gameObject.SetActive(true);
			((MonoBehaviour)this).StopAllCoroutines();
			((MonoBehaviour)this).StartCoroutine(WaitForTimeout(duration, targetBlock));
		}
	}

	public virtual IEnumerator ShowTimer(float duration, LuaEnvironment luaEnv, Closure callBack)
	{
		if ((Object)(object)CachedSlider == (Object)null || duration <= 0f)
		{
			yield break;
		}
		((Component)CachedSlider).gameObject.SetActive(true);
		((MonoBehaviour)this).StopAllCoroutines();
		float elapsedTime = 0f;
		Slider timeoutSlider = CachedSlider;
		while (elapsedTime < duration)
		{
			if ((Object)(object)timeoutSlider != (Object)null)
			{
				float value = 1f - elapsedTime / duration;
				timeoutSlider.value = value;
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		Clear();
		((Component)this).gameObject.SetActive(false);
		HideSayDialog();
		if (callBack != null)
		{
			luaEnv.RunLuaFunction(callBack, runAsCoroutine: true);
		}
	}

	public virtual bool IsActive()
	{
		return ((Component)this).gameObject.activeInHierarchy;
	}

	public void Shuffle(Random r)
	{
		for (int i = 0; i < CachedButtons.Length; i++)
		{
			((Component)CachedButtons[i]).transform.SetSiblingIndex(r.Next(CachedButtons.Length));
		}
	}
}
