using UnityEngine;
using UnityEngine.UI;

namespace namudev
{
    public class PropertyGridFloat : PropertyGridItem<float>
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
            inputField.text = Value.ToString();
            inputField.onValueChange.AddListener(OnValueChange);
            inputField.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnValueChange(string str)
        {
            float f;
            if (float.TryParse(str, out f))
            {
                Value = f;
            }
        }

        private void OnEndEdit(string str)
        {
            var inputField = controls.GetComponentInChildren<InputField>();
            inputField.onValueChange.RemoveListener(OnValueChange);
            inputField.text = Value.ToString();
            inputField.onValueChange.AddListener(OnValueChange);
        }
    }
}
