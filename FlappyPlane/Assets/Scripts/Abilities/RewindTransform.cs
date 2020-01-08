using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class RewindTransform : MonoBehaviour
{
    public event Action OnRewindFinish;

    private List<PosRot> _transforms;
    private bool _isRewinding;
    private Rigidbody _rigidBody;
    private bool _isInitialKinematic;
    [HideInInspector] public float recordTime = 5.0f;

    private void Awake()
    {
        _transforms = new List<PosRot>();
        _rigidBody = GetComponentInParent<Rigidbody>();
        _isInitialKinematic = _rigidBody.isKinematic;
    }

    private void FixedUpdate()
    {
        if (_isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    public void StartRewind()
    {
        _isRewinding = true;
        _rigidBody.isKinematic = true;
    }

    public void StopRewind()
    {
        _isRewinding = false;
        _rigidBody.isKinematic = _isInitialKinematic;
    }

    private void Record()
    {
        int maxRecordAmount = (int) Math.Round(recordTime / Time.fixedDeltaTime);

        /* remove last record if exceed the limit */
        if (_transforms.Count > maxRecordAmount)
        {
            _transforms.RemoveAt(0);
        }

        _transforms.Add(new PosRot(transform.position, transform.rotation));
    }

    private void Rewind()
    {
        if (_transforms.Count > 0)
        {
            PosRot lastPosRot = _transforms[_transforms.Count - 1];
            _rigidBody.position = lastPosRot.Position;
            _rigidBody.rotation = lastPosRot.Rotation;
            _transforms.RemoveAt(_transforms.Count - 1);
        }
        else
        {
            StopRewind();
            OnRewindFinish?.Invoke();
        }
    }
}