using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EvidenceCreator : MonoBehaviour
{
    private const int EVIDENCE_TOTAL = 5;
    
    [SerializeField] private GameObject _evidencePrefab;
    [SerializeField] private List<Article> _evidenceDataList;
    [SerializeField] private List<GameObject> _evidencePiles;
    [SerializeField] private Vector3 _evidenceRotation;
    [SerializeField] private float _evidenceOffset;
    
    private AudioSource _audioSource;
    private List<EvidenceObject> _evidenceObjects = new();

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
        var shuffledList = new List<Article>(_evidenceDataList);
        shuffledList = ShuffleList(shuffledList);
        
        for (int i = 0; i < shuffledList.Count && i < EVIDENCE_TOTAL; i++)
        {
            var evidence = Instantiate(_evidencePrefab, _evidencePiles[i % pilesCount].transform);
            evidence.transform.localPosition = new Vector3(0, Mathf.Floor(i / (float)pilesCount) * _evidenceOffset, 0);
            evidence.transform.localEulerAngles = _evidenceRotation;
            evidence.GetComponent<EvidenceObject>().SetEvidenceData(_evidenceDataList[i], PlayClip);
            _evidenceObjects.Add(evidence.GetComponent<EvidenceObject>());
        }

        StartCoroutine(AwaitAllOnBoard());
    }

    private IEnumerator AwaitAllOnBoard()
    {
        yield return new WaitUntil(() => EvidenceObject.TotalEvidencePlaced == EVIDENCE_TOTAL);
        Debug.Log("All evidence placed");
        _evidenceObjects.ForEach(x => x.DisableMoving());
    }
    
    private List<Article> ShuffleList(List<Article> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var temp = list[i];
            var randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

        return list;
    }

    public void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}