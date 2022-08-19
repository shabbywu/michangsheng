using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E8E RID: 3726
	public abstract class VariableBase<T> : Variable
	{
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600698F RID: 27023 RVA: 0x0029131C File Offset: 0x0028F51C
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

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06006990 RID: 27024 RVA: 0x00291371 File Offset: 0x0028F571
		// (set) Token: 0x06006991 RID: 27025 RVA: 0x00291395 File Offset: 0x0028F595
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

		// Token: 0x06006992 RID: 27026 RVA: 0x002913BB File Offset: 0x0028F5BB
		public override void OnReset()
		{
			this.Value = this.startValue;
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x002913CC File Offset: 0x0028F5CC
		public override string ToString()
		{
			T t = this.Value;
			return t.ToString();
		}

		// Token: 0x06006994 RID: 27028 RVA: 0x002913ED File Offset: 0x0028F5ED
		protected virtual void Start()
		{
			this.startValue = this.Value;
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x002913FB File Offset: 0x0028F5FB
		public virtual void Apply(SetOperator setOperator, T value)
		{
			Debug.LogError("Variable doesn't have any operators.");
		}

		// Token: 0x04005985 RID: 22917
		private VariableBase<T> _globalStaicRef;

		// Token: 0x04005986 RID: 22918
		[SerializeField]
		protected T value;

		// Token: 0x04005987 RID: 22919
		protected T startValue;
	}
}
