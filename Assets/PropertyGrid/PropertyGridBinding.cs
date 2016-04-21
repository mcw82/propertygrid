using System;
using System.Reflection;
using UnityEngine;

namespace namudev
{
    public class PropertyGridBinding : MonoBehaviour
    {
        public event EventHandler ValueChanged;

        public string Caption { get; private set; }
        public object Value { get; private set; }
        public Type ValueType { get; private set; } 

        private object targetObject;
        private PropertyInfo propertyInfo;

        public void Initialize(string caption, object value, Type valueType)
        {
            Caption = caption;
            Value = value;
            ValueType = valueType;
        }

        public void Initialize(object targetObject, PropertyInfo propertyInfo)
        {
            this.targetObject = targetObject;
            this.propertyInfo = propertyInfo;
            Caption = propertyInfo.Name;
            Value = propertyInfo.GetValue(targetObject, null);
            ValueType = propertyInfo.PropertyType;
        }

        public void SetValue(object value)
        {
            if (!Equals(this.Value, value))
            {
                Value = value;
                if (ValueChanged != null)
                {
                    ValueChanged(this, EventArgs.Empty);
                }
                if ((targetObject != null) && (propertyInfo != null))
                {
                    propertyInfo.SetValue(targetObject, Value, null);
                }
            }
        }
    }
}
