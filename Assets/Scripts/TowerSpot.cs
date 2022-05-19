using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        Debug.Log("tower spot");
        
        ScoreManager sm = FindObjectOfType<ScoreManager>();
        BuildingManager bm = FindObjectOfType<BuildingManager>();

        if (bm.selectedTower != null)
        {
            if (sm.money < bm.selectedTower.GetComponent<Tower>().Cost)
            {
                Debug.Log("para yok");
                return;
            }

            sm.money -= bm.selectedTower.GetComponent<Tower>().Cost;
            Debug.Log($"Kalan Para: {sm.money}");
            Instantiate(bm.selectedTower, this.transform.position, this.transform.rotation);
            Destroy(gameObject);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
