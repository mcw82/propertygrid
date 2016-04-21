using UnityEngine;
using UnityEngine.UI;

namespace namudev
{
    public class PropertyGridString : PropertyGridItem<string>
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

            var inputField = controls.GetComponentInChildren<InputField>();
            inputField.text = Value;
            inputField.onValueChange.AddListener((string value) => { Value = value; });
        }
    }
}
