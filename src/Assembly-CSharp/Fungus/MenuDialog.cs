using System;
using System.Collections;
using System.Linq;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x020012D3 RID: 4819
	public class MenuDialog : MonoBehaviour
	{
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600755E RID: 30046 RVA: 0x000500B3 File Offset: 0x0004E2B3
		public int NowOption
		{
			get
			{
				return this.nextOptionIndex - 1;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600755F RID: 30047 RVA: 0x000500BD File Offset: 0x0004E2BD
		// (set) Token: 0x06007560 RID: 30048 RVA: 0x000500C4 File Offset: 0x0004E2C4
		public static MenuDialog ActiveMenuDialog { get; set; }

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06007561 RID: 30049 RVA: 0x000500CC File Offset: 0x0004E2CC
		public virtual Button[] CachedButtons
		{
			get
			{
				return this.cachedButtons;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06007562 RID: 30050 RVA: 0x000500D4 File Offset: 0x0004E2D4
		public virtual Slider CachedSlider
		{
			get
			{
				return this.cachedSlider;
			}
		}

		// Token: 0x06007563 RID: 30051 RVA: 0x000500DC File Offset: 0x0004E2DC
		public virtual void SetActive(bool state)
		{
			base.gameObject.SetActive(state);
		}

		// Token: 0x06007564 RID: 30052 RVA: 0x002B0150 File Offset: 0x002AE350
		public static MenuDialog GetMenuDialog()
		{
			if (MenuDialog.ActiveMenuDialog == null)
			{
				MenuDialog menuDialog = Object.FindObjectOfType<MenuDialog>();
				if (menuDialog != null)
				{
					MenuDialog.ActiveMenuDialog = menuDialog;
				}
				if (MenuDialog.ActiveMenuDialog == null)
				{
					GameObject gameObject = Resources.Load<GameObject>("Prefabs/MenuDialog");
					if (gameObject != null)
					{
						GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject);
						gameObject2.SetActive(false);
						gameObject2.name = "MenuDialog";
						MenuDialog.ActiveMenuDialog = gameObject2.GetComponent<MenuDialog>();
					}
				}
			}
			return MenuDialog.ActiveMenuDialog;
		}

		// Token: 0x06007565 RID: 30053 RVA: 0x002B01C8 File Offset: 0x002AE3C8
		protected virtual void Awake()
		{
			Button[] componentsInChildren = base.GetComponentsInChildren<Button>();
			this.cachedButtons = componentsInChildren;
			Slider componentInChildren = base.GetComponentInChildren<Slider>();
			this.cachedSlider = componentInChildren;
			if (Application.isPlaying)
			{
				this.Clear();
				CanClickManager.Inst.MenuDialogCache.Add(this);
			}
			this.CheckEventSystem();
		}

		// Token: 0x06007566 RID: 30054 RVA: 0x002AE198 File Offset: 0x002AC398
		protected virtual void CheckEventSystem()
		{
			if (Object.FindObjectOfType<EventSystem>() == null)
			{
				GameObject gameObject = Resources.Load<GameObject>("Prefabs/EventSystem");
				if (gameObject != null)
				{
					Object.Instantiate<GameObject>(gameObject).name = "EventSystem";
				}
			}
		}

		// Token: 0x06007567 RID: 30055 RVA: 0x000500EA File Offset: 0x0004E2EA
		protected virtual void OnEnable()
		{
			Canvas.ForceUpdateCanvases();
		}

		// Token: 0x06007568 RID: 30056 RVA: 0x000500F1 File Offset: 0x0004E2F1
		protected virtual IEnumerator WaitForTimeout(float timeoutDuration, Block targetBlock)
		{
			float elapsedTime = 0f;
			Slider timeoutSlider = this.CachedSlider;
			while (elapsedTime < timeoutDuration)
			{
				if (timeoutSlider != null)
				{
					float value = 1f - elapsedTime / timeoutDuration;
					timeoutSlider.value = value;
				}
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			this.Clear();
			base.gameObject.SetActive(false);
			this.HideSayDialog();
			if (targetBlock != null)
			{
				targetBlock.StartExecution();
			}
			yield break;
		}

		// Token: 0x06007569 RID: 30057 RVA: 0x0005010E File Offset: 0x0004E30E
		protected IEnumerator CallBlock(Block block)
		{
			yield return new WaitForEndOfFrame();
			block.StartExecution();
			yield break;
		}

		// Token: 0x0600756A RID: 30058 RVA: 0x0005011D File Offset: 0x0004E31D
		protected IEnumerator CallLuaClosure(LuaEnvironment luaEnv, Closure callback)
		{
			yield return new WaitForEndOfFrame();
			if (callback != null)
			{
				luaEnv.RunLuaFunction(callback, true, null);
			}
			yield break;
		}

		// Token: 0x0600756B RID: 30059 RVA: 0x002B0214 File Offset: 0x002AE414
		public virtual void Clear()
		{
			base.StopAllCoroutines();
			if (this.nextOptionIndex != 0)
			{
				MenuSignals.DoMenuEnd(this);
			}
			this.nextOptionIndex = 0;
			Button[] array = this.CachedButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].onClick.RemoveAllListeners();
			}
			for (int j = 0; j < array.Length; j++)
			{
				Button button = array[j];
				if (button != null)
				{
					button.transform.SetSiblingIndex(j);
					button.gameObject.SetActive(false);
				}
			}
			Slider slider = this.CachedSlider;
			if (slider != null)
			{
				slider.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600756C RID: 30060 RVA: 0x002B02B4 File Offset: 0x002AE4B4
		public virtual void HideSayDialog()
		{
			SayDialog sayDialog = SayDialog.GetSayDialog();
			if (sayDialog != null)
			{
				sayDialog.FadeWhenDone = true;
			}
		}

		// Token: 0x0600756D RID: 30061 RVA: 0x002B02D8 File Offset: 0x002AE4D8
		public virtual bool AddOption(string text, bool interactable, bool hideOption, Block targetBlock)
		{
			UnityAction action = delegate()
			{
				EventSystem.current.SetSelectedGameObject(null);
				this.StopAllCoroutines();
				this.Clear();
				this.HideSayDialog();
				if (targetBlock != null)
				{
					MonoBehaviour flowchart = targetBlock.GetFlowchart();
					this.gameObject.SetActive(false);
					flowchart.StartCoroutine(this.CallBlock(targetBlock));
				}
			};
			return this.AddOption(text, interactable, hideOption, action);
		}

		// Token: 0x0600756E RID: 30062 RVA: 0x002B0310 File Offset: 0x002AE510
		public virtual bool AddOption(string text, bool interactable, LuaEnvironment luaEnv, Closure callBack)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			LuaEnvironment env = luaEnv;
			Closure call = callBack;
			UnityAction action = delegate()
			{
				this.StopAllCoroutines();
				this.Clear();
				this.HideSayDialog();
				this.StartCoroutine(this.CallLuaClosure(env, call));
			};
			return this.AddOption(text, interactable, false, action);
		}

		// Token: 0x0600756F RID: 30063 RVA: 0x002B0368 File Offset: 0x002AE568
		private bool AddOption(string text, bool interactable, bool hideOption, UnityAction action)
		{
			if (this.nextOptionIndex >= this.CachedButtons.Length)
			{
				return false;
			}
			if (this.nextOptionIndex == 0)
			{
				MenuSignals.DoMenuStart(this);
			}
			Button button = this.cachedButtons[this.nextOptionIndex];
			this.nextOptionIndex++;
			if (hideOption)
			{
				return true;
			}
			button.gameObject.SetActive(true);
			button.interactable = interactable;
			if (interactable && this.autoSelectFirstButton)
			{
				if (!(from x in this.cachedButtons
				select x.gameObject).Contains(EventSystem.current.currentSelectedGameObject))
				{
					EventSystem.current.SetSelectedGameObject(button.gameObject);
				}
			}
			TextAdapter textAdapter = new TextAdapter();
			textAdapter.InitFromGameObject(button.gameObject, true);
			if (textAdapter.HasTextObject())
			{
				text = TextVariationHandler.SelectVariations(text, 0);
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
			button.onClick.AddListener(action);
			return true;
		}

		// Token: 0x06007570 RID: 30064 RVA: 0x002B0474 File Offset: 0x002AE674
		public virtual void ShowTimer(float duration, Block targetBlock)
		{
			if (this.cachedSlider != null)
			{
				this.cachedSlider.gameObject.SetActive(true);
				base.gameObject.SetActive(true);
				base.StopAllCoroutines();
				base.StartCoroutine(this.WaitForTimeout(duration, targetBlock));
			}
		}

		// Token: 0x06007571 RID: 30065 RVA: 0x00050133 File Offset: 0x0004E333
		public virtual IEnumerator ShowTimer(float duration, LuaEnvironment luaEnv, Closure callBack)
		{
			if (this.CachedSlider == null || duration <= 0f)
			{
				yield break;
			}
			this.CachedSlider.gameObject.SetActive(true);
			base.StopAllCoroutines();
			float elapsedTime = 0f;
			Slider timeoutSlider = this.CachedSlider;
			while (elapsedTime < duration)
			{
				if (timeoutSlider != null)
				{
					float value = 1f - elapsedTime / duration;
					timeoutSlider.value = value;
				}
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			this.Clear();
			base.gameObject.SetActive(false);
			this.HideSayDialog();
			if (callBack != null)
			{
				luaEnv.RunLuaFunction(callBack, true, null);
			}
			yield break;
		}

		// Token: 0x06007572 RID: 30066 RVA: 0x0004FD5F File Offset: 0x0004DF5F
		public virtual bool IsActive()
		{
			return base.gameObject.activeInHierarchy;
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06007573 RID: 30067 RVA: 0x002B04C4 File Offset: 0x002AE6C4
		public virtual int DisplayedOptionsCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.cachedButtons.Length; i++)
				{
					if (this.cachedButtons[i].gameObject.activeSelf)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06007574 RID: 30068 RVA: 0x002B0500 File Offset: 0x002AE700
		public void Shuffle(Random r)
		{
			for (int i = 0; i < this.CachedButtons.Length; i++)
			{
				this.CachedButtons[i].transform.SetSiblingIndex(r.Next(this.CachedButtons.Length));
			}
		}

		// Token: 0x04006690 RID: 26256
		[Tooltip("Automatically select the first interactable button when the menu is shown.")]
		[SerializeField]
		protected bool autoSelectFirstButton;

		// Token: 0x04006691 RID: 26257
		protected Button[] cachedButtons;

		// Token: 0x04006692 RID: 26258
		protected Slider cachedSlider;

		// Token: 0x04006693 RID: 26259
		private int nextOptionIndex;
	}
}
