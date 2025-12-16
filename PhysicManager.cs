using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicManager : MonoBehaviour
{

    public GameObject obj;
    public GameObject cube;

    [Header("Inertia")]
    public Vector3 inertia = new Vector3(1f, 2f, 3f);

    [Header("Initial angular velocity (in local body axes)")]
    public Vector3 initialOmega = new Vector3(0.01f, 5f, 0.0f);

    [Header("Simulation")]
    public bool simulateOnStart = true;
    public bool applySmallPerturbation = true;
    public Vector3 perturbation = new Vector3(0.01f, 0f, 0.02f);


    public Vector3 omega; // ω (radians / sec)
    private Quaternion rotation; // orientace tělesa
    private Vector3 invInertia; // 1 / I 

    void Start()
    {
        Set();
    }


    public void Set()
    {
        float x = cube.transform.localScale.x * cube.transform.localScale.x;
        float y = cube.transform.localScale.y * cube.transform.localScale.y;
        float z = cube.transform.localScale.z * cube.transform.localScale.z;
        inertia = new Vector3(
            y + z,
            x + z,
            x + y
        );

        invInertia = new Vector3(
            1.0f / inertia.x,
            1.0f / inertia.y,
            1.0f / inertia.z
        );

        omega = initialOmega;
        if (applySmallPerturbation) omega += perturbation;
        rotation = Quaternion.identity;
        obj.transform.rotation = Quaternion.identity;

    }



    void FixedUpdate()
    {
        if (!simulateOnStart) return;

        float dt = Time.fixedDeltaTime;

        // Integrace ω pomocí RK4 pro stabilitu
        omega = IntegrateOmegaRK4(omega, dt);

        // Úhel = |ω| * dt (radiány). Pokud je úhel malý, AngleAxis je bezpečný.
        float angle = omega.magnitude * dt;
        Vector3 axis = omega / omega.magnitude; // v lokálním tělesném prostoru
        // Quaternion.AngleAxis očekává stupně, proto *Rad2Deg
        Quaternion dq = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, axis);
        // dq je rotace relativní k lokálním osám tělesa
        // protože omega je v body-frame, použij rotation = rotation * dq
        rotation = rotation * dq;
        obj.transform.rotation = rotation;
    }

    // RHS Eulerovy rovnice: dω/dt = I^{-1} (τ - ω × (I ω))
    // zde τ = 0 (zadáno v tělesném rámci), takže dω/dt = - I^{-1} (ω × (I ω))
    private Vector3 OmegaDot(Vector3 w)
    {
        // I * w (diagonálně)
        Vector3 Iw = new Vector3(inertia.x * w.x, inertia.y * w.y, inertia.z * w.z);

        // gyro = ω × (Iω)
        Vector3 gyro = Vector3.Cross(w, Iw);

        // dω/dt = I^{-1} * ( - gyro )  (protože τ = 0)
        Vector3 domega = new Vector3(-gyro.x * invInertia.x, -gyro.y * invInertia.y, -gyro.z * invInertia.z);
        return domega;
    }

    // RK4 integrátor pro vektor ω
    private Vector3 IntegrateOmegaRK4(Vector3 w, float dt)
    {
        Vector3 k1 = OmegaDot(w);
        Vector3 k2 = OmegaDot(w + 0.5f * dt * k1);
        Vector3 k3 = OmegaDot(w + 0.5f * dt * k2);
        Vector3 k4 = OmegaDot(w + dt * k3);

        Vector3 wNext = w + (dt / 6f) * (k1 + 2f * k2 + 2f * k3 + k4);
        return wNext;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.cyan;
        Vector3 worldOmega = obj.transform.rotation * omega;
        Gizmos.DrawLine(obj.transform.position, obj.transform.position + worldOmega);
        Gizmos.DrawSphere(obj.transform.position + worldOmega, 0.02f);
    }
}
