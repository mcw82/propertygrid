using UnityEngine;
using UnityEngine.UI;

namespace namudev
{
    public class PropertyGridVector4 : PropertyGridItem<Vector4>
    {
        private GameObject caption;
        private GameObject inputFieldX;
        private GameObject inputFieldY;
        private GameObject inputFieldZ;
        private GameObject inputFieldW;

        protected override void Awake()
        {
            base.Awake();

            caption = transform.Find("Caption").gameObject;
            inputFieldX = transform.Find("Controls/InputFields/InputFieldX").gameObject;
            inputFieldY = transform.Find("Controls/InputFields/InputFieldY").gameObject;
            inputFieldZ = transform.Find("Controls/InputFields/InputFieldZ").gameObject;
            inputFieldW = transform.Find("Controls/InputFields/InputFieldW").gameObject;
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

            InputField z = inputFieldZ.GetComponentInChildren<InputField>();
            z.text = Value.z.ToString();
            z.onValueChange.AddListener(OnValueChangeZ);
            z.onEndEdit.AddListener(OnEndEditZ);

            InputField w = inputFieldW.GetComponentInChildren<InputField>();
            w.text = Value.w.ToString();
            w.onValueChange.AddListener(OnValueChangeW);
            w.onEndEdit.AddListener(OnEndEditW);
        }

        private void OnValueChangeX(string str)
        {
            float x;
            if (float.TryParse(str, out x))
            {
                Vector4 v = Value;
                Value = new Vector4(x, v.y, v.z, v.w);
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
                Vector4 v = Value;
                Value = new Vector4(v.x, y, v.z, v.w);
            }
        }

        private void OnEndEditY(string str)
        {
            InputField y = inputFieldY.GetComponentInChildren<InputField>();
            y.onValueChange.RemoveListener(OnValueChangeY);
            y.text = Value.y.ToString();
            y.onValueChange.AddListener(OnValueChangeY);
        }

        private void OnValueChangeZ(string str)
        {
            float z;
            if (float.TryParse(str, out z))
            {
                Vector4 v = Value;
                Value = new Vector4(v.x, v.y, z, v.w);
            }
        }

        private void OnEndEditZ(string str)
        {
            InputField z = inputFieldZ.GetComponentInChildren<InputField>();
            z.onValueChange.RemoveListener(OnValueChangeZ);
            z.text = Value.z.ToString();
            z.onValueChange.AddListener(OnValueChangeZ);
        }

        private void OnValueChangeW(string str)
        {
            float w;
            if (float.TryParse(str, out w))
            {
                Vector4 v = Value;
                Value = new Vector4(v.x, v.y, v.z, w);
            }
        }

        private void OnEndEditW(string str)
        {
            InputField w = inputFieldW.GetComponentInChildren<InputField>();
            w.onValueChange.RemoveListener(OnValueChangeW);
            w.text = Value.w.ToString();
            w.onValueChange.AddListener(OnValueChangeW);
        }
    }
}
