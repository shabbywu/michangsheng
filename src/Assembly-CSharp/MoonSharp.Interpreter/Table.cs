using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter;

public class Table : RefIdObject, IScriptPrivateResource
{
	private readonly LinkedList<TablePair> m_Values;

	private readonly LinkedListIndex<DynValue, TablePair> m_ValueMap;

	private readonly LinkedListIndex<string, TablePair> m_StringMap;

	private readonly LinkedListIndex<int, TablePair> m_ArrayMap;

	private readonly Script m_Owner;

	private int m_InitArray;

	private int m_CachedLength = -1;

	private bool m_ContainsNilEntries;

	private Table m_MetaTable;

	public Script OwnerScript => m_Owner;

	public object this[params object[] keys]
	{
		get
		{
			return Get(keys).ToObject();
		}
		set
		{
			Set(keys, DynValue.FromObject(OwnerScript, value));
		}
	}

	public object this[object key]
	{
		get
		{
			return Get(key).ToObject();
		}
		set
		{
			Set(key, DynValue.FromObject(OwnerScript, value));
		}
	}

	public int Length
	{
		get
		{
			if (m_CachedLength < 0)
			{
				m_CachedLength = 0;
				for (int i = 1; m_ArrayMap.ContainsKey(i) && !m_ArrayMap.Find(i).Value.Value.IsNil(); i++)
				{
					m_CachedLength = i;
				}
			}
			return m_CachedLength;
		}
	}

	public Table MetaTable
	{
		get
		{
			return m_MetaTable;
		}
		set
		{
			this.CheckScriptOwnership(m_MetaTable);
			m_MetaTable = value;
		}
	}

	public IEnumerable<TablePair> Pairs => m_Values.Select((TablePair n) => new TablePair(n.Key, n.Value));

	public IEnumerable<DynValue> Keys => m_Values.Select((TablePair n) => n.Key);

	public IEnumerable<DynValue> Values => m_Values.Select((TablePair n) => n.Value);

	public Table(Script owner)
	{
		m_Values = new LinkedList<TablePair>();
		m_StringMap = new LinkedListIndex<string, TablePair>(m_Values);
		m_ArrayMap = new LinkedListIndex<int, TablePair>(m_Values);
		m_ValueMap = new LinkedListIndex<DynValue, TablePair>(m_Values);
		m_Owner = owner;
	}

	public Table(Script owner, params DynValue[] arrayValues)
		: this(owner)
	{
		for (int i = 0; i < arrayValues.Length; i++)
		{
			Set(DynValue.NewNumber(i + 1), arrayValues[i]);
		}
	}

	public void Clear()
	{
		m_Values.Clear();
		m_StringMap.Clear();
		m_ArrayMap.Clear();
		m_ValueMap.Clear();
		m_CachedLength = -1;
	}

	private int GetIntegralKey(double d)
	{
		int num = (int)d;
		if (d >= 1.0 && d == (double)num)
		{
			return num;
		}
		return -1;
	}

	private Table ResolveMultipleKeys(object[] keys, out object key)
	{
		Table table = this;
		key = ((keys.Length != 0) ? keys[0] : null);
		for (int i = 1; i < keys.Length; i++)
		{
			DynValue obj = table.RawGet(key) ?? throw new ScriptRuntimeException("Key '{0}' did not point to anything");
			if (obj.Type != DataType.Table)
			{
				throw new ScriptRuntimeException("Key '{0}' did not point to a table");
			}
			table = obj.Table;
			key = keys[i];
		}
		return table;
	}

	public void Append(DynValue value)
	{
		this.CheckScriptOwnership(value);
		PerformTableSet(m_ArrayMap, Length + 1, DynValue.NewNumber(Length + 1), value, isNumber: true, Length + 1);
	}

	private void PerformTableSet<T>(LinkedListIndex<T, TablePair> listIndex, T key, DynValue keyDynValue, DynValue value, bool isNumber, int appendKey)
	{
		TablePair tablePair = listIndex.Set(key, new TablePair(keyDynValue, value));
		if (m_ContainsNilEntries && value.IsNotNil() && (tablePair.Value == null || tablePair.Value.IsNil()))
		{
			CollectDeadKeys();
		}
		else if (value.IsNil())
		{
			m_ContainsNilEntries = true;
			if (isNumber)
			{
				m_CachedLength = -1;
			}
		}
		else
		{
			if (!isNumber || (tablePair.Value != null && !tablePair.Value.IsNilOrNan()))
			{
				return;
			}
			if (appendKey >= 0)
			{
				LinkedListNode<TablePair> linkedListNode = m_ArrayMap.Find(appendKey + 1);
				if (linkedListNode == null || linkedListNode.Value.Value == null || linkedListNode.Value.Value.IsNil())
				{
					m_CachedLength++;
				}
				else
				{
					m_CachedLength = -1;
				}
			}
			else
			{
				m_CachedLength = -1;
			}
		}
	}

	public void Set(string key, DynValue value)
	{
		if (key == null)
		{
			throw ScriptRuntimeException.TableIndexIsNil();
		}
		this.CheckScriptOwnership(value);
		PerformTableSet(m_StringMap, key, DynValue.NewString(key), value, isNumber: false, -1);
	}

	public void Set(int key, DynValue value)
	{
		this.CheckScriptOwnership(value);
		PerformTableSet(m_ArrayMap, key, DynValue.NewNumber(key), value, isNumber: true, -1);
	}

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
		if (key.Type == DataType.String)
		{
			Set(key.String, value);
			return;
		}
		if (key.Type == DataType.Number)
		{
			int integralKey = GetIntegralKey(key.Number);
			if (integralKey > 0)
			{
				Set(integralKey, value);
				return;
			}
		}
		this.CheckScriptOwnership(key);
		this.CheckScriptOwnership(value);
		PerformTableSet(m_ValueMap, key, key, value, isNumber: false, -1);
	}

	public void Set(object key, DynValue value)
	{
		if (key == null)
		{
			throw ScriptRuntimeException.TableIndexIsNil();
		}
		if (key is string)
		{
			Set((string)key, value);
		}
		else if (key is int)
		{
			Set((int)key, value);
		}
		else
		{
			Set(DynValue.FromObject(OwnerScript, key), value);
		}
	}

	public void Set(object[] keys, DynValue value)
	{
		if (keys == null || keys.Length == 0)
		{
			throw ScriptRuntimeException.TableIndexIsNil();
		}
		ResolveMultipleKeys(keys, out var key).Set(key, value);
	}

	public DynValue Get(string key)
	{
		return RawGet(key) ?? DynValue.Nil;
	}

	public DynValue Get(int key)
	{
		return RawGet(key) ?? DynValue.Nil;
	}

	public DynValue Get(DynValue key)
	{
		return RawGet(key) ?? DynValue.Nil;
	}

	public DynValue Get(object key)
	{
		return RawGet(key) ?? DynValue.Nil;
	}

	public DynValue Get(params object[] keys)
	{
		return RawGet(keys) ?? DynValue.Nil;
	}

	private static DynValue RawGetValue(LinkedListNode<TablePair> linkedListNode)
	{
		return linkedListNode?.Value.Value;
	}

	public DynValue RawGet(string key)
	{
		return RawGetValue(m_StringMap.Find(key));
	}

	public DynValue RawGet(int key)
	{
		return RawGetValue(m_ArrayMap.Find(key));
	}

	public DynValue RawGet(DynValue key)
	{
		if (key.Type == DataType.String)
		{
			return RawGet(key.String);
		}
		if (key.Type == DataType.Number)
		{
			int integralKey = GetIntegralKey(key.Number);
			if (integralKey > 0)
			{
				return RawGet(integralKey);
			}
		}
		return RawGetValue(m_ValueMap.Find(key));
	}

	public DynValue RawGet(object key)
	{
		if (key == null)
		{
			return null;
		}
		if (key is string)
		{
			return RawGet((string)key);
		}
		if (key is int)
		{
			return RawGet((int)key);
		}
		return RawGet(DynValue.FromObject(OwnerScript, key));
	}

	public DynValue RawGet(params object[] keys)
	{
		if (keys == null || keys.Length == 0)
		{
			return null;
		}
		object key;
		return ResolveMultipleKeys(keys, out key).RawGet(key);
	}

	private bool PerformTableRemove<T>(LinkedListIndex<T, TablePair> listIndex, T key, bool isNumber)
	{
		bool num = listIndex.Remove(key);
		if (num && isNumber)
		{
			m_CachedLength = -1;
		}
		return num;
	}

	public bool Remove(string key)
	{
		return PerformTableRemove(m_StringMap, key, isNumber: false);
	}

	public bool Remove(int key)
	{
		return PerformTableRemove(m_ArrayMap, key, isNumber: true);
	}

	public bool Remove(DynValue key)
	{
		if (key.Type == DataType.String)
		{
			return Remove(key.String);
		}
		if (key.Type == DataType.Number)
		{
			int integralKey = GetIntegralKey(key.Number);
			if (integralKey > 0)
			{
				return Remove(integralKey);
			}
		}
		return PerformTableRemove(m_ValueMap, key, isNumber: false);
	}

	public bool Remove(object key)
	{
		if (key is string)
		{
			return Remove((string)key);
		}
		if (key is int)
		{
			return Remove((int)key);
		}
		return Remove(DynValue.FromObject(OwnerScript, key));
	}

	public bool Remove(params object[] keys)
	{
		if (keys == null || keys.Length == 0)
		{
			return false;
		}
		object key;
		return ResolveMultipleKeys(keys, out key).Remove(key);
	}

	public void CollectDeadKeys()
	{
		for (LinkedListNode<TablePair> linkedListNode = m_Values.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.Value.IsNil())
			{
				Remove(linkedListNode.Value.Key);
			}
		}
		m_ContainsNilEntries = false;
		m_CachedLength = -1;
	}

	public TablePair? NextKey(DynValue v)
	{
		if (v.IsNil())
		{
			LinkedListNode<TablePair> first = m_Values.First;
			if (first == null)
			{
				return TablePair.Nil;
			}
			if (first.Value.Value.IsNil())
			{
				return NextKey(first.Value.Key);
			}
			return first.Value;
		}
		if (v.Type == DataType.String)
		{
			return GetNextOf(m_StringMap.Find(v.String));
		}
		if (v.Type == DataType.Number)
		{
			int integralKey = GetIntegralKey(v.Number);
			if (integralKey > 0)
			{
				return GetNextOf(m_ArrayMap.Find(integralKey));
			}
		}
		return GetNextOf(m_ValueMap.Find(v));
	}

	private TablePair? GetNextOf(LinkedListNode<TablePair> linkedListNode)
	{
		do
		{
			if (linkedListNode == null)
			{
				return null;
			}
			if (linkedListNode.Next == null)
			{
				return TablePair.Nil;
			}
			linkedListNode = linkedListNode.Next;
		}
		while (linkedListNode.Value.Value.IsNil());
		return linkedListNode.Value;
	}

	internal void InitNextArrayKeys(DynValue val, bool lastpos)
	{
		if (val.Type == DataType.Tuple && lastpos)
		{
			DynValue[] tuple = val.Tuple;
			foreach (DynValue val2 in tuple)
			{
				InitNextArrayKeys(val2, lastpos: true);
			}
		}
		else
		{
			Set(++m_InitArray, val.ToScalar());
		}
	}
}
