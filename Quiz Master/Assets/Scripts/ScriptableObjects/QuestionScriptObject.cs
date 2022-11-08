using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
    public class QuestionScriptObject : ScriptableObject
    {
        [SerializeField]
        [TextArea(2, 6)]
        private string _question = "Enter new question Text Here";

        [SerializeField] 
        private string[] _answers = new string[4];
    
        [SerializeField]
        [Range(0,3)]
        private int _correctAnswerIndex;

        public string Question => _question;

        public string[] Answers => _answers;

        public int CorrectAnswerIndex => _correctAnswerIndex;
    }
}