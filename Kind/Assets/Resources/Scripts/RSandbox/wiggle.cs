using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wiggle : FFComponent {
    FFAction.ActionSequence seq;
    public float time = 0.5f;

    private void Start()
    {
        seq = action.Sequence();
        StartSeq();
    }

    void StartSeq()
    {
        var rot10Clockwise = Quaternion.AngleAxis(10.0f, Vector3.forward);
        var rot10CounterClockwise = Quaternion.AngleAxis(-10.0f, Vector3.forward);

        seq.Property(ffrotation, ffrotation.Val * rot10Clockwise, FFEase.E_SmoothStartEnd, time);
        seq.Sync();
        seq.Property(ffrotation, ffrotation.Val * rot10CounterClockwise, FFEase.E_SmoothStartEnd, time);
        seq.Sync();
        seq.Property(ffrotation, ffrotation.Val * rot10CounterClockwise, FFEase.E_SmoothStartEnd, time);
        seq.Sync();
        seq.Property(ffrotation, ffrotation.Val * rot10Clockwise, FFEase.E_SmoothStartEnd, time);
        seq.Sync();
        seq.Call(StartSeq);
    }
}
