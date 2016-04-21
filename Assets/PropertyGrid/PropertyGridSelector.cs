using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace namudev
{
    public class PropertyGridSelector : MonoBehaviour
    {
        private static readonly Color highlight = new Color(0.73f, 1.0f, 0.45f);

#pragma warning disable 649
        [SerializeField]
        private GameObject propertyGrid;
#pragma warning restore 649

        private GameObject selected;
        private Color color;

        private void Update()
        {
            const int LEFT_MOUSE_BUTTON = 0;
            if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
            {
                if (!IsCursorOverUi())
                {
                    var hit = new RaycastHit();
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                    {
                        GameObject gameObject = hit.transform.gameObject;
                        var renderer = gameObject.GetComponent<MeshRenderer>();
                        if (renderer != null)
                        {
                            Select(gameObject);
                        }
                    }
                    else
                    {
                        ClearSelection();
                    }
                }
            }
        }

        private bool IsCursorOverUi()
        {
            var pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);
            return (raycastResults.Count > 0);
        }

        private void Select(GameObject gameObject)
        {
            ClearSelection();

            selected = gameObject;
            color = selected.GetComponent<MeshRenderer>().material.color;
            selected.GetComponent<MeshRenderer>().material.color = highlight;

            if (propertyGrid != null)
            {
                propertyGrid.GetComponent<PropertyGrid>().Populate(selected);
            }
        }

        private void ClearSelection()
        {
            if (selected != null)
            {
                selected.GetComponent<MeshRenderer>().material.color = color;
                selected = null;
            }

            if (propertyGrid != null)
            {
                propertyGrid.GetComponent<PropertyGrid>().Clear();
            }
        }
    }
}
