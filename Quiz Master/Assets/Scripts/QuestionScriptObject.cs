using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
    [UsedImplicitly]
    public class QuestionScriptObject : ScriptableObject
    {
        [SerializeField]
        [TextArea(2, 6)]
        private string _question = "Enter new question Text Here";

        [SerializeField] 
        private string[] _answers = new string[4];
    
        [SerializeField]
        [Range(0,3)]
        private int _correctAnswerIndex = 0;

        public string Question => _question;

        public string[] Answers => _answers;

        public int CorrectAnswerIndex => _correctAnswerIndex;
    }
}