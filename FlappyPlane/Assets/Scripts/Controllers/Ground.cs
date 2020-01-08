using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private WaveManager _waveManager;
    public Vector3 upScalingUnit = new Vector3(0.1f, 0, 0.1f);
    private readonly float _initialWidth = 20.0f;

    void Start()
    {
        _waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        _waveManager.OnNewWave += Enlarge;
    }

    void Enlarge(uint wave)
    {
        transform.localScale += upScalingUnit;
        transform.position = transform.position + new Vector3(1.0f, 0, 0);
    }

    public float GetCurrentWidth()
    {
        return _initialWidth + _waveManager.GetWave() - 10; // minus 10 to let some marge before edges
    }
}