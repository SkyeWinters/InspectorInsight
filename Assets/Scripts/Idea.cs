using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Idea", fileName = "SO_Idea_", order = 0)]
public class Idea : ScriptableObject
{
    public string Hint;
    public string Description;
    public List<ScriptableObject> Connections;
}