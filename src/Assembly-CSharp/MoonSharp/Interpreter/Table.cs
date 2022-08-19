using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CA5 RID: 3237
	public class Table : RefIdObject, IScriptPrivateResource
	{
		// Token: 0x06005A85 RID: 23173 RVA: 0x0025854C File Offset: 0x0025674C
		public Table(Script owner)
		{
			this.m_Values = new LinkedList<TablePair>();
			this.m_StringMap = new LinkedListIndex<string, TablePair>(this.m_Values);
			this.m_ArrayMap = new LinkedListIndex<int, TablePair>(this.m_Values);
			this.m_ValueMap = new LinkedListIndex<DynValue, TablePair>(this.m_Values);
			this.m_Owner = owner;
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x002585AC File Offset: 0x002567AC
		public Table(Script owner, params DynValue[] arrayValues) : this(owner)
		{
			for (int i = 0; i < arrayValues.Length; i++)
			{
				this.Set(DynValue.NewNumber((double)(i + 1)), arrayValues[i]);
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06005A87 RID: 23175 RVA: 0x002585E0 File Offset: 0x002567E0
		public Script OwnerScript
		{
			get
			{
				return this.m_Owner;
			}
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x002585E8 File Offset: 0x002567E8
		public void Clear()
		{
			this.m_Values.Clear();
			this.m_StringMap.Clear();
			this.m_ArrayMap.Clear();
			this.m_ValueMap.Clear();
			this.m_CachedLength = -1;
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x00258620 File Offset: 0x00256820
		private int GetIntegralKey(double d)
		{
			int num = (int)d;
			if (d >= 1.0 && d == (double)num)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x1700069F RID: 1695
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

		// Token: 0x170006A0 RID: 1696
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

		// Token: 0x06005A8E RID: 23182 RVA: 0x0025868C File Offset: 0x0025688C
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

		// Token: 0x06005A8F RID: 23183 RVA: 0x002586EB File Offset: 0x002568EB
		public void Append(DynValue value)
		{
			this.CheckScriptOwnership(value);
			this.PerformTableSet<int>(this.m_ArrayMap, this.Length + 1, DynValue.NewNumber((double)(this.Length + 1)), value, true, this.Length + 1);
		}

		// Token: 0x06005A90 RID: 23184 RVA: 0x00258720 File Offset: 0x00256920
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

		// Token: 0x06005A91 RID: 23185 RVA: 0x002587FE File Offset: 0x002569FE
		public void Set(string key, DynValue value)
		{
			if (key == null)
			{
				throw ScriptRuntimeException.TableIndexIsNil();
			}
			this.CheckScriptOwnership(value);
			this.PerformTableSet<string>(this.m_StringMap, key, DynValue.NewString(key), value, false, -1);
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x00258826 File Offset: 0x00256A26
		public void Set(int key, DynValue value)
		{
			this.CheckScriptOwnership(value);
			this.PerformTableSet<int>(this.m_ArrayMap, key, DynValue.NewNumber((double)key), value, true, -1);
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x00258848 File Offset: 0x00256A48
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

		// Token: 0x06005A94 RID: 23188 RVA: 0x002588CC File Offset: 0x00256ACC
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

		// Token: 0x06005A95 RID: 23189 RVA: 0x00258924 File Offset: 0x00256B24
		public void Set(object[] keys, DynValue value)
		{
			if (keys == null || keys.Length == 0)
			{
				throw ScriptRuntimeException.TableIndexIsNil();
			}
			object key;
			this.ResolveMultipleKeys(keys, out key).Set(key, value);
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x0025894E File Offset: 0x00256B4E
		public DynValue Get(string key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x00258960 File Offset: 0x00256B60
		public DynValue Get(int key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x00258972 File Offset: 0x00256B72
		public DynValue Get(DynValue key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x00258984 File Offset: 0x00256B84
		public DynValue Get(object key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x00258996 File Offset: 0x00256B96
		public DynValue Get(params object[] keys)
		{
			return this.RawGet(keys) ?? DynValue.Nil;
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x002589A8 File Offset: 0x00256BA8
		private static DynValue RawGetValue(LinkedListNode<TablePair> linkedListNode)
		{
			if (linkedListNode == null)
			{
				return null;
			}
			return linkedListNode.Value.Value;
		}

		// Token: 0x06005A9C RID: 23196 RVA: 0x002589C8 File Offset: 0x00256BC8
		public DynValue RawGet(string key)
		{
			return Table.RawGetValue(this.m_StringMap.Find(key));
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x002589DB File Offset: 0x00256BDB
		public DynValue RawGet(int key)
		{
			return Table.RawGetValue(this.m_ArrayMap.Find(key));
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x002589F0 File Offset: 0x00256BF0
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

		// Token: 0x06005A9F RID: 23199 RVA: 0x00258A48 File Offset: 0x00256C48
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

		// Token: 0x06005AA0 RID: 23200 RVA: 0x00258A98 File Offset: 0x00256C98
		public DynValue RawGet(params object[] keys)
		{
			if (keys == null || keys.Length == 0)
			{
				return null;
			}
			object key;
			return this.ResolveMultipleKeys(keys, out key).RawGet(key);
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x00258ABD File Offset: 0x00256CBD
		private bool PerformTableRemove<T>(LinkedListIndex<T, TablePair> listIndex, T key, bool isNumber)
		{
			bool flag = listIndex.Remove(key);
			if (flag && isNumber)
			{
				this.m_CachedLength = -1;
			}
			return flag;
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x00258AD2 File Offset: 0x00256CD2
		public bool Remove(string key)
		{
			return this.PerformTableRemove<string>(this.m_StringMap, key, false);
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x00258AE2 File Offset: 0x00256CE2
		public bool Remove(int key)
		{
			return this.PerformTableRemove<int>(this.m_ArrayMap, key, true);
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x00258AF4 File Offset: 0x00256CF4
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

		// Token: 0x06005AA5 RID: 23205 RVA: 0x00258B47 File Offset: 0x00256D47
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

		// Token: 0x06005AA6 RID: 23206 RVA: 0x00258B88 File Offset: 0x00256D88
		public bool Remove(params object[] keys)
		{
			object key;
			return keys != null && keys.Length != 0 && this.ResolveMultipleKeys(keys, out key).Remove(key);
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x00258BB0 File Offset: 0x00256DB0
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

		// Token: 0x06005AA8 RID: 23208 RVA: 0x00258C10 File Offset: 0x00256E10
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

		// Token: 0x06005AA9 RID: 23209 RVA: 0x00258CD8 File Offset: 0x00256ED8
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

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06005AAA RID: 23210 RVA: 0x00258D30 File Offset: 0x00256F30
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

		// Token: 0x06005AAB RID: 23211 RVA: 0x00258D90 File Offset: 0x00256F90
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

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06005AAC RID: 23212 RVA: 0x00258DE9 File Offset: 0x00256FE9
		// (set) Token: 0x06005AAD RID: 23213 RVA: 0x00258DF1 File Offset: 0x00256FF1
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

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06005AAE RID: 23214 RVA: 0x00258E06 File Offset: 0x00257006
		public IEnumerable<TablePair> Pairs
		{
			get
			{
				return from n in this.m_Values
				select new TablePair(n.Key, n.Value);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06005AAF RID: 23215 RVA: 0x00258E32 File Offset: 0x00257032
		public IEnumerable<DynValue> Keys
		{
			get
			{
				return from n in this.m_Values
				select n.Key;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06005AB0 RID: 23216 RVA: 0x00258E5E File Offset: 0x0025705E
		public IEnumerable<DynValue> Values
		{
			get
			{
				return from n in this.m_Values
				select n.Value;
			}
		}

		// Token: 0x0400528A RID: 21130
		private readonly LinkedList<TablePair> m_Values;

		// Token: 0x0400528B RID: 21131
		private readonly LinkedListIndex<DynValue, TablePair> m_ValueMap;

		// Token: 0x0400528C RID: 21132
		private readonly LinkedListIndex<string, TablePair> m_StringMap;

		// Token: 0x0400528D RID: 21133
		private readonly LinkedListIndex<int, TablePair> m_ArrayMap;

		// Token: 0x0400528E RID: 21134
		private readonly Script m_Owner;

		// Token: 0x0400528F RID: 21135
		private int m_InitArray;

		// Token: 0x04005290 RID: 21136
		private int m_CachedLength = -1;

		// Token: 0x04005291 RID: 21137
		private bool m_ContainsNilEntries;

		// Token: 0x04005292 RID: 21138
		private Table m_MetaTable;
	}
}
