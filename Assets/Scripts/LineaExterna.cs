using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineaExterna : MonoBehaviour
{

    [SerializeField] private Material Outline_Material;
    [SerializeField] private float Outline_Scale;
    [SerializeField] private Color Outline_Color;
    private Renderer outlineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        outlineRenderer = CreateOutline(Outline_Material, Outline_Scale, Outline_Color);
        outlineRenderer.enabled = true;
    }

    Renderer CreateOutline(Material outlineM, float scaleFactor, Color color)
    {
        GameObject outlineGameObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineGameObject.GetComponent<Renderer>();

        rend.material = outlineM;
        rend.material.SetColor("_Outline_Color", color);
        rend.material.SetFloat("_scaleFactor", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineGameObject.GetComponent<LineaExterna>().enabled = false;
        outlineGameObject.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;
    }
}
