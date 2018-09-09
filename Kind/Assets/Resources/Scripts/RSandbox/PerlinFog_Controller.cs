using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct FogMsg
{
    public float FogDensity;
}

public class PerlinFog_Controller : MonoBehaviour
{
    UB.D2FogsPE fog;
    // Use this for initialization
    void Awake()
    {
        fog = GetComponent<UB.D2FogsPE>();
        FFMessage<FogMsg>.Connect(IncrementFogDensity);
    }

    private void OnDestroy()
    {
        FFMessage<FogMsg>.Disconnect(IncrementFogDensity);
    }

    private int IncrementFogDensity(FogMsg msg)
    {
        fog.Density = msg.FogDensity;
        return 0;
    }

}
