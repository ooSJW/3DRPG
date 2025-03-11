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
    using TMPro;
    using Unity.Services.Analytics.Internal;
    using UnityEngine.EventSystems;
    using UnityEngine.InputSystem;
    using DG.Tweening;

    public partial class SkillUIController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler // Data Property
    {
        private int skillPoint = 0;
        public int SKillPoint
        {
            get => skillPoint;
            set
            {
                skillPoint = value;
                skillPointText.text = skillPoint.ToString();
            }
        }
    }

    public partial class SkillUIController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler // Data Field
    {
        [SerializeField] private TMP_Text skillPointText;
        [SerializeField] private List<SkillUIObject> skillUIObjectList = default;
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform skillRectTransform;

        private Vector2 offset;
    }
    public partial class SkillUIController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize()
        {
            Allocate();
            SetUp();
            for (int i = 0; i < skillUIObjectList.Count; i++)
            {
                skillUIObjectList[i].Initialize();
            }
        }
        private void SetUp()
        {

        }
    }
    public partial class SkillUIController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler // Main
    {
        private void Update()
        {
            SKillPoint = MainSystem.Instance.PlayerManager.Player.SkillPoint;
        }
    }

    public partial class SkillUIController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler // Property
    {
        public void CloseBtnClick()
        {
            gameObject.SetActive(false);
        }
    }
    public partial class SkillUIController : MonoBehaviour, IPointerClickHandler,IBeginDragHandler, IDragHandler // Interface
    {

        public void OnPointerClick(PointerEventData eventData)
        {
            skillRectTransform.SetAsLastSibling();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Vector2 localPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                 (RectTransform)canvas.transform,
                 eventData.position,
                 eventData.pressEventCamera,
                 out localPosition))
                offset = localPosition - (Vector2)skillRectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 localPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                eventData.position,
                eventData.pressEventCamera,
                out localPosition))
                skillRectTransform.anchoredPosition = localPosition - offset;
        }
    }
}
