using UnityEngine;

namespace Testing {
    [CreateAssetMenu(fileName = "ModulTest", menuName = "Test/List", order = 1)]
    public class ListForTest : ScriptableObject {

        public ElementForTest[] elements;
    }
}
