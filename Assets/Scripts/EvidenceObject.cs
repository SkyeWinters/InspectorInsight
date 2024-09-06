using TMPro;
using UnityEngine;

public class EvidenceObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    
    public void SetEvidenceData(EvidenceData data)
    {
        _nameText.text = data.Name;
        _descriptionText.text = data.Description;
    }
}