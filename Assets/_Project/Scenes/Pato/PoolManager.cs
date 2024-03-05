using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject _bulletPrefab;
    public GameObject _RocketPrefab;
    public GameObject _SubfusilPrefab;
    public GameObject _pistolaPrefab;
    List<Bullet> bulletPool = new List<Bullet>();

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void ShotBullet(BULLET_TYPE _t)
    {
        for(int i = 0; i<bulletPool.Count; i++)
        {
            if(!bulletPool[i].gameObject.activeInHierarchy && bulletPool[i]._bulletType == _t)
            {
                bulletPool[i].transform.position = transform.position;
                bulletPool[i].transform.rotation = transform.rotation;
                bulletPool[i].gameObject.SetActive(true);
                return;
            }
        }
        switch (_t)
        {
            case BULLET_TYPE.Pistola:
                bulletPool.Add(Instantiate(_pistolaPrefab, transform.position, transform.rotation).GetComponent<Bullet>());
                break;

            case BULLET_TYPE.Subfusiles:
                bulletPool.Add(Instantiate(_SubfusilPrefab, transform.position, transform.rotation).GetComponent<Bullet>());
                break;
            
            case BULLET_TYPE.Rocket_Launcher:
                bulletPool.Add(Instantiate(_RocketPrefab, transform.position, transform.rotation).GetComponent<Bullet>());
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShotBullet(BULLET_TYPE.Pistola);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            ShotBullet(BULLET_TYPE.Subfusiles);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShotBullet(BULLET_TYPE.Rocket_Launcher);
        }
    }
}
