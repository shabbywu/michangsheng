using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001072 RID: 4210
	public class Table : RefIdObject, IScriptPrivateResource
	{
		// Token: 0x0600656F RID: 25967 RVA: 0x00282F2C File Offset: 0x0028112C
		public Table(Script owner)
		{
			this.m_Values = new LinkedList<TablePair>();
			this.m_StringMap = new LinkedListIndex<string, TablePair>(this.m_Values);
			this.m_ArrayMap = new LinkedListIndex<int, TablePair>(this.m_Values);
			this.m_ValueMap = new LinkedListIndex<DynValue, TablePair>(this.m_Values);
			this.m_Owner = owner;
		}

		// Token: 0x06006570 RID: 25968 RVA: 0x00282F8C File Offset: 0x0028118C
		public Table(Script owner, params DynValue[] arrayValues) : this(owner)
		{
			for (int i = 0; i < arrayValues.Length; i++)
			{
				this.Set(DynValue.NewNumber((double)(i + 1)), arrayValues[i]);
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06006571 RID: 25969 RVA: 0x00045CCF File Offset: 0x00043ECF
		public Script OwnerScript
		{
			get
			{
				return this.m_Owner;
			}
		}

		// Token: 0x06006572 RID: 25970 RVA: 0x00045CD7 File Offset: 0x00043ED7
		public void Clear()
		{
			this.m_Values.Clear();
			this.m_StringMap.Clear();
			this.m_ArrayMap.Clear();
			this.m_ValueMap.Clear();
			this.m_CachedLength = -1;
		}

		// Token: 0x06006573 RID: 25971 RVA: 0x00282FC0 File Offset: 0x002811C0
		private int GetIntegralKey(double d)
		{
			int num = (int)d;
			if (d >= 1.0 && d == (double)num)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x170008FA RID: 2298
		public object this[params object[] keys]
		{
			get
			{
				return this.Get(keys).ToObject();
			}
			set
			{
				this.Set(keys, DynValue.FromObject(this.OwnerScript, value));
			}
		}

		// Token: 0x170008FB RID: 2299
		public object this[object key]
		{
			get
			{
				return this.Get(key).ToObject();
			}
			set
			{
				this.Set(key, DynValue.FromObject(this.OwnerScript, value));
			}
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x00282FE4 File Offset: 0x002811E4
		private Table ResolveMultipleKeys(object[] keys, out object key)
		{
			Table table = this;
			key = ((keys.Length != 0) ? keys[0] : null);
			for (int i = 1; i < keys.Length; i++)
			{
				DynValue dynValue = table.RawGet(key);
				if (dynValue == null)
				{
					throw new ScriptRuntimeException("Key '{0}' did not point to anything");
				}
				if (dynValue.Type != DataType.Table)
				{
					throw new ScriptRuntimeException("Key '{0}' did not point to a table");
				}
				table = dynValue.Table;
				key = keys[i];
			}
			return table;
		}

		// Token: 0x06006579 RID: 25977 RVA: 0x00045D52 File Offset: 0x00043F52
		public void Append(DynValue value)
		{
			this.CheckScriptOwnership(value);
			this.PerformTableSet<int>(this.m_ArrayMap, this.Length + 1, DynValue.NewNumber((double)(this.Length + 1)), value, true, this.Length + 1);
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x00283044 File Offset: 0x00281244
		private void PerformTableSet<T>(LinkedListIndex<T, TablePair> listIndex, T key, DynValue keyDynValue, DynValue value, bool isNumber, int appendKey)
		{
			TablePair tablePair = listIndex.Set(key, new TablePair(keyDynValue, value));
			if (this.m_ContainsNilEntries && value.IsNotNil() && (tablePair.Value == null || tablePair.Value.IsNil()))
			{
				this.CollectDeadKeys();
				return;
			}
			if (value.IsNil())
			{
				this.m_ContainsNilEntries = true;
				if (isNumber)
				{
					this.m_CachedLength = -1;
					return;
				}
			}
			else if (isNumber && (tablePair.Value == null || tablePair.Value.IsNilOrNan()))
			{
				if (appendKey >= 0)
				{
					LinkedListNode<TablePair> linkedListNode = this.m_ArrayMap.Find(appendKey + 1);
					if (linkedListNode == null || linkedListNode.Value.Value == null || linkedListNode.Value.Value.IsNil())
					{
						this.m_CachedLength++;
						return;
					}
					this.m_CachedLength = -1;
					return;
				}
				else
				{
					this.m_CachedLength = -1;
				}
			}
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x00045D87 File Offset: 0x00043F87
		public void Set(string key, DynValue value)
		{
			if (key == null)
			{
				throw ScriptRuntimeException.TableIndexIsNil();
			}
			this.CheckScriptOwnership(value);
			this.PerformTableSet<string>(this.m_StringMap, key, DynValue.NewString(key), value, false, -1);
		}

		// Token: 0x0600657C RID: 25980 RVA: 0x00045DAF File Offset: 0x00043FAF
		public void Set(int key, DynValue value)
		{
			this.CheckScriptOwnership(value);
			this.PerformTableSet<int>(this.m_ArrayMap, key, DynValue.NewNumber((double)key), value, true, -1);
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x00283124 File Offset: 0x00281324
		public void Set(DynValue key, DynValue value)
		{
			if (key.IsNilOrNan())
			{
				if (key.IsNil())
				{
					throw ScriptRuntimeException.TableIndexIsNil();
				}
				throw ScriptRuntimeException.TableIndexIsNaN();
			}
			else
			{
				if (key.Type == DataType.String)
				{
					this.Set(key.String, value);
					return;
				}
				if (key.Type == DataType.Number)
				{
					int integralKey = this.GetIntegralKey(key.Number);
					if (integralKey > 0)
					{
						this.Set(integralKey, value);
						return;
					}
				}
				this.CheckScriptOwnership(key);
				this.CheckScriptOwnership(value);
				this.PerformTableSet<DynValue>(this.m_ValueMap, key, key, value, false, -1);
				return;
			}
		}

		// Token: 0x0600657E RID: 25982 RVA: 0x002831A8 File Offset: 0x002813A8
		public void Set(object key, DynValue value)
		{
			if (key == null)
			{
				throw ScriptRuntimeException.TableIndexIsNil();
			}
			if (key is string)
			{
				this.Set((string)key, value);
				return;
			}
			if (key is int)
			{
				this.Set((int)key, value);
				return;
			}
			this.Set(DynValue.FromObject(this.OwnerScript, key), value);
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x00283200 File Offset: 0x00281400
		public void Set(object[] keys, DynValue value)
		{
			if (keys == null || keys.Length == 0)
			{
				throw ScriptRuntimeException.TableIndexIsNil();
			}
			object key;
			this.ResolveMultipleKeys(keys, out key).Set(key, value);
		}

		// Token: 0x06006580 RID: 25984 RVA: 0x00045DCF File Offset: 0x00043FCF
		public DynValue Get(string key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x00045DE1 File Offset: 0x00043FE1
		public DynValue Get(int key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x00045DF3 File Offset: 0x00043FF3
		public DynValue Get(DynValue key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x00045E05 File Offset: 0x00044005
		public DynValue Get(object key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06006584 RID: 25988 RVA: 0x00045E17 File Offset: 0x00044017
		public DynValue Get(params object[] keys)
		{
			return this.RawGet(keys) ?? DynValue.Nil;
		}

		// Token: 0x06006585 RID: 25989 RVA: 0x0028322C File Offset: 0x0028142C
		private static DynValue RawGetValue(LinkedListNode<TablePair> linkedListNode)
		{
			if (linkedListNode == null)
			{
				return null;
			}
			return linkedListNode.Value.Value;
		}

		// Token: 0x06006586 RID: 25990 RVA: 0x00045E29 File Offset: 0x00044029
		public DynValue RawGet(string key)
		{
			return Table.RawGetValue(this.m_StringMap.Find(key));
		}

		// Token: 0x06006587 RID: 25991 RVA: 0x00045E3C File Offset: 0x0004403C
		public DynValue RawGet(int key)
		{
			return Table.RawGetValue(this.m_ArrayMap.Find(key));
		}

		// Token: 0x06006588 RID: 25992 RVA: 0x0028324C File Offset: 0x0028144C
		public DynValue RawGet(DynValue key)
		{
			if (key.Type == DataType.String)
			{
				return this.RawGet(key.String);
			}
			if (key.Type == DataType.Number)
			{
				int integralKey = this.GetIntegralKey(key.Number);
				if (integralKey > 0)
				{
					return this.RawGet(integralKey);
				}
			}
			return Table.RawGetValue(this.m_ValueMap.Find(key));
		}

		// Token: 0x06006589 RID: 25993 RVA: 0x002832A4 File Offset: 0x002814A4
		public DynValue RawGet(object key)
		{
			if (key == null)
			{
				return null;
			}
			if (key is string)
			{
				return this.RawGet((string)key);
			}
			if (key is int)
			{
				return this.RawGet((int)key);
			}
			return this.RawGet(DynValue.FromObject(this.OwnerScript, key));
		}

		// Token: 0x0600658A RID: 25994 RVA: 0x002832F4 File Offset: 0x002814F4
		public DynValue RawGet(params object[] keys)
		{
			if (keys == null || keys.Length == 0)
			{
				return null;
			}
			object key;
			return this.ResolveMultipleKeys(keys, out key).RawGet(key);
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x00045E4F File Offset: 0x0004404F
		private bool PerformTableRemove<T>(LinkedListIndex<T, TablePair> listIndex, T key, bool isNumber)
		{
			bool flag = listIndex.Remove(key);
			if (flag && isNumber)
			{
				this.m_CachedLength = -1;
			}
			return flag;
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x00045E64 File Offset: 0x00044064
		public bool Remove(string key)
		{
			return this.PerformTableRemove<string>(this.m_StringMap, key, false);
		}

		// Token: 0x0600658D RID: 25997 RVA: 0x00045E74 File Offset: 0x00044074
		public bool Remove(int key)
		{
			return this.PerformTableRemove<int>(this.m_ArrayMap, key, true);
		}

		// Token: 0x0600658E RID: 25998 RVA: 0x0028331C File Offset: 0x0028151C
		public bool Remove(DynValue key)
		{
			if (key.Type == DataType.String)
			{
				return this.Remove(key.String);
			}
			if (key.Type == DataType.Number)
			{
				int integralKey = this.GetIntegralKey(key.Number);
				if (integralKey > 0)
				{
					return this.Remove(integralKey);
				}
			}
			return this.PerformTableRemove<DynValue>(this.m_ValueMap, key, false);
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x00045E84 File Offset: 0x00044084
		public bool Remove(object key)
		{
			if (key is string)
			{
				return this.Remove((string)key);
			}
			if (key is int)
			{
				return this.Remove((int)key);
			}
			return this.Remove(DynValue.FromObject(this.OwnerScript, key));
		}

		// Token: 0x06006590 RID: 26000 RVA: 0x00283370 File Offset: 0x00281570
		public bool Remove(params object[] keys)
		{
			object key;
			return keys != null && keys.Length != 0 && this.ResolveMultipleKeys(keys, out key).Remove(key);
		}

		// Token: 0x06006591 RID: 26001 RVA: 0x00283398 File Offset: 0x00281598
		public void CollectDeadKeys()
		{
			for (LinkedListNode<TablePair> linkedListNode = this.m_Values.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value.Value.IsNil())
				{
					this.Remove(linkedListNode.Value.Key);
				}
			}
			this.m_ContainsNilEntries = false;
			this.m_CachedLength = -1;
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x002833F8 File Offset: 0x002815F8
		public TablePair? NextKey(DynValue v)
		{
			if (v.IsNil())
			{
				LinkedListNode<TablePair> first = this.m_Values.First;
				if (first == null)
				{
					return new TablePair?(TablePair.Nil);
				}
				if (first.Value.Value.IsNil())
				{
					return this.NextKey(first.Value.Key);
				}
				return new TablePair?(first.Value);
			}
			else
			{
				if (v.Type == DataType.String)
				{
					return this.GetNextOf(this.m_StringMap.Find(v.String));
				}
				if (v.Type == DataType.Number)
				{
					int integralKey = this.GetIntegralKey(v.Number);
					if (integralKey > 0)
					{
						return this.GetNextOf(this.m_ArrayMap.Find(integralKey));
					}
				}
				return this.GetNextOf(this.m_ValueMap.Find(v));
			}
		}

		// Token: 0x06006593 RID: 26003 RVA: 0x002834C0 File Offset: 0x002816C0
		private TablePair? GetNextOf(LinkedListNode<TablePair> linkedListNode)
		{
			while (linkedListNode != null)
			{
				if (linkedListNode.Next == null)
				{
					return new TablePair?(TablePair.Nil);
				}
				linkedListNode = linkedListNode.Next;
				if (!linkedListNode.Value.Value.IsNil())
				{
					return new TablePair?(linkedListNode.Value);
				}
			}
			return null;
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x00283518 File Offset: 0x00281718
		public int Length
		{
			get
			{
				if (this.m_CachedLength < 0)
				{
					this.m_CachedLength = 0;
					int num = 1;
					while (this.m_ArrayMap.ContainsKey(num) && !this.m_ArrayMap.Find(num).Value.Value.IsNil())
					{
						this.m_CachedLength = num;
						num++;
					}
				}
				return this.m_CachedLength;
			}
		}

		// Token: 0x06006595 RID: 26005 RVA: 0x00283578 File Offset: 0x00281778
		internal void InitNextArrayKeys(DynValue val, bool lastpos)
		{
			if (val.Type == DataType.Tuple && lastpos)
			{
				foreach (DynValue val2 in val.Tuple)
				{
					this.InitNextArrayKeys(val2, true);
				}
				return;
			}
			int i = this.m_InitArray + 1;
			this.m_InitArray = i;
			this.Set(i, val.ToScalar());
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06006596 RID: 26006 RVA: 0x00045EC2 File Offset: 0x000440C2
		// (set) Token: 0x06006597 RID: 26007 RVA: 0x00045ECA File Offset: 0x000440CA
		public Table MetaTable
		{
			get
			{
				return this.m_MetaTable;
			}
			set
			{
				this.CheckScriptOwnership(this.m_MetaTable);
				this.m_MetaTable = value;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06006598 RID: 26008 RVA: 0x00045EDF File Offset: 0x000440DF
		public IEnumerable<TablePair> Pairs
		{
			get
			{
				return from n in this.m_Values
				select new TablePair(n.Key, n.Value);
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06006599 RID: 26009 RVA: 0x00045F0B File Offset: 0x0004410B
		public IEnumerable<DynValue> Keys
		{
			get
			{
				return from n in this.m_Values
				select n.Key;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600659A RID: 26010 RVA: 0x00045F37 File Offset: 0x00044137
		public IEnumerable<DynValue> Values
		{
			get
			{
				return from n in this.m_Values
				select n.Value;
			}
		}

		// Token: 0x04005E59 RID: 24153
		private readonly LinkedList<TablePair> m_Values;

		// Token: 0x04005E5A RID: 24154
		private readonly LinkedListIndex<DynValue, TablePair> m_ValueMap;

		// Token: 0x04005E5B RID: 24155
		private readonly LinkedListIndex<string, TablePair> m_StringMap;

		// Token: 0x04005E5C RID: 24156
		private readonly LinkedListIndex<int, TablePair> m_ArrayMap;

		// Token: 0x04005E5D RID: 24157
		private readonly Script m_Owner;

		// Token: 0x04005E5E RID: 24158
		private int m_InitArray;

		// Token: 0x04005E5F RID: 24159
		private int m_CachedLength = -1;

		// Token: 0x04005E60 RID: 24160
		private bool m_ContainsNilEntries;

		// Token: 0x04005E61 RID: 24161
		private Table m_MetaTable;
	}
}
