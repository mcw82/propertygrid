using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace namudev
{
    public class PropertyGridEnum : PropertyGridItem<Enum>
    {
        private GameObject caption;
        private GameObject controls;

        protected override void Awake()
        {
            base.Awake();

            caption = transform.Find("Caption").gameObject;
            controls = transform.Find("Controls").gameObject;
        }

        private void Start()
        {
            caption.GetComponentInChildren<Text>().text = Caption;

            var dropdown = controls.GetComponentInChildren<Dropdown>();
            var options = new List<Dropdown.OptionData>();
            var strings = new List<string>();
            var values = new List<Enum>();
            foreach (var e in Enum.GetValues(ValueType))
            {
                options.Add(new Dropdown.OptionData(e.ToString()));
                strings.Add(e.ToString());
                values.Add((Enum)e);
            }
            dropdown.options = options;
            dropdown.value = strings.IndexOf(Value.ToString());
            dropdown.onValueChanged.AddListener((int index) => { Value = values[index]; });
        }
    }   
}
