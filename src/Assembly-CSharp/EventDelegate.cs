using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000B8 RID: 184
[Serializable]
public class EventDelegate
{
	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00009E2D File Offset: 0x0000802D
	// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00009E35 File Offset: 0x00008035
	public MonoBehaviour target
	{
		get
		{
			return this.mTarget;
		}
		set
		{
			this.mTarget = value;
			this.mCachedCallback = null;
			this.mRawDelegate = false;
			this.mCached = false;
			this.mMethod = null;
			this.mParameters = null;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00009E61 File Offset: 0x00008061
	// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00009E69 File Offset: 0x00008069
	public string methodName
	{
		get
		{
			return this.mMethodName;
		}
		set
		{
			this.mMethodName = value;
			this.mCachedCallback = null;
			this.mRawDelegate = false;
			this.mCached = false;
			this.mMethod = null;
			this.mParameters = null;
		}
	}

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00009E95 File Offset: 0x00008095
	public EventDelegate.Parameter[] parameters
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			return this.mParameters;
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00009EAB File Offset: 0x000080AB
	public bool isValid
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			return (this.mRawDelegate && this.mCachedCallback != null) || (this.mTarget != null && !string.IsNullOrEmpty(this.mMethodName));
		}
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x060006DA RID: 1754 RVA: 0x00079620 File Offset: 0x00077820
	public bool isEnabled
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRawDelegate && this.mCachedCallback != null)
			{
				return true;
			}
			if (this.mTarget == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = this.mTarget;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0000403D File Offset: 0x0000223D
	public EventDelegate()
	{
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00009EEB File Offset: 0x000080EB
	public EventDelegate(EventDelegate.Callback call)
	{
		this.Set(call);
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00009EFA File Offset: 0x000080FA
	public EventDelegate(MonoBehaviour target, string methodName)
	{
		this.Set(target, methodName);
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x00009F0A File Offset: 0x0000810A
	private static string GetMethodName(EventDelegate.Callback callback)
	{
		return callback.Method.Name;
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00009F17 File Offset: 0x00008117
	private static bool IsValid(EventDelegate.Callback callback)
	{
		return callback != null && callback.Method != null;
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00079678 File Offset: 0x00077878
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !this.isValid;
		}
		if (obj is EventDelegate.Callback)
		{
			EventDelegate.Callback callback = obj as EventDelegate.Callback;
			if (callback.Equals(this.mCachedCallback))
			{
				return true;
			}
			MonoBehaviour monoBehaviour = callback.Target as MonoBehaviour;
			return this.mTarget == monoBehaviour && string.Equals(this.mMethodName, EventDelegate.GetMethodName(callback));
		}
		else
		{
			if (obj is EventDelegate)
			{
				EventDelegate eventDelegate = obj as EventDelegate;
				return this.mTarget == eventDelegate.mTarget && string.Equals(this.mMethodName, eventDelegate.mMethodName);
			}
			return false;
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00009F2A File Offset: 0x0000812A
	public override int GetHashCode()
	{
		return EventDelegate.s_Hash;
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x00079718 File Offset: 0x00077918
	private void Set(EventDelegate.Callback call)
	{
		this.Clear();
		if (call != null && EventDelegate.IsValid(call))
		{
			this.mTarget = (call.Target as MonoBehaviour);
			if (this.mTarget == null)
			{
				this.mRawDelegate = true;
				this.mCachedCallback = call;
				this.mMethodName = null;
				return;
			}
			this.mMethodName = EventDelegate.GetMethodName(call);
			this.mRawDelegate = false;
		}
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00009F31 File Offset: 0x00008131
	public void Set(MonoBehaviour target, string methodName)
	{
		this.Clear();
		this.mTarget = target;
		this.mMethodName = methodName;
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00079780 File Offset: 0x00077980
	private void Cache()
	{
		this.mCached = true;
		if (this.mRawDelegate)
		{
			return;
		}
		if ((this.mCachedCallback == null || this.mCachedCallback.Target as MonoBehaviour != this.mTarget || EventDelegate.GetMethodName(this.mCachedCallback) != this.mMethodName) && this.mTarget != null && !string.IsNullOrEmpty(this.mMethodName))
		{
			Type type = this.mTarget.GetType();
			this.mMethod = null;
			while (type != null)
			{
				try
				{
					this.mMethod = type.GetMethod(this.mMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (this.mMethod != null)
					{
						break;
					}
				}
				catch (Exception)
				{
				}
				type = type.BaseType;
			}
			if (this.mMethod == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Could not find method '",
					this.mMethodName,
					"' on ",
					this.mTarget.GetType()
				}), this.mTarget);
				return;
			}
			if (this.mMethod.ReturnType != typeof(void))
			{
				Debug.LogError(string.Concat(new object[]
				{
					this.mTarget.GetType(),
					".",
					this.mMethodName,
					" must have a 'void' return type."
				}), this.mTarget);
				return;
			}
			ParameterInfo[] parameters = this.mMethod.GetParameters();
			if (parameters.Length == 0)
			{
				this.mCachedCallback = (EventDelegate.Callback)Delegate.CreateDelegate(typeof(EventDelegate.Callback), this.mTarget, this.mMethodName);
				this.mArgs = null;
				this.mParameters = null;
				return;
			}
			this.mCachedCallback = null;
			if (this.mParameters == null || this.mParameters.Length != parameters.Length)
			{
				this.mParameters = new EventDelegate.Parameter[parameters.Length];
				int i = 0;
				int num = this.mParameters.Length;
				while (i < num)
				{
					this.mParameters[i] = new EventDelegate.Parameter();
					i++;
				}
			}
			int j = 0;
			int num2 = this.mParameters.Length;
			while (j < num2)
			{
				this.mParameters[j].expectedType = parameters[j].ParameterType;
				j++;
			}
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x000799C8 File Offset: 0x00077BC8
	public bool Execute()
	{
		if (!this.mCached)
		{
			this.Cache();
		}
		if (this.mCachedCallback != null)
		{
			this.mCachedCallback();
			return true;
		}
		if (this.mMethod != null)
		{
			if (this.mParameters == null || this.mParameters.Length == 0)
			{
				this.mMethod.Invoke(this.mTarget, null);
			}
			else
			{
				if (this.mArgs == null || this.mArgs.Length != this.mParameters.Length)
				{
					this.mArgs = new object[this.mParameters.Length];
				}
				int i = 0;
				int num = this.mParameters.Length;
				while (i < num)
				{
					this.mArgs[i] = this.mParameters[i].value;
					i++;
				}
				try
				{
					this.mMethod.Invoke(this.mTarget, this.mArgs);
				}
				catch (ArgumentException ex)
				{
					string text = "Error calling ";
					if (this.mTarget == null)
					{
						text += this.mMethod.Name;
					}
					else
					{
						text = string.Concat(new object[]
						{
							text,
							this.mTarget.GetType(),
							".",
							this.mMethod.Name
						});
					}
					text = text + ": " + ex.Message;
					text += "\n  Expected: ";
					ParameterInfo[] parameters = this.mMethod.GetParameters();
					if (parameters.Length == 0)
					{
						text += "no arguments";
					}
					else
					{
						text += parameters[0];
						for (int j = 1; j < parameters.Length; j++)
						{
							text = text + ", " + parameters[j].ParameterType;
						}
					}
					text += "\n  Received: ";
					if (this.mParameters.Length == 0)
					{
						text += "no arguments";
					}
					else
					{
						text += this.mParameters[0].type;
						for (int k = 1; k < this.mParameters.Length; k++)
						{
							text = text + ", " + this.mParameters[k].type;
						}
					}
					text += "\n";
					Debug.LogError(text);
				}
				int l = 0;
				int num2 = this.mArgs.Length;
				while (l < num2)
				{
					this.mArgs[l] = null;
					l++;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00009F47 File Offset: 0x00008147
	public void Clear()
	{
		this.mTarget = null;
		this.mMethodName = null;
		this.mRawDelegate = false;
		this.mCachedCallback = null;
		this.mParameters = null;
		this.mCached = false;
		this.mMethod = null;
		this.mArgs = null;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00079C3C File Offset: 0x00077E3C
	public override string ToString()
	{
		if (this.mTarget != null)
		{
			string text = this.mTarget.GetType().ToString();
			int num = text.LastIndexOf('.');
			if (num > 0)
			{
				text = text.Substring(num + 1);
			}
			if (!string.IsNullOrEmpty(this.methodName))
			{
				return text + "/" + this.methodName;
			}
			return text + "/[delegate]";
		}
		else
		{
			if (!this.mRawDelegate)
			{
				return null;
			}
			return "[delegate]";
		}
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00079CBC File Offset: 0x00077EBC
	public static void Execute(List<EventDelegate> list)
	{
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null)
				{
					eventDelegate.Execute();
					if (i >= list.Count)
					{
						break;
					}
					if (list[i] != eventDelegate)
					{
						continue;
					}
					if (eventDelegate.oneShot)
					{
						list.RemoveAt(i);
						continue;
					}
				}
			}
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x00079D14 File Offset: 0x00077F14
	public static bool IsValid(List<EventDelegate> list)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.isValid)
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00079D50 File Offset: 0x00077F50
	public static EventDelegate Set(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		if (list != null)
		{
			EventDelegate eventDelegate = new EventDelegate(callback);
			list.Clear();
			list.Add(eventDelegate);
			return eventDelegate;
		}
		return null;
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00009F81 File Offset: 0x00008181
	public static void Set(List<EventDelegate> list, EventDelegate del)
	{
		if (list != null)
		{
			list.Clear();
			list.Add(del);
		}
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x00009F93 File Offset: 0x00008193
	public static EventDelegate Add(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		return EventDelegate.Add(list, callback, false);
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x00079D78 File Offset: 0x00077F78
	public static EventDelegate Add(List<EventDelegate> list, EventDelegate.Callback callback, bool oneShot)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(callback))
				{
					return eventDelegate;
				}
				i++;
			}
			EventDelegate eventDelegate2 = new EventDelegate(callback);
			eventDelegate2.oneShot = oneShot;
			list.Add(eventDelegate2);
			return eventDelegate2;
		}
		Debug.LogWarning("Attempting to add a callback to a list that's null");
		return null;
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x00009F9D File Offset: 0x0000819D
	public static void Add(List<EventDelegate> list, EventDelegate ev)
	{
		EventDelegate.Add(list, ev, ev.oneShot);
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x00079DD4 File Offset: 0x00077FD4
	public static void Add(List<EventDelegate> list, EventDelegate ev, bool oneShot)
	{
		if (ev.mRawDelegate || ev.target == null || string.IsNullOrEmpty(ev.methodName))
		{
			EventDelegate.Add(list, ev.mCachedCallback, oneShot);
			return;
		}
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(ev))
				{
					return;
				}
				i++;
			}
			EventDelegate eventDelegate2 = new EventDelegate(ev.target, ev.methodName);
			eventDelegate2.oneShot = oneShot;
			if (ev.mParameters != null && ev.mParameters.Length != 0)
			{
				eventDelegate2.mParameters = new EventDelegate.Parameter[ev.mParameters.Length];
				for (int j = 0; j < ev.mParameters.Length; j++)
				{
					eventDelegate2.mParameters[j] = ev.mParameters[j];
				}
			}
			list.Add(eventDelegate2);
			return;
		}
		Debug.LogWarning("Attempting to add a callback to a list that's null");
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x00079EBC File Offset: 0x000780BC
	public static bool Remove(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(callback))
				{
					list.RemoveAt(i);
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x00079EBC File Offset: 0x000780BC
	public static bool Remove(List<EventDelegate> list, EventDelegate ev)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(ev))
				{
					list.RemoveAt(i);
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x0400051B RID: 1307
	[SerializeField]
	private MonoBehaviour mTarget;

	// Token: 0x0400051C RID: 1308
	[SerializeField]
	private string mMethodName;

	// Token: 0x0400051D RID: 1309
	[SerializeField]
	private EventDelegate.Parameter[] mParameters;

	// Token: 0x0400051E RID: 1310
	public bool oneShot;

	// Token: 0x0400051F RID: 1311
	[NonSerialized]
	private EventDelegate.Callback mCachedCallback;

	// Token: 0x04000520 RID: 1312
	[NonSerialized]
	private bool mRawDelegate;

	// Token: 0x04000521 RID: 1313
	[NonSerialized]
	private bool mCached;

	// Token: 0x04000522 RID: 1314
	[NonSerialized]
	private MethodInfo mMethod;

	// Token: 0x04000523 RID: 1315
	[NonSerialized]
	private object[] mArgs;

	// Token: 0x04000524 RID: 1316
	private static int s_Hash = "EventDelegate".GetHashCode();

	// Token: 0x020000B9 RID: 185
	[Serializable]
	public class Parameter
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x00009FBD File Offset: 0x000081BD
		public Parameter()
		{
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00009FD5 File Offset: 0x000081D5
		public Parameter(Object obj, string field)
		{
			this.obj = obj;
			this.field = field;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00079F00 File Offset: 0x00078100
		public object value
		{
			get
			{
				if (!this.cached)
				{
					this.cached = true;
					this.fieldInfo = null;
					this.propInfo = null;
					if (this.obj != null && !string.IsNullOrEmpty(this.field))
					{
						Type type = this.obj.GetType();
						this.propInfo = type.GetProperty(this.field);
						if (this.propInfo == null)
						{
							this.fieldInfo = type.GetField(this.field);
						}
					}
				}
				if (this.propInfo != null)
				{
					return this.propInfo.GetValue(this.obj, null);
				}
				if (this.fieldInfo != null)
				{
					return this.fieldInfo.GetValue(this.obj);
				}
				return this.obj;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00009FFB File Offset: 0x000081FB
		public Type type
		{
			get
			{
				if (this.obj == null)
				{
					return typeof(void);
				}
				return this.obj.GetType();
			}
		}

		// Token: 0x04000525 RID: 1317
		public Object obj;

		// Token: 0x04000526 RID: 1318
		public string field;

		// Token: 0x04000527 RID: 1319
		[NonSerialized]
		public Type expectedType = typeof(void);

		// Token: 0x04000528 RID: 1320
		[NonSerialized]
		public bool cached;

		// Token: 0x04000529 RID: 1321
		[NonSerialized]
		public PropertyInfo propInfo;

		// Token: 0x0400052A RID: 1322
		[NonSerialized]
		public FieldInfo fieldInfo;
	}

	// Token: 0x020000BA RID: 186
	// (Invoke) Token: 0x060006F8 RID: 1784
	public delegate void Callback();
}
