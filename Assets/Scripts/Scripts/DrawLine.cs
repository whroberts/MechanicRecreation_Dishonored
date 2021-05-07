using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer _line = null;

    FarReach _farReach;

    Gradient _purpleGradient;
    Color _purple = new Color(0.392f, 0f, 0.784f);

    private void Awake()
    {
        _farReach = GetComponent<FarReach>();
    }

    public void Draw(Vector3 objectToFollow)
    {
        _line.enabled = true;
        _line.colorGradient = CreateGradient(_purple, _purpleGradient);
        _line.SetPosition(0, this.transform.position);
        _line.SetPosition(2, objectToFollow);
        Vector3 midpoint = new Vector3((_line.GetPosition(0).x + _line.GetPosition(2).x) / 2, (_line.GetPosition(0).y + _line.GetPosition(2).y) / 2, (_line.GetPosition(0).z + _line.GetPosition(2).z) / 2);
        Vector3 topPoint = new Vector3(midpoint.x, midpoint.y + 0.5f, midpoint.z);
        _line.SetPosition(1, topPoint);
    }

    public Gradient CreateGradient(Color color, Gradient gradient)
    {
        gradient = new Gradient();

        GradientColorKey[] colorKey = new GradientColorKey[2];
        colorKey[0].color = color;
        colorKey[0].time = 0.0f;
        colorKey[1].color = color;
        colorKey[1].time = 1.0f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.2f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        return gradient;
    }
}
