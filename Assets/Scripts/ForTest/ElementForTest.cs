using UnityEngine;
using UnityEngine.Events;

namespace Testing {
    [CreateAssetMenu(fileName = "ElementTest", menuName = "Test/Element", order = 1)]
    public class ElementForTest : ScriptableObject {
        [Header("Вопрос")]
        public string question;
        [Header("Варианты ответа (Первый верный)")]
        public string[] answer;
    }
}


