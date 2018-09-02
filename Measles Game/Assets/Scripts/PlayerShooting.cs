using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    [HideInInspector]
    public List<GameObject> Projectiles;
    private float projectileVelocity;
    public GameObject projectilePrefab;
    // Use this for initialization
    void Start() {
        projectileVelocity = 25;
        Debug.Log("Inicializando");
        Projectiles = new List<GameObject>();

    }

    // Update is called once per frame
    void Update() {
        if (Projectiles.Count > 0)
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                GameObject goBullet = Projectiles[i];
                if (goBullet != null)
                {
                    goBullet.transform.Translate(new Vector3(1, 0) * Time.deltaTime * projectileVelocity);
                    Vector3 bulletScreenPos = Camera.main.WorldToScreenPoint(goBullet.transform.position);
                    if (bulletScreenPos.x >= 10000 || bulletScreenPos.x < 0)
                    {
                        DestroyObject(goBullet);
                        Projectiles.Remove(goBullet);

                    }
                }
            }
        } 
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            GameObject bullet = (GameObject) Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectiles.Add(bullet);
        }

        
    }
}