using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WXB
{
	// Token: 0x02000695 RID: 1685
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x001706B6 File Offset: 0x0016E8B6
		// (set) Token: 0x06003546 RID: 13638 RVA: 0x001706BE File Offset: 0x0016E8BE
		public int countAll { get; private set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x001706C7 File Offset: 0x0016E8C7
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x001706D6 File Offset: 0x0016E8D6
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x001706E3 File Offset: 0x0016E8E3
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x00170704 File Offset: 0x0016E904
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

		// Token: 0x0600354B RID: 13643 RVA: 0x00170758 File Offset: 0x0016E958
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

		// Token: 0x04002EF4 RID: 12020
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x04002EF5 RID: 12021
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x04002EF6 RID: 12022
		private readonly UnityAction<T> m_ActionOnRelease;
	}
}
