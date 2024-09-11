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

    private IXRInteractor _interactable;
    private bool _hasLink;

    private void Awake()
    {
        _lineRenderer.SetPositions(new[] {transform.position, transform.position, transform.position, transform.position});
        _hintNode.text = _idea.ConnectionOne.Title + "\n" + _idea.ConnectionTwo.Title;
        _hintNode.gameObject.SetActive(false);
    }

    public void OnGrab(SelectEnterEventArgs grabData)
    {
        _interactable = grabData.interactorObject;
        Debug.Log("Grabbed with " + ((_interactable.interactionLayers & (1 << 1)) == 0 ? "Right" : "Left"));
        if (SelectedIdea != null)
        {
            SelectedIdea.StopSelecting();
        }
        _lineRenderer.enabled = true;
        var index = _hasLink ? 2 : 0;
        _hintNode.gameObject.SetActive(true);
        
        _lineRenderer.SetPosition(index, transform.position);
        _particleSystem.Play();
        SelectedIdea = this;
    }

    public void LinkTo(Article evidence, Transform target)
    {
        var index = -1;
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
        Debug.Log("Released");
        _particleSystem.Stop();
        SelectedIdea = null;
        _hintNode.gameObject.SetActive(false);
    }
}