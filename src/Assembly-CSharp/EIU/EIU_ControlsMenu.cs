using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EIU
{
	// Token: 0x02000B2A RID: 2858
	public class EIU_ControlsMenu : MonoBehaviour
	{
		// Token: 0x06004FA6 RID: 20390 RVA: 0x0021A21C File Offset: 0x0021841C
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

		// Token: 0x06004FA7 RID: 20391 RVA: 0x0021A274 File Offset: 0x00218474
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

		// Token: 0x06004FA8 RID: 20392 RVA: 0x0021A378 File Offset: 0x00218578
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

		// Token: 0x06004FA9 RID: 20393 RVA: 0x0021A410 File Offset: 0x00218610
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

		// Token: 0x06004FAA RID: 20394 RVA: 0x0021A4D8 File Offset: 0x002186D8
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

		// Token: 0x06004FAB RID: 20395 RVA: 0x0021A5C4 File Offset: 0x002187C4
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

		// Token: 0x06004FAC RID: 20396 RVA: 0x0021A63C File Offset: 0x0021883C
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

		// Token: 0x06004FAD RID: 20397 RVA: 0x0021A788 File Offset: 0x00218988
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

		// Token: 0x06004FAE RID: 20398 RVA: 0x0021A7D4 File Offset: 0x002189D4
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

		// Token: 0x06004FAF RID: 20399 RVA: 0x0021A83E File Offset: 0x00218A3E
		private void CloseRebindingDialog()
		{
			this.rebinding = false;
			this.rebBtn.gameObject.SetActive(false);
			this.controlsList.gameObject.SetActive(true);
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x0021A869 File Offset: 0x00218A69
		public void CancelRebinding()
		{
			this.CloseRebindingDialog();
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x0021A874 File Offset: 0x00218A74
		private void SaveAllAxes()
		{
			for (int i = 0; i < this.Axes.Count; i++)
			{
				this.SaveAxesDefault(this.Axes[i]);
			}
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x0021A8A9 File Offset: 0x00218AA9
		private void SaveAxis(EIU_AxisBase axis)
		{
			PlayerPrefs.SetInt(axis.axisName + "pKey", axis.positiveKey);
			PlayerPrefs.SetInt(axis.axisName + "nKey", axis.negativeKey);
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x0021A8E4 File Offset: 0x00218AE4
		private void SaveAxesDefault(EIU_AxisBase axis)
		{
			this.defaultAxes.Add(axis.axisName + "pKey", axis.positiveKey);
			this.defaultAxes.Add(axis.axisName + "nKey", axis.negativeKey);
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x0021A934 File Offset: 0x00218B34
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

		// Token: 0x06004FB5 RID: 20405 RVA: 0x0021A99C File Offset: 0x00218B9C
		public static EIU_ControlsMenu Instance()
		{
			return EIU_ControlsMenu.instance;
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x0021A9A3 File Offset: 0x00218BA3
		private void Awake()
		{
			EIU_ControlsMenu.instance = this;
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x0021A9AC File Offset: 0x00218BAC
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

		// Token: 0x06004FB8 RID: 20408 RVA: 0x0021AA04 File Offset: 0x00218C04
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

		// Token: 0x04004EA8 RID: 20136
		[HideInInspector]
		public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();

		// Token: 0x04004EA9 RID: 20137
		private Dictionary<string, int> defaultAxes = new Dictionary<string, int>();

		// Token: 0x04004EAA RID: 20138
		[Header("UI References")]
		[Space(5f)]
		public GameObject axisPrefab;

		// Token: 0x04004EAB RID: 20139
		public Transform controlsList;

		// Token: 0x04004EAC RID: 20140
		public EIU_RebindButton rebBtn;

		// Token: 0x04004EAD RID: 20141
		[HideInInspector]
		public bool rebinding;

		// Token: 0x04004EAE RID: 20142
		private bool negativeKey;

		// Token: 0x04004EAF RID: 20143
		private EIU_AxisBase targetAxis;

		// Token: 0x04004EB0 RID: 20144
		private bool initOnce;

		// Token: 0x04004EB1 RID: 20145
		public static EIU_ControlsMenu instance;
	}
}
