using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class uimanager : MonoBehaviour
{

    public List<TMP_InputField> scaleList;
    public List<TMP_InputField> omegaList;
    public PhysicManager physicManager;
    public Stats statsManager;

    // Start is called before the first frame update
    void Start()
    {
        scaleList[0].text = physicManager.cube.transform.localScale.x.ToString("F2", CultureInfo.InvariantCulture);
        scaleList[1].text = physicManager.cube.transform.localScale.y.ToString("F2", CultureInfo.InvariantCulture);
        scaleList[2].text = physicManager.cube.transform.localScale.z.ToString("F2", CultureInfo.InvariantCulture);

        omegaList[0].text = physicManager.initialOmega.x.ToString("F2", CultureInfo.InvariantCulture);
        omegaList[1].text = physicManager.initialOmega.y.ToString("F2", CultureInfo.InvariantCulture);
        omegaList[2].text = physicManager.initialOmega.z.ToString("F2", CultureInfo.InvariantCulture);


    }

    public void SetValues()
    {
        physicManager.cube.transform.localScale = new Vector3(
            float.Parse(scaleList[0].text, CultureInfo.InvariantCulture),
            float.Parse(scaleList[1].text, CultureInfo.InvariantCulture),
            float.Parse(scaleList[2].text, CultureInfo.InvariantCulture)
        );

        physicManager.initialOmega.x = float.Parse(omegaList[0].text, CultureInfo.InvariantCulture);
        physicManager.initialOmega.y = float.Parse(omegaList[1].text, CultureInfo.InvariantCulture);
        physicManager.initialOmega.z = float.Parse(omegaList[2].text, CultureInfo.InvariantCulture);

        physicManager.Set();
        statsManager.time_elapsed = 0.0f;

        scaleList[0].text = physicManager.cube.transform.localScale.x.ToString("F2", CultureInfo.InvariantCulture);
        scaleList[1].text = physicManager.cube.transform.localScale.y.ToString("F2", CultureInfo.InvariantCulture);
        scaleList[2].text = physicManager.cube.transform.localScale.z.ToString("F2", CultureInfo.InvariantCulture);

        omegaList[0].text = physicManager.initialOmega.x.ToString("F2", CultureInfo.InvariantCulture);
        omegaList[1].text = physicManager.initialOmega.y.ToString("F2", CultureInfo.InvariantCulture);
        omegaList[2].text = physicManager.initialOmega.z.ToString("F2", CultureInfo.InvariantCulture);
    }

}
