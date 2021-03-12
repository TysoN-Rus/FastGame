using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


namespace Testing {

    public class TestManager : MonoBehaviour {

        private int numModule = 0;
        public int countMaxQuestionDef = 10;
        private int countMaxQuestion;

        public Button questionStartButton;

        [Header("UIElement")]
        [SerializeField] private Text questionText;
        [SerializeField] private Transform parentButtons;
        [SerializeField] private Button prefabButtonAnswer;
        [SerializeField] private Text countQuestionsText;
        [SerializeField] private Text countErrorsText;
        private int countErrors = 0;
        
        [Header("Moduls")]
        [SerializeField] private ListForTest[] moduls;

        private Random myRand;

        private int numQuestState = 0;
        
        private void Awake() {
            myRand = new Random((int)Time.time);

        //    Actions.ContentBaseInstanceEvent.AddListener(x => {
        //        if (x != null)
        //            Close();
        //    });

        //    Actions.Changed.AddListener(x => Close());
        //    Actions.Changed.AddListener(x => CheckAvailability());
        } 

        private List<ElementForTest> elements = new List<ElementForTest>();
        private int numModuls;

        private void CheckAvailability() {
            bool bee = true;
            countMaxQuestion = countMaxQuestionDef;
            //numModuls = CarActor.Instance.SelectedSection;

            print(numModuls);

            if (numModuls >= moduls.Length) {
                Debug.LogError("[ERROR] Нет такого модуля");
                bee = false;
            }

            if (countMaxQuestion > moduls[numModuls].elements.Length) {
                countMaxQuestion = moduls[numModuls].elements.Length;
                Debug.Log("Превышенно количество вопросов");
            }
            if (countMaxQuestion == 0) {
                Debug.LogError("[ERROR] В модуле нет вопросов");
                bee = false;
            }
            
            questionStartButton.interactable = bee;
        }

        public void GenerationListTest() {
            questionText.transform.parent.gameObject.SetActive(true);
            numQuestState = 0;
            countErrors = 0;
            elements.Clear();

            List<ElementForTest> elementsTemp = new List<ElementForTest>(moduls[numModuls].elements);

            for (int i = 0; i < countMaxQuestion; i++) {
                int num = myRand.Next(0, elementsTemp.Count);
                elements.Add(elementsTemp[num]);
                elementsTemp.RemoveAt(num);
            }
            elementsTemp.Clear();

            SetNextQuest();
            return;
        }


        public void SetNextQuest() {
            while (parentButtons.childCount > 0) {
                DestroyImmediate(parentButtons.GetChild(0).gameObject);
            }
            questionText.text = elements[numQuestState].question;
            countQuestionsText.text = "Вопросы: " + (numQuestState + 1) + " / " + countMaxQuestion;
            countErrorsText.text = "Ошибки:  " + countErrors.ToString();

            List<string> elementsTemp = new List<string>(elements[numQuestState].answer);

            while (elementsTemp.Count > 0) {
                int temp = myRand.Next(elementsTemp.Count);
                Button button;
                button = Instantiate(prefabButtonAnswer, parentButtons);

                string bee = elementsTemp[temp];
                button.onClick.AddListener(() => SelectAnswer(bee));
                button.GetComponentInChildren<Text>().text = bee;
                elementsTemp.RemoveAt(temp);
            }
        }

        public void SelectAnswer(string str) {
            if (elements[numQuestState].answer[0] == str) {
                Debug.Log("Верно");
            } else {
                Debug.Log("НЕ Верно");
                countErrors++;
            }
            numQuestState++;
            if (numQuestState >= countMaxQuestion) {
                Debug.Log("Пройдено");

                questionText.text = "Тест пройден";
                while (parentButtons.childCount > 0) {
                    DestroyImmediate(parentButtons.GetChild(0).gameObject);
                }
                Button button;
                button = Instantiate(prefabButtonAnswer, parentButtons);

                string bee = "Закрыть";
                button.onClick.AddListener(() => Close());
                button.GetComponentInChildren<Text>().text = bee;
                return;
            }
            SetNextQuest();
        }

        public void Close() {
            questionText.transform.parent.gameObject.SetActive(false);
        }
    }

}
