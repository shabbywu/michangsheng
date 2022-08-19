using System;
using System.Collections.Generic;
using UnityEngine;

namespace EIU
{
	// Token: 0x02000B27 RID: 2855
	public class EasyInputUtility : MonoBehaviour
	{
		// Token: 0x06004F99 RID: 20377 RVA: 0x00219F18 File Offset: 0x00218118
		private void Awake()
		{
			this.menu = Object.FindObjectOfType<EIU_ControlsMenu>();
			if (EasyInputUtility.instance != null)
			{
				Object.Destroy(base.gameObject);
			}
			else
			{
				EasyInputUtility.instance = this;
				Object.DontDestroyOnLoad(base.gameObject);
			}
			if (this.menu)
			{
				this.menu.Axes = this.Axes;
				this.menu.Init();
			}
			this.LoadAllAxes();
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x00219F8C File Offset: 0x0021818C
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

		// Token: 0x06004F9B RID: 20379 RVA: 0x0021A024 File Offset: 0x00218224
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

		// Token: 0x06004F9C RID: 20380 RVA: 0x0021A07C File Offset: 0x0021827C
		public bool GetButton(string name)
		{
			bool result = false;
			for (int i = 0; i < this.Axes.Count; i++)
			{
				if (string.Equals(this.Axes[i].axisName, name))
				{
					result = this.Axes[i].positive;
					result = Input.GetKey(this.Axes[i].positiveKey);
				}
			}
			return result;
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x0021A0E4 File Offset: 0x002182E4
		public bool GetButtonDown(string name)
		{
			bool result = false;
			for (int i = 0; i < this.Axes.Count; i++)
			{
				if (string.Equals(this.Axes[i].axisName, name))
				{
					result = Input.GetKeyDown(this.Axes[i].positiveKey);
				}
			}
			return result;
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x0021A13C File Offset: 0x0021833C
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

		// Token: 0x04004E95 RID: 20117
		public static EasyInputUtility instance;

		// Token: 0x04004E96 RID: 20118
		private EIU_ControlsMenu menu;

		// Token: 0x04004E97 RID: 20119
		[Header("Define All Axes and Buttons Here")]
		[Space(5f)]
		public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();
	}
}
