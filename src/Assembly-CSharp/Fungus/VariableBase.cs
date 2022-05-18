using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012FF RID: 4863
	public abstract class VariableBase<T> : Variable
	{
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600768E RID: 30350 RVA: 0x002B35F8 File Offset: 0x002B17F8
		private VariableBase<T> globalStaicRef
		{
			get
			{
				if (this._globalStaicRef != null)
				{
					return this._globalStaicRef;
				}
				if (Application.isPlaying)
				{
					return this._globalStaicRef = FungusManager.Instance.GlobalVariables.GetOrAddVariable<T>(this.Key, this.value, base.GetType());
				}
				return null;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600768F RID: 30351 RVA: 0x00050B1F File Offset: 0x0004ED1F
		// (set) Token: 0x06007690 RID: 30352 RVA: 0x00050B43 File Offset: 0x0004ED43
		public virtual T Value
		{
			get
			{
				if (this.scope != VariableScope.Global || !Application.isPlaying)
				{
					return this.value;
				}
				return this.globalStaicRef.value;
			}
			set
			{
				if (this.scope != VariableScope.Global || !Application.isPlaying)
				{
					this.value = value;
					return;
				}
				this.globalStaicRef.Value = value;
			}
		}

		// Token: 0x06007691 RID: 30353 RVA: 0x00050B69 File Offset: 0x0004ED69
		public override void OnReset()
		{
			this.Value = this.startValue;
		}

		// Token: 0x06007692 RID: 30354 RVA: 0x002B3650 File Offset: 0x002B1850
		public override string ToString()
		{
			T t = this.Value;
			return t.ToString();
		}

		// Token: 0x06007693 RID: 30355 RVA: 0x00050B77 File Offset: 0x0004ED77
		protected virtual void Start()
		{
			this.startValue = this.Value;
		}

		// Token: 0x06007694 RID: 30356 RVA: 0x00050B85 File Offset: 0x0004ED85
		public virtual void Apply(SetOperator setOperator, T value)
		{
			Debug.LogError("Variable doesn't have any operators.");
		}

		// Token: 0x04006766 RID: 26470
		private VariableBase<T> _globalStaicRef;

		// Token: 0x04006767 RID: 26471
		[SerializeField]
		protected T value;

		// Token: 0x04006768 RID: 26472
		protected T startValue;
	}
}
