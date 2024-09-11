using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Idea", fileName = "SO_Idea_", order = 0)]
public class Idea : ScriptableObject
{
    public string Title;
    public string Hint;
    public string Description;
    public Article ConnectionOne;
    public Article ConnectionTwo;
}