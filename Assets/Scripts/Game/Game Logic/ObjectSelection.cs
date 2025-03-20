using System.Collections;
using System.Collections.Generic;
using Humanoids;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectSelection : MonoBehaviour
{
    [SerializeField] private SelectedObjectScriptable _selectedObject;
    [SerializeField] private GameObject _groundClick;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Intermission.inIntermission == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Clickable") && hit.transform.gameObject.CompareTag("Player"))
                    {
                        GameObject clickedObject = hit.transform.gameObject;

                        if(!clickedObject.GetComponent<Humanoid>().IsDead())
                        {
                            Deselect();

                            Select(clickedObject);
                        }
                    }
                }
            }

            if(Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground") && _selectedObject.CurrentlySelected != null)
                    {
                        Humanoid humanoid = _selectedObject.CurrentlySelected.gameObject.GetComponent<Humanoid>();
                        
                        if(humanoid.MoveCommand(hit.point))
                        {
                            Vector3 _abovePoint = hit.point;

                            _abovePoint.y += 0.1f;

                            Instantiate(_groundClick, _abovePoint, _groundClick.transform.rotation);
                        }
                    }
                }
            }
        } 
        else
        {
            Deselect();
        }
    }

    /// <summary>
    /// Deselects the currently selected object.
    /// </summary>
    private void Deselect()
    {
        _selectedObject.Deselect();
    }

    /// <summary>
    /// Selects a new object.
    /// </summary>
    private void Select(GameObject clickedObject)
    {
        _selectedObject.Select(clickedObject);

        _audioSource.Play();
    }
}
