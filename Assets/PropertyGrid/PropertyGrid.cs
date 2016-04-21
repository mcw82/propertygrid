using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace namudev
{
    public class PropertyGrid : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField]
        private GameObject targetObject;
        
        [SerializeField]
        private bool logging;
#pragma warning restore 649

        private GameObject label;
        private GameObject scrollbar;
        private Dictionary<Type,GameObject> itemTemplateMap;
        private List<GameObject> items;

#if UNITY_EDITOR
        [MenuItem("GameObject/UI/PropertyGrid")]
        private static void Create()
        {
            string[] assets = AssetDatabase.FindAssets("l:NamudevPropertyGrid");
            if (assets.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(assets[0]);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                GameObject gameObject = Instantiate(prefab);
                gameObject.name = prefab.name;
                Transform activeTransform = Selection.activeTransform;
                if ((activeTransform != null) && (activeTransform.gameObject.GetComponent<RectTransform>() != null))
                {
                    gameObject.transform.SetParent(activeTransform, false);
                }
                else
                {
                    var canvas = FindObjectOfType<Canvas>();
                    if (canvas != null)
                    {
                        gameObject.transform.SetParent(canvas.transform, false);
                    }
                }
                Undo.RegisterCreatedObjectUndo(gameObject, "Create PropertyGrid");
            }
            else
            {
                Debug.LogError("PropertyGrid prefab not found");
            }
        }
#endif

        public void AppendLabel(string text)
        {
            GameObject item = Instantiate(label);
            items.Add(item);

            item.name = text;
            item.transform.SetParent(label.transform.parent);
            item.SetActive(true);
            item.GetComponentInChildren<Text>().text = text;
        }

        public T AppendProperty<T>(string caption, object value)
            where T : Component
        {
            if (!PropertyGridItem.TypeMap.ContainsValue(typeof(T)))
            {
                string format = "Can not add property of type {0}";
                string message = string.Format(format, typeof(T).Name);
                Log(message);
                return default(T);
            }

            Type type = PropertyGridItem.TypeMap.FirstOrDefault(pair => pair.Value == typeof(T)).Key;
            if (!itemTemplateMap.ContainsKey(type))
            {
                string format = "No template found for type {0}";
                string message = string.Format(format, type.Name);
                Log(message);
                return default(T);
            }

            GameObject item = Instantiate(itemTemplateMap[type]);
            items.Add(item);

            PropertyGridBinding binding = item.AddComponent<PropertyGridBinding>();
            binding.Initialize(caption, value, type);

            item.name = caption;
            item.transform.SetParent(itemTemplateMap[type].transform.parent);
            item.AddComponent<T>();
            item.SetActive(true);

            return item.GetComponent<T>();
        }

        public void Populate(object obj)
        {
            var gameObject = obj as GameObject;
            if (gameObject != null)
            {
                AppendLabel(gameObject.GetType().Name);
            }
            AppendProperties(obj);
            if (gameObject != null)
            {
                Component[] components = gameObject.GetComponents<Component>();
                foreach (Component component in components)
                {
                    AppendLabel(component.GetType().Name);
                    var exclude = new List<string> { "hideFlags", "name", "tag" };
                    AppendProperties(component, exclude);
                }
            }
        }

        public void Clear()
        {
            scrollbar.GetComponent<Scrollbar>().value = 1.0f;
            foreach (GameObject item in items)
            {
                Destroy(item);
            }
            items.Clear();
        }

        private void Awake()
        {
            label = transform.Find("Scroll/Panel/Label").gameObject;
            scrollbar = transform.Find("Scrollbar").gameObject;
            itemTemplateMap = new Dictionary<Type,GameObject>();
            foreach (Transform t in transform.Find("Scroll/Panel"))
            {
                var template = t.gameObject.GetComponent<PropertyGridTemplate>();
                if (template != null)
                {
                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        Type type = assembly.GetType(template.Type);
                        if (type != null)
                        {
                            itemTemplateMap[type] = t.gameObject;
                            string format = "Matched template '{0}' with type '{1}' from assembly '{2}'";
                            string message = string.Format(format, template.gameObject.name, type.Name, assembly.FullName);
                            Log(message);
                        }
                    }
                }
                t.gameObject.SetActive(false);
            }
            items = new List<GameObject>();

            if (targetObject != null)
            {
                Populate(targetObject);
            }
        }

        private void AppendProperties(object obj, List<string> exclude = null)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (!propertyInfo.CanRead || (propertyInfo.GetGetMethod(false) == null))
                {
                    continue;
                }
                else if (!propertyInfo.CanWrite || (propertyInfo.GetSetMethod(false) == null))
                {
                    continue;
                }
                else if ((exclude == null) || !exclude.Contains(propertyInfo.Name))
                {
                    AppendProperty(obj, propertyInfo);
                }
            }
        }

        private void AppendProperty(object obj, PropertyInfo propertyInfo)
        {
            Type key = null;
            if (itemTemplateMap.ContainsKey(propertyInfo.PropertyType))
            {
                key = propertyInfo.PropertyType;
            }
            else if (propertyInfo.PropertyType.IsSubclassOf(typeof(Enum)))
            {
                key = typeof(Enum);
            }
            if (key == null)
            {
                string format = "Skipped property '{0}' (no template found for type '{1}')";
                string message = string.Format(format, propertyInfo.Name, propertyInfo.PropertyType.Name);
                Log(message);
                return;
            }

            GameObject item = Instantiate(itemTemplateMap[key]);
            items.Add(item);

            PropertyGridBinding binding = item.AddComponent<PropertyGridBinding>();
            binding.Initialize(obj, propertyInfo);

            item.name = propertyInfo.Name;
            item.transform.SetParent(itemTemplateMap[key].transform.parent);
            item.AddComponent(PropertyGridItem.TypeMap[key]);
            item.SetActive(true);
        }

        private void Log(string message)
        {
            if (logging)
            {
                Debug.Log("[PROPERTYGRID] " + message);
            }
        }
    }
}
