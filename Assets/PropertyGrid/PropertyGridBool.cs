using UnityEngine;
using UnityEngine.UI;

namespace namudev
{
    public class PropertyGridBool : PropertyGridItem<bool>
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

            var toggle = controls.GetComponentInChildren<Toggle>();
            toggle.isOn = Value;
            toggle.onValueChanged.AddListener((bool value) => { Value = value; });
        }
    }
}
