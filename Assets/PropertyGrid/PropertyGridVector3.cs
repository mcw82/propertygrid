using UnityEngine;
using UnityEngine.UI;

namespace namudev
{
    public class PropertyGridVector3 : PropertyGridItem<Vector3>
    {
        private GameObject caption;
        private GameObject inputFieldX;
        private GameObject inputFieldY;
        private GameObject inputFieldZ;

        protected override void Awake()
        {
            base.Awake();

            caption = transform.Find("Caption").gameObject;
            inputFieldX = transform.Find("Controls/InputFields/InputFieldX").gameObject;
            inputFieldY = transform.Find("Controls/InputFields/InputFieldY").gameObject;
            inputFieldZ = transform.Find("Controls/InputFields/InputFieldZ").gameObject;
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
        }

        private void OnValueChangeX(string str)
        {
            float x;
            if (float.TryParse(str, out x))
            {
                Vector3 v = Value;
                Value = new Vector3(x, v.y, v.z);
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
                Vector3 v = Value;
                Value = new Vector3(v.x, y, v.z);
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
                Vector3 v = Value;
                Value = new Vector3(v.x, v.y, z);
            }
        }

        private void OnEndEditZ(string str)
        {
            InputField z = inputFieldZ.GetComponentInChildren<InputField>();
            z.onValueChange.RemoveListener(OnValueChangeZ);
            z.text = Value.z.ToString();
            z.onValueChange.AddListener(OnValueChangeZ);
        }
    }
}
