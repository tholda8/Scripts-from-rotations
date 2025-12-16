using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public PhysicManager physic;
    public TMP_Text statsText;

    float kinetic_enery = 0.0f;
    float angular_momentum = 0.0f;
    public float time_elapsed = 0.0f;
    void CalculateKineticEnergy()
    {
        Vector3 omega = physic.omega;
        Vector3 I = physic.inertia;
        kinetic_enery = 0.5f * (
            I.x * omega.x * omega.x +
            I.y * omega.y * omega.y +
            I.z * omega.z * omega.z
        );
    }

    void CalculateAngularMomentum()
    {
        Vector3 omega = physic.omega;
        Vector3 I = physic.inertia;
        Vector3 L = new Vector3(
            I.x * omega.x,
            I.y * omega.y,
            I.z * omega.z
        );
        angular_momentum = L.magnitude;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        time_elapsed += Time.fixedDeltaTime;
        CalculateAngularMomentum();
        CalculateKineticEnergy();
        statsText.text = "{E_K} = "+ kinetic_enery.ToString("F6") + "\n"+
                         "{L} = " + angular_momentum.ToString("F6") + "\n"+
                         "{t} = " + time_elapsed.ToString("F2");


    }
}
