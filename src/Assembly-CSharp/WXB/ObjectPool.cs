using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WXB
{
	// Token: 0x020009A9 RID: 2473
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x0002D54E File Offset: 0x0002B74E
		// (set) Token: 0x06003F04 RID: 16132 RVA: 0x0002D556 File Offset: 0x0002B756
		public int countAll { get; private set; }

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x0002D55F File Offset: 0x0002B75F
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06003F06 RID: 16134 RVA: 0x0002D56E File Offset: 0x0002B76E
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0002D57B File Offset: 0x0002B77B
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x001B8898 File Offset: 0x001B6A98
		public T Get()
		{
			T t;
			if (this.m_Stack.Count == 0)
			{
				t = Activator.CreateInstance<T>();
				int countAll = this.countAll;
				this.countAll = countAll + 1;
			}
			else
			{
				t = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet.Invoke(t);
			}
			return t;
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x001B88EC File Offset: 0x001B6AEC
		public void Release(T element)
		{
			if (this.m_Stack.Count > 0 && this.m_Stack.Peek() == element)
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease.Invoke(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x040038AD RID: 14509
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x040038AE RID: 14510
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x040038AF RID: 14511
		private readonly UnityAction<T> m_ActionOnRelease;
	}
}
