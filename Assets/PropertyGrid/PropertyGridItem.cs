using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace namudev
{
    public static class PropertyGridItem
    {
        public static Dictionary<Type,Type> TypeMap;

        static PropertyGridItem()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            IEnumerable<Type> derivedTypes = types.Where(t => t.BaseType != null);
            IEnumerable<Type> itemTypes = derivedTypes.Where(t => t.BaseType.IsGenericType && 
                (t.BaseType.GetGenericTypeDefinition() == typeof(PropertyGridItem<>)));

            TypeMap = new Dictionary<Type,Type>();
            foreach (Type type in itemTypes)
            {
                TypeMap[type.BaseType.GetGenericArguments()[0]] = type;
            }
        }
    }

    public abstract class PropertyGridItem<T> : MonoBehaviour
    {
#pragma warning disable 67
        public event EventHandler ValueChanged;
#pragma warning restore 67

        public string Caption { get { return binding.Caption; } }

        public T Value
        {
            get { return (T)binding.Value; }
            protected set { binding.SetValue(value); }
        }

        public Type ValueType { get { return binding.ValueType; } }

        private PropertyGridBinding binding;

        protected virtual void Awake()
        {
            binding = GetComponent<PropertyGridBinding>();
            binding.ValueChanged += (o, e) =>
            {
                if (ValueChanged != null)
                {
                    ValueChanged(this, EventArgs.Empty);
                }
            };
        }
    }
}
