using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName ="Quiz Question", fileName = "New Question")]
[UsedImplicitly]
public class QuestionScriptObject : ScriptableObject
{
    [SerializeField]
    [TextArea(2,6)]
    private string _question = "Enter new question Text Here";

    public string Question => _question;

    [SerializeField]
    private string _answer1 = "Answer 1";

    [SerializeField]
    private string _answer2 = "Answer 2";

    [SerializeField]
    private string _answer3 = "Answer 3";

    [SerializeField]
    private string _answer4 = "Answer 4";
}