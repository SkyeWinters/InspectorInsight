using System;
using System.Collections.Generic;
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

    private List<ScriptableObject> _linkedArticles;

    private void Awake()
    {
        _lineRenderer.enabled = true;
        _particleSystem.Stop();
        if (_idea != null)
        {
            SetIdea(_idea);
        }
        _hintNode.gameObject.SetActive(false);
    }
    
    public void SetIdea(Idea idea)
    {
        _idea = idea;
        _hintNode.text = _idea.Hint;
        
        _lineRenderer.positionCount = _idea.Connections.Count * 2;
        
        var positions = new Vector3[_lineRenderer.positionCount];
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            positions[i] = transform.position;
        }
        _lineRenderer.SetPositions(positions);
        
        _linkedArticles = new List<ScriptableObject>();
    }

    public void OnGrab(SelectEnterEventArgs grabData)
    {
        if (IsCompleted() && SelectedIdea != null && SelectedIdea._idea.Connections.Contains(_idea))
        {
            SelectedIdea.LinkTo(_idea, transform);
            SelectedIdea.StopSelecting();
        }
        else
        {
            if (SelectedIdea != null)
            {
                SelectedIdea.StopSelecting();
            }

            StartSelecting();
        }
    }

    private void StartSelecting()
    {
        _hintNode.text = IsCompleted() ? _idea.Description : _idea.Hint;
        _hintNode.gameObject.SetActive(true);
        _particleSystem.Play();
        SelectedIdea = this;
    }

    public void LinkTo(ScriptableObject evidence, Transform target)
    {
        if (!_idea.Connections.Contains(evidence) || _linkedArticles.Contains(evidence)) return;
        
        _lineRenderer.SetPosition(_idea.Connections.IndexOf(evidence) * 2 + 1, target.position);
        _linkedArticles.Add(evidence);
    }
    
    public bool IsCompleted()
    {
        return _linkedArticles.Count == _idea.Connections.Count;
    }
    
    public void StopSelecting()
    {
        SelectedIdea = null;
        _particleSystem.Stop();
        _hintNode.gameObject.SetActive(false);
    }
}