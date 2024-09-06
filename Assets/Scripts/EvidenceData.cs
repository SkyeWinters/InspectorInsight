using UnityEngine;

[CreateAssetMenu(menuName = "Evidence", fileName = "SO_Evidence_Name", order = 0)]
public class EvidenceData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    
    public string Name => _name;
    public string Description => _description;
}
