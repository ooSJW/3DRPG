/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public partial class QuestWindow : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler // Data Field
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform questTransform;

        [SerializeField] private RectTransform questBtnImageTransform;
        [SerializeField] private GameObject questInfo;

        private Vector2 offset;
    }
    public partial class QuestWindow : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }

    public partial class QuestWindow : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler // Property
    {
        private void Start()
        {
            RotateImage();
        }

        public void QuestInfoBtnClick()
        {
            MainSystem.Instance.QuestManager.QuestController.GetCurrentQuest().SetQuestText();
            questInfo.SetActive(!questInfo.activeSelf);
            RotateImage();
        }

        public void RotateImage()
        {
            Vector3 rotation = Vector3.zero;

            if (questInfo.activeSelf)
                rotation.z = 270;
            else
                rotation.z = 90;

            questBtnImageTransform.rotation = Quaternion.Euler(rotation);
        }
    }

    public partial class QuestWindow : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler // Interface
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            questTransform.SetAsLastSibling();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Vector2 localPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                 (RectTransform)canvas.transform,
                 eventData.position,
                 eventData.pressEventCamera,
                 out localPosition))
                offset = localPosition - (Vector2)questTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 localPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                eventData.position,
                eventData.pressEventCamera,
                out localPosition))
                questTransform.anchoredPosition = localPosition - offset;
        }
    }
}
