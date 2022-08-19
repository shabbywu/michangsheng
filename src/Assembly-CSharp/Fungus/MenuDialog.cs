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
	// Token: 0x02000E74 RID: 3700
	public class MenuDialog : MonoBehaviour
	{
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060068A4 RID: 26788 RVA: 0x0028DC8F File Offset: 0x0028BE8F
		public int NowOption
		{
			get
			{
				return this.nextOptionIndex - 1;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060068A5 RID: 26789 RVA: 0x0028DC99 File Offset: 0x0028BE99
		// (set) Token: 0x060068A6 RID: 26790 RVA: 0x0028DCA0 File Offset: 0x0028BEA0
		public static MenuDialog ActiveMenuDialog { get; set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060068A7 RID: 26791 RVA: 0x0028DCA8 File Offset: 0x0028BEA8
		public virtual Button[] CachedButtons
		{
			get
			{
				return this.cachedButtons;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060068A8 RID: 26792 RVA: 0x0028DCB0 File Offset: 0x0028BEB0
		public virtual Slider CachedSlider
		{
			get
			{
				return this.cachedSlider;
			}
		}

		// Token: 0x060068A9 RID: 26793 RVA: 0x0028DCB8 File Offset: 0x0028BEB8
		public virtual void SetActive(bool state)
		{
			base.gameObject.SetActive(state);
		}

		// Token: 0x060068AA RID: 26794 RVA: 0x0028DCC8 File Offset: 0x0028BEC8
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

		// Token: 0x060068AB RID: 26795 RVA: 0x0028DD40 File Offset: 0x0028BF40
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

		// Token: 0x060068AC RID: 26796 RVA: 0x0028DD8C File Offset: 0x0028BF8C
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

		// Token: 0x060068AD RID: 26797 RVA: 0x0028DDCA File Offset: 0x0028BFCA
		protected virtual void OnEnable()
		{
			Canvas.ForceUpdateCanvases();
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x0028DDD1 File Offset: 0x0028BFD1
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

		// Token: 0x060068AF RID: 26799 RVA: 0x0028DDEE File Offset: 0x0028BFEE
		protected IEnumerator CallBlock(Block block)
		{
			yield return new WaitForEndOfFrame();
			block.StartExecution();
			yield break;
		}

		// Token: 0x060068B0 RID: 26800 RVA: 0x0028DDFD File Offset: 0x0028BFFD
		protected IEnumerator CallLuaClosure(LuaEnvironment luaEnv, Closure callback)
		{
			yield return new WaitForEndOfFrame();
			if (callback != null)
			{
				luaEnv.RunLuaFunction(callback, true, null);
			}
			yield break;
		}

		// Token: 0x060068B1 RID: 26801 RVA: 0x0028DE14 File Offset: 0x0028C014
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

		// Token: 0x060068B2 RID: 26802 RVA: 0x0028DEB4 File Offset: 0x0028C0B4
		public virtual void HideSayDialog()
		{
			SayDialog sayDialog = SayDialog.GetSayDialog();
			if (sayDialog != null)
			{
				sayDialog.FadeWhenDone = true;
			}
		}

		// Token: 0x060068B3 RID: 26803 RVA: 0x0028DED8 File Offset: 0x0028C0D8
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

		// Token: 0x060068B4 RID: 26804 RVA: 0x0028DF10 File Offset: 0x0028C110
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

		// Token: 0x060068B5 RID: 26805 RVA: 0x0028DF68 File Offset: 0x0028C168
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

		// Token: 0x060068B6 RID: 26806 RVA: 0x0028E074 File Offset: 0x0028C274
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

		// Token: 0x060068B7 RID: 26807 RVA: 0x0028E0C1 File Offset: 0x0028C2C1
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

		// Token: 0x060068B8 RID: 26808 RVA: 0x0028C138 File Offset: 0x0028A338
		public virtual bool IsActive()
		{
			return base.gameObject.activeInHierarchy;
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060068B9 RID: 26809 RVA: 0x0028E0E8 File Offset: 0x0028C2E8
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

		// Token: 0x060068BA RID: 26810 RVA: 0x0028E124 File Offset: 0x0028C324
		public void Shuffle(Random r)
		{
			for (int i = 0; i < this.CachedButtons.Length; i++)
			{
				this.CachedButtons[i].transform.SetSiblingIndex(r.Next(this.CachedButtons.Length));
			}
		}

		// Token: 0x040058EF RID: 22767
		[Tooltip("Automatically select the first interactable button when the menu is shown.")]
		[SerializeField]
		protected bool autoSelectFirstButton;

		// Token: 0x040058F0 RID: 22768
		protected Button[] cachedButtons;

		// Token: 0x040058F1 RID: 22769
		protected Slider cachedSlider;

		// Token: 0x040058F2 RID: 22770
		private int nextOptionIndex;
	}
}
