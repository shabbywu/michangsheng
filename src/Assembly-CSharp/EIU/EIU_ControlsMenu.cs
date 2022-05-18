using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EIU
{
	// Token: 0x02000E9E RID: 3742
	public class EIU_ControlsMenu : MonoBehaviour
	{
		// Token: 0x060059C5 RID: 22981 RVA: 0x00249F5C File Offset: 0x0024815C
		public void Init()
		{
			if (!this.initOnce)
			{
				this.SaveAllAxes();
				this.initOnce = true;
			}
			this.rebBtn.gameObject.SetActive(false);
			this.LoadAllAxes();
			this.CreateAxisButtons();
			this.ResetAxes();
			EasyInputUtility.instance.Axes = this.Axes;
		}

		// Token: 0x060059C6 RID: 22982 RVA: 0x00249FB4 File Offset: 0x002481B4
		private void ResetAxes()
		{
			foreach (EIU_AxisBase eiu_AxisBase in this.Axes)
			{
				if (eiu_AxisBase.positiveKey == null)
				{
					int positiveKey = 0;
					this.defaultAxes.TryGetValue(eiu_AxisBase.axisName + "pKey", out positiveKey);
					eiu_AxisBase.positiveKey = positiveKey;
					eiu_AxisBase.pUIButton.ChangeKeyText(eiu_AxisBase.positiveKey.ToString());
					this.SaveAxis(eiu_AxisBase);
				}
				if (eiu_AxisBase.negativeKey == null)
				{
					int num = 0;
					this.defaultAxes.TryGetValue(eiu_AxisBase.axisName + "nKey", out num);
					eiu_AxisBase.negativeKey = num;
					if (eiu_AxisBase.nUIButton)
					{
						eiu_AxisBase.nUIButton.ChangeKeyText(eiu_AxisBase.negativeKey.ToString());
					}
					this.SaveAxis(eiu_AxisBase);
				}
			}
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x0024A0B8 File Offset: 0x002482B8
		private void FixedUpdate()
		{
			for (int i = 0; i < this.Axes.Count; i++)
			{
				EIU_AxisBase eiu_AxisBase = this.Axes[i];
				eiu_AxisBase.negative = Input.GetKey(eiu_AxisBase.negativeKey);
				eiu_AxisBase.positive = Input.GetKey(eiu_AxisBase.positiveKey);
				eiu_AxisBase.targetAxis = (float)(eiu_AxisBase.negative ? -1 : (eiu_AxisBase.positive ? 1 : 0));
				eiu_AxisBase.axis = Mathf.MoveTowards(eiu_AxisBase.axis, eiu_AxisBase.targetAxis, Time.deltaTime * eiu_AxisBase.sensitivity);
			}
		}

		// Token: 0x060059C8 RID: 22984 RVA: 0x0024A150 File Offset: 0x00248350
		private void OnGUI()
		{
			if (this.rebinding)
			{
				this.controlsList.gameObject.SetActive(false);
				Event current = Event.current;
				if (current != null)
				{
					if (current.isKey && !current.isMouse && current.keyCode != null)
					{
						this.ChangeInputKey(this.targetAxis.axisName, current.keyCode, this.negativeKey);
						this.CloseRebindingDialog();
					}
					if (current.isMouse && !EventSystem.current.IsPointerOverGameObject())
					{
						KeyCode keyCode = 0;
						int button = current.button;
						if (button != 0)
						{
							if (button == 1)
							{
								keyCode = 324;
							}
						}
						else
						{
							keyCode = 323;
						}
						if (keyCode != null)
						{
							this.ChangeInputKey(this.targetAxis.axisName, keyCode, this.negativeKey);
							this.CloseRebindingDialog();
						}
					}
				}
			}
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x0024A218 File Offset: 0x00248418
		public void ResetAllAxes()
		{
			foreach (EIU_AxisBase eiu_AxisBase in this.Axes)
			{
				int positiveKey = 0;
				this.defaultAxes.TryGetValue(eiu_AxisBase.axisName + "pKey", out positiveKey);
				eiu_AxisBase.positiveKey = positiveKey;
				int num = 0;
				this.defaultAxes.TryGetValue(eiu_AxisBase.axisName + "nKey", out num);
				eiu_AxisBase.negativeKey = num;
				eiu_AxisBase.pUIButton.ChangeKeyText(eiu_AxisBase.positiveKey.ToString());
				if (eiu_AxisBase.nUIButton)
				{
					eiu_AxisBase.nUIButton.ChangeKeyText(eiu_AxisBase.negativeKey.ToString());
				}
				this.SaveAxis(eiu_AxisBase);
			}
		}

		// Token: 0x060059CA RID: 22986 RVA: 0x0024A304 File Offset: 0x00248504
		public void ChangeInputKey(string name, KeyCode newKey, bool negative = false)
		{
			EIU_AxisBase eiu_AxisBase = this.ReturnAxis(name);
			if (eiu_AxisBase == null)
			{
				Debug.Log("Doesn't exist!");
				return;
			}
			if (negative)
			{
				eiu_AxisBase.negativeKey = newKey;
				eiu_AxisBase.nUIButton.ChangeKeyText(eiu_AxisBase.negativeKey.ToString());
			}
			else
			{
				eiu_AxisBase.positiveKey = newKey;
				eiu_AxisBase.pUIButton.ChangeKeyText(eiu_AxisBase.positiveKey.ToString());
			}
			this.SaveAxis(eiu_AxisBase);
		}

		// Token: 0x060059CB RID: 22987 RVA: 0x0024A37C File Offset: 0x0024857C
		private void CreateAxisButtons()
		{
			foreach (EIU_AxisBase eiu_AxisBase in this.Axes)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.axisPrefab);
				gameObject.transform.SetParent(this.controlsList);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				EIU_AxisButton component = gameObject.GetComponent<EIU_AxisButton>();
				component.init(eiu_AxisBase.axisName, eiu_AxisBase.pKeyDescription, eiu_AxisBase.positiveKey.ToString(), false);
				eiu_AxisBase.pUIButton = component;
				if (eiu_AxisBase.nKeyDescription != "")
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.axisPrefab);
					gameObject2.transform.SetParent(this.controlsList);
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.transform.localPosition = Vector3.zero;
					EIU_AxisButton component2 = gameObject2.GetComponent<EIU_AxisButton>();
					component2.init(eiu_AxisBase.axisName, eiu_AxisBase.nKeyDescription, eiu_AxisBase.negativeKey.ToString(), true);
					eiu_AxisBase.nUIButton = component2;
				}
			}
		}

		// Token: 0x060059CC RID: 22988 RVA: 0x0024A4C8 File Offset: 0x002486C8
		private EIU_AxisBase ReturnAxis(string name)
		{
			EIU_AxisBase result = null;
			for (int i = 0; i < this.Axes.Count; i++)
			{
				if (string.Equals(name, this.Axes[i].axisName))
				{
					result = this.Axes[i];
				}
			}
			return result;
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x0024A514 File Offset: 0x00248714
		public void OpenRebindButtonDialog(string axisName, bool negative)
		{
			this.targetAxis = this.ReturnAxis(axisName);
			this.rebinding = true;
			if (!negative)
			{
				this.rebBtn.init(this.targetAxis.pKeyDescription);
			}
			else
			{
				this.rebBtn.init(this.targetAxis.nKeyDescription);
			}
			this.rebBtn.gameObject.SetActive(true);
			this.negativeKey = negative;
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x0003FB5B File Offset: 0x0003DD5B
		private void CloseRebindingDialog()
		{
			this.rebinding = false;
			this.rebBtn.gameObject.SetActive(false);
			this.controlsList.gameObject.SetActive(true);
		}

		// Token: 0x060059CF RID: 22991 RVA: 0x0003FB86 File Offset: 0x0003DD86
		public void CancelRebinding()
		{
			this.CloseRebindingDialog();
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x0024A580 File Offset: 0x00248780
		private void SaveAllAxes()
		{
			for (int i = 0; i < this.Axes.Count; i++)
			{
				this.SaveAxesDefault(this.Axes[i]);
			}
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x0003FB8E File Offset: 0x0003DD8E
		private void SaveAxis(EIU_AxisBase axis)
		{
			PlayerPrefs.SetInt(axis.axisName + "pKey", axis.positiveKey);
			PlayerPrefs.SetInt(axis.axisName + "nKey", axis.negativeKey);
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x0024A5B8 File Offset: 0x002487B8
		private void SaveAxesDefault(EIU_AxisBase axis)
		{
			this.defaultAxes.Add(axis.axisName + "pKey", axis.positiveKey);
			this.defaultAxes.Add(axis.axisName + "nKey", axis.negativeKey);
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x0024A608 File Offset: 0x00248808
		private void LoadAllAxes()
		{
			for (int i = 0; i < this.Axes.Count; i++)
			{
				EIU_AxisBase eiu_AxisBase = this.Axes[i];
				int @int = PlayerPrefs.GetInt(eiu_AxisBase.axisName + "pKey");
				int int2 = PlayerPrefs.GetInt(eiu_AxisBase.axisName + "nKey");
				eiu_AxisBase.positiveKey = @int;
				eiu_AxisBase.negativeKey = int2;
			}
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x0003FBC6 File Offset: 0x0003DDC6
		public static EIU_ControlsMenu Instance()
		{
			return EIU_ControlsMenu.instance;
		}

		// Token: 0x060059D5 RID: 22997 RVA: 0x0003FBCD File Offset: 0x0003DDCD
		private void Awake()
		{
			EIU_ControlsMenu.instance = this;
		}

		// Token: 0x060059D6 RID: 22998 RVA: 0x0024A670 File Offset: 0x00248870
		public float GetAxis(string name)
		{
			float result = 0f;
			for (int i = 0; i < this.Axes.Count; i++)
			{
				if (string.Equals(this.Axes[i].axisName, name))
				{
					result = this.Axes[i].axis;
				}
			}
			return result;
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x0024A6C8 File Offset: 0x002488C8
		public bool GetButton(string name)
		{
			bool result = false;
			for (int i = 0; i < this.Axes.Count; i++)
			{
				if (string.Equals(this.Axes[i].axisName, name))
				{
					result = this.Axes[i].positive;
				}
			}
			return result;
		}

		// Token: 0x0400591F RID: 22815
		[HideInInspector]
		public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();

		// Token: 0x04005920 RID: 22816
		private Dictionary<string, int> defaultAxes = new Dictionary<string, int>();

		// Token: 0x04005921 RID: 22817
		[Header("UI References")]
		[Space(5f)]
		public GameObject axisPrefab;

		// Token: 0x04005922 RID: 22818
		public Transform controlsList;

		// Token: 0x04005923 RID: 22819
		public EIU_RebindButton rebBtn;

		// Token: 0x04005924 RID: 22820
		[HideInInspector]
		public bool rebinding;

		// Token: 0x04005925 RID: 22821
		private bool negativeKey;

		// Token: 0x04005926 RID: 22822
		private EIU_AxisBase targetAxis;

		// Token: 0x04005927 RID: 22823
		private bool initOnce;

		// Token: 0x04005928 RID: 22824
		public static EIU_ControlsMenu instance;
	}
}
