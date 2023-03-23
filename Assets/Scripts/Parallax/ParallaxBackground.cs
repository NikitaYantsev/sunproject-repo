using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    List<ParallaxLayer> parallaxLayers = new();

    void Start()
    {
        if (parallaxCamera == null)
            if (Camera.main != null)
                parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
        if (parallaxCamera != null)
        {
            parallaxCamera.onCameraTranslateByX += MoveOnlyByX;
            parallaxCamera.onCameraTranslateByAny += MoveByXY;
        }
            
        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }
    public void MoveOnlyByX(float deltaX)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(deltaX);
        }
    }

    public void MoveByXY(float deltaX, float deltaY)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(deltaX, deltaY);
        }
    }
}
