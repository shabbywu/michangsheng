using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D10 RID: 3344
	public class PropertyTableAssigner<T> : IPropertyTableAssigner
	{
		// Token: 0x06005D96 RID: 23958 RVA: 0x002630F2 File Offset: 0x002612F2
		public PropertyTableAssigner(params string[] expectedMissingProperties)
		{
			this.m_InternalAssigner = new PropertyTableAssigner(typeof(T), expectedMissingProperties);
		}

		// Token: 0x06005D97 RID: 23959 RVA: 0x00263110 File Offset: 0x00261310
		public void AddExpectedMissingProperty(string name)
		{
			this.m_InternalAssigner.AddExpectedMissingProperty(name);
		}

		// Token: 0x06005D98 RID: 23960 RVA: 0x0026311E File Offset: 0x0026131E
		public void AssignObject(T obj, Table data)
		{
			this.m_InternalAssigner.AssignObject(obj, data);
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x00263132 File Offset: 0x00261332
		public PropertyTableAssigner GetTypeUnsafeAssigner()
		{
			return this.m_InternalAssigner;
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x0026313A File Offset: 0x0026133A
		public void SetSubassignerForType(Type propertyType, IPropertyTableAssigner assigner)
		{
			this.m_InternalAssigner.SetSubassignerForType(propertyType, assigner);
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x00263149 File Offset: 0x00261349
		public void SetSubassigner<SubassignerType>(PropertyTableAssigner<SubassignerType> assigner)
		{
			this.m_InternalAssigner.SetSubassignerForType(typeof(SubassignerType), assigner);
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x00263161 File Offset: 0x00261361
		void IPropertyTableAssigner.AssignObjectUnchecked(object o, Table data)
		{
			this.AssignObject((T)((object)o), data);
		}

		// Token: 0x040053E2 RID: 21474
		private PropertyTableAssigner m_InternalAssigner;
	}
}
