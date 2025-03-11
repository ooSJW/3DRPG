/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class QuestMark : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite[] sprite;
        private Canvas canvas;
    }
    public partial class QuestMark : MonoBehaviour
    {
        private void Start()
        {
            MainSystem.Instance.QuestManager.QuestController.ChangeQuestMark();
        }
        private void Update()
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
    public partial class QuestMark : MonoBehaviour
    {
        public void ChangeImage(QuestImage questImage)
        {
            image.sprite = sprite[(int)questImage];
        }
    }
}
