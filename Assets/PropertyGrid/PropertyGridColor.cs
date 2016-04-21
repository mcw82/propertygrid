using UnityEngine;
using UnityEngine.UI;

namespace namudev
{
    public class PropertyGridColor : PropertyGridItem<Color>
    {
        private GameObject caption;
        private GameObject inputFieldR;
        private GameObject inputFieldG;
        private GameObject inputFieldB;
        private GameObject inputFieldA;

        protected override void Awake()
        {
            base.Awake();

            caption = transform.Find("Caption").gameObject;
            inputFieldR = transform.Find("Controls/InputFields/InputFieldR").gameObject;
            inputFieldG = transform.Find("Controls/InputFields/InputFieldG").gameObject;
            inputFieldB = transform.Find("Controls/InputFields/InputFieldB").gameObject;
            inputFieldA = transform.Find("Controls/InputFields/InputFieldA").gameObject;
        }

        private void Start()
        {
            caption.GetComponentInChildren<Text>().text = Caption;

            InputField r = inputFieldR.GetComponentInChildren<InputField>();
            r.text = Value.r.ToString();
            r.onValueChange.AddListener(OnValueChangeR);
            r.onEndEdit.AddListener(OnEndEditR);

            InputField g = inputFieldG.GetComponentInChildren<InputField>();
            g.text = Value.g.ToString();
            g.onValueChange.AddListener(OnValueChangeG);
            g.onEndEdit.AddListener(OnEndEditG);

            InputField b = inputFieldB.GetComponentInChildren<InputField>();
            b.text = Value.b.ToString();
            b.onValueChange.AddListener(OnValueChangeB);
            b.onEndEdit.AddListener(OnEndEditB);

            InputField a = inputFieldA.GetComponentInChildren<InputField>();
            a.text = Value.a.ToString();
            a.onValueChange.AddListener(OnValueChangeA);
            a.onEndEdit.AddListener(OnEndEditA);
        }

        private void OnValueChangeR(string str)
        {
            float r;
            if (float.TryParse(str, out r))
            {
                Color c = Value;
                Value = new Color(r, c.g, c.b, c.a);
            }
        }

        private void OnEndEditR(string str)
        {
            InputField r = inputFieldR.GetComponentInChildren<InputField>();
            r.onValueChange.RemoveListener(OnValueChangeR);
            r.text = Value.r.ToString();
            r.onValueChange.AddListener(OnValueChangeR);
        }

        private void OnValueChangeG(string str)
        {
            float g;
            if (float.TryParse(str, out g))
            {
                Color c = Value;
                Value = new Color(c.r, g, c.b, c.a);
            }
        }

        private void OnEndEditG(string str)
        {
            InputField g = inputFieldG.GetComponentInChildren<InputField>();
            g.onValueChange.RemoveListener(OnValueChangeG);
            g.text = Value.g.ToString();
            g.onValueChange.AddListener(OnValueChangeG);
        }

        private void OnValueChangeB(string str)
        {
            float b;
            if (float.TryParse(str, out b))
            {
                Color c = Value;
                Value = new Color(c.r, c.g, b, c.a);
            }
        }

        private void OnEndEditB(string str)
        {
            InputField b = inputFieldB.GetComponentInChildren<InputField>();
            b.onValueChange.RemoveListener(OnValueChangeB);
            b.text = Value.b.ToString();
            b.onValueChange.AddListener(OnValueChangeB);
        }

        private void OnValueChangeA(string str)
        {
            float a;
            if (float.TryParse(str, out a))
            {
                Color c = Value;
                Value = new Color(c.r, c.g, c.b, a);
            }
        }

        private void OnEndEditA(string str)
        {
            InputField a = inputFieldA.GetComponentInChildren<InputField>();
            a.onValueChange.RemoveListener(OnValueChangeA);
            a.text = Value.a.ToString();
            a.onValueChange.AddListener(OnValueChangeA);
        }
    }
}
