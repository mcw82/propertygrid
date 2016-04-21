using UnityEngine;
using UnityEngine.UI;

namespace namudev
{
    public class PropertyGridVector2 : PropertyGridItem<Vector2>
    {
        private GameObject caption;
        private GameObject inputFieldX;
        private GameObject inputFieldY;

        protected override void Awake()
        {
            base.Awake();

            caption = transform.Find("Caption").gameObject;
            inputFieldX = transform.Find("Controls/InputFields/InputFieldX").gameObject;
            inputFieldY = transform.Find("Controls/InputFields/InputFieldY").gameObject;
        }

        private void Start()
        {
            caption.GetComponentInChildren<Text>().text = Caption;

            InputField x = inputFieldX.GetComponentInChildren<InputField>();
            x.text = Value.x.ToString();
            x.onValueChange.AddListener(OnValueChangeX);
            x.onEndEdit.AddListener(OnEndEditX);

            InputField y = inputFieldY.GetComponentInChildren<InputField>();
            y.text = Value.y.ToString();
            y.onValueChange.AddListener(OnValueChangeY);
            y.onEndEdit.AddListener(OnEndEditY);
        }

        private void OnValueChangeX(string str)
        {
            float x;
            if (float.TryParse(str, out x))
            {
                Vector2 v = Value;
                Value = new Vector2(x, v.y);
            }
        }

        private void OnEndEditX(string str)
        {
            InputField x = inputFieldX.GetComponentInChildren<InputField>();
            x.onValueChange.RemoveListener(OnValueChangeX);
            x.text = Value.x.ToString();
            x.onValueChange.AddListener(OnValueChangeX);
        }

        private void OnValueChangeY(string str)
        {
            float y;
            if (float.TryParse(str, out y))
            {
                Vector2 v = Value;
                Value = new Vector2(v.x, y);
            }
        }

        private void OnEndEditY(string str)
        {
            InputField y = inputFieldY.GetComponentInChildren<InputField>();
            y.onValueChange.RemoveListener(OnValueChangeY);
            y.text = Value.y.ToString();
            y.onValueChange.AddListener(OnValueChangeY);
        }
    }
}
