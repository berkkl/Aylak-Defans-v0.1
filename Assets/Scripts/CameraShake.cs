using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    public int flag = 0;

    public static CameraShake instance;

    public ScoreManager sm;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (flag == 1 && ScoreManager.instance.lives >= 1)
        {
            //Debug.Log("girdi" + ScoreManager.instance.lives);
            StartCoroutine(Shake(0.3f, 1));
            flag = 0;
        }
        
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        //Debug.Log(originalPos +"origiiii");
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            
            yield return null;
        }
        //Debug.Log(originalPos + "abiciiii");
        transform.localPosition = new Vector3(0f, 0f, -10f);
    } 
}
