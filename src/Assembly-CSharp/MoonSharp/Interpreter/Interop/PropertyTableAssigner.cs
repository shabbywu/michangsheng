using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010F4 RID: 4340
	public class PropertyTableAssigner<T> : IPropertyTableAssigner
	{
		// Token: 0x060068C5 RID: 26821 RVA: 0x00047D3A File Offset: 0x00045F3A
		public PropertyTableAssigner(params string[] expectedMissingProperties)
		{
			this.m_InternalAssigner = new PropertyTableAssigner(typeof(T), expectedMissingProperties);
		}

		// Token: 0x060068C6 RID: 26822 RVA: 0x00047D58 File Offset: 0x00045F58
		public void AddExpectedMissingProperty(string name)
		{
			this.m_InternalAssigner.AddExpectedMissingProperty(name);
		}

		// Token: 0x060068C7 RID: 26823 RVA: 0x00047D66 File Offset: 0x00045F66
		public void AssignObject(T obj, Table data)
		{
			this.m_InternalAssigner.AssignObject(obj, data);
		}

		// Token: 0x060068C8 RID: 26824 RVA: 0x00047D7A File Offset: 0x00045F7A
		public PropertyTableAssigner GetTypeUnsafeAssigner()
		{
			return this.m_InternalAssigner;
		}

		// Token: 0x060068C9 RID: 26825 RVA: 0x00047D82 File Offset: 0x00045F82
		public void SetSubassignerForType(Type propertyType, IPropertyTableAssigner assigner)
		{
			this.m_InternalAssigner.SetSubassignerForType(propertyType, assigner);
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x00047D91 File Offset: 0x00045F91
		public void SetSubassigner<SubassignerType>(PropertyTableAssigner<SubassignerType> assigner)
		{
			this.m_InternalAssigner.SetSubassignerForType(typeof(SubassignerType), assigner);
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x00047DA9 File Offset: 0x00045FA9
		void IPropertyTableAssigner.AssignObjectUnchecked(object o, Table data)
		{
			this.AssignObject((T)((object)o), data);
		}

		// Token: 0x04005FF9 RID: 24569
		private PropertyTableAssigner m_InternalAssigner;
	}
}
