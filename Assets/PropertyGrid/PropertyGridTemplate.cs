using UnityEngine;

namespace namudev
{
    public class PropertyGridTemplate : MonoBehaviour
    {
        public string Type { get { return type; } }

        [SerializeField]
#pragma warning disable 649
        private string type;
#pragma warning restore 649
    }
}
