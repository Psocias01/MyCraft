using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaDissolve : MonoBehaviour
{

    [SerializeField] private float _dissolveSpeed;
    [SerializeField] private Material _dissolveMat;

    private bool isDissolving;
    
    private float _dissolveValue;
    private float _valueStart = 1;
    private float _valueEnd = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDissolving)
        {
            StartCoroutine(Dissolve());
        }
    }

    private IEnumerator Dissolve()
    {
        isDissolving = true;

        _dissolveValue = _valueStart;
        
        // Emieza el dissolve hasta 0
        while (_dissolveValue > _valueEnd)
        {
            _dissolveValue -= Time.deltaTime * _dissolveSpeed;
            _dissolveMat.SetFloat("_Dissolve", _dissolveValue);

            yield return null;
        }

        Debug.Log($"<color=green><b>CHANGE STATE</b></color>");
        
        yield return new WaitForSeconds(1f);
        
        // Emieza el des-dissolve hasta 1
        while (_dissolveValue < _valueStart)
        {
            _dissolveValue += Time.deltaTime * _dissolveSpeed;
            _dissolveMat.SetFloat("_Dissolve", _dissolveValue);

            yield return null;
        }

        isDissolving = false;
    }
}
