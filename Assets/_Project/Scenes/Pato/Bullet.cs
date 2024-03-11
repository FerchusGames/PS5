using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BULLET_TYPE _bulletType;
    public float speed = 1;
    const float bulletLifeTime = 3;
    float currentBulletTime = 0;

    void OnEnable()
    {
        currentBulletTime = bulletLifeTime;
    }
    // Start is called before the first frame update
  
    void CheckCurrentBulletLifetime()
    {
        currentBulletTime -= Time.deltaTime;

        if (currentBulletTime < 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * 0.1f);
        CheckCurrentBulletLifetime();
    }
    
}
public enum BULLET_TYPE
{
    Pistola,
    Subfusiles,
    Rocket_Launcher

}
