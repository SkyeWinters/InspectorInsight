using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IdeaBubble : MonoBehaviour
{
    public static IdeaBubble SelectedIdea;
    
    [SerializeField] private Idea _idea;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private TMP_Text _hintNode;

    private bool _hasLink;

    private void Awake()
    {
        _lineRenderer.SetPositions(new[] {transform.position, transform.position, transform.position, transform.position});
        _lineRenderer.enabled = true;
        _particleSystem.Stop();
        _hintNode.text = _idea.ConnectionOne.Title + "\n" + _idea.ConnectionTwo.Title;
        _hintNode.gameObject.SetActive(false);
    }

    public void OnGrab(SelectEnterEventArgs grabData)
    {
        if (SelectedIdea != null)
        {
            SelectedIdea.StopSelecting();
        }

        StartSelecting();
    }

    private void StartSelecting()
    {
        var index = _hasLink ? 2 : 0;
        
        _hintNode.gameObject.SetActive(true);
        _lineRenderer.SetPosition(index, transform.position);
        _particleSystem.Play();
        SelectedIdea = this;
    }

    public void LinkTo(Article evidence, Transform target)
    {
        int index;
        if (_idea.ConnectionOne == evidence)
        {
            index = 1;
        }
        else if (_idea.ConnectionTwo == evidence)
        {
            index = 3;
        }
        else
        {
            return;
        }
        _lineRenderer.SetPosition(index, target.position);
        _hasLink = true;
    }
    
    public void StopSelecting()
    {
        SelectedIdea = null;
        _particleSystem.Stop();
        _hintNode.gameObject.SetActive(false);
    }
}