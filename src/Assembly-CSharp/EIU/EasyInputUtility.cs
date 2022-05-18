using System;
using System.Collections.Generic;
using UnityEngine;

namespace EIU
{
	// Token: 0x02000E9B RID: 3739
	public class EasyInputUtility : MonoBehaviour
	{
		// Token: 0x060059B8 RID: 22968 RVA: 0x00249CD0 File Offset: 0x00247ED0
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

		// Token: 0x060059B9 RID: 22969 RVA: 0x00249D44 File Offset: 0x00247F44
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

		// Token: 0x060059BA RID: 22970 RVA: 0x00249DDC File Offset: 0x00247FDC
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

		// Token: 0x060059BB RID: 22971 RVA: 0x00249E34 File Offset: 0x00248034
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

		// Token: 0x060059BC RID: 22972 RVA: 0x00249E9C File Offset: 0x0024809C
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

		// Token: 0x060059BD RID: 22973 RVA: 0x00249EF4 File Offset: 0x002480F4
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

		// Token: 0x0400590C RID: 22796
		public static EasyInputUtility instance;

		// Token: 0x0400590D RID: 22797
		private EIU_ControlsMenu menu;

		// Token: 0x0400590E RID: 22798
		[Header("Define All Axes and Buttons Here")]
		[Space(5f)]
		public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();
	}
}
