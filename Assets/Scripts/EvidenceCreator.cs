using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EvidenceCreator : MonoBehaviour
{
    [SerializeField] private GameObject _evidencePrefab;
    [SerializeField] private List<Article> _evidenceDataList;
    [SerializeField] private List<GameObject> _evidencePiles;
    [SerializeField] private Vector3 _evidenceRotation;
    [SerializeField] private float _evidenceOffset;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        CreateEvidence();
    }
    
    private void CreateEvidence()
    {
        var pilesCount = _evidencePiles.Count;
        
        for (int i = 0; i < _evidenceDataList.Count; i++)
        {
            var evidence = Instantiate(_evidencePrefab, _evidencePiles[i % pilesCount].transform);
            evidence.transform.localPosition = new Vector3(0, Mathf.Floor(i / (float)pilesCount) * _evidenceOffset, 0);
            evidence.transform.localEulerAngles = _evidenceRotation;
            evidence.GetComponent<EvidenceObject>().SetEvidenceData(_evidenceDataList[i], PlayClip);
        }
    }

    public void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}