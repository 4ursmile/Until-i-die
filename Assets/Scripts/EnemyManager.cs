using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    static public EnemyManager Instance;
    [SerializeField] List<Transform> SpawnPos;
    [SerializeField] GameObject enemyPrefab;
    Queue<GameObject> enemys;
    [SerializeField] int firtsMonsternumber = 10;
    [SerializeField] int StartMonster = 4;
    List<GameObject> Xenemies;
    public float startDomValue;
    public float endDomValue;
    [SerializeField] Material enemyMa;
    private void Awake()
    {
        Instance = this;
        Xenemies = new List<GameObject>();
        enemys = new Queue<GameObject>();
        for (int i = 0; i< firtsMonsternumber; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, SpawnPos[Random.Range(0,SpawnPos.Count)]);
            Xenemies.Add(enemy);
            enemy.SetActive(false);
            enemys.Enqueue(enemy);
        }
        for(int i = 0; i< StartMonster; i++)
            SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        if (enemys.Count > 0)
        {
            GameObject enemy = enemys.Dequeue();
            enemy.transform.position = SpawnPos[Random.Range(0, SpawnPos.Count)].position;
            enemy.SetActive(true);
        }
    }
    public void EnQueueEnemy(GameObject enemyDeath)
    {
        enemyDeath.SetActive(false);
        enemys.Enqueue(enemyDeath);
        SpawnEnemy();
    }
    public void UpDateEnemy()
    {
        startDomValue = Mathf.Clamp(startDomValue + 0.01f, 0.01f, endDomValue);
        enemyMa.DOFloat(startDomValue,"_RadiusStep", 0.5f);
        GameObject[] Xenemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var K in Xenemies)
        {
            var enemyY = K.GetComponent<Enemy>();
            if (Player.Instance.Scoreget > 10 && Player.Instance.Scoreget % 30 == 0)
            {
                enemyY.maxHp += 15;
                enemyY.damage += 1;
            }
            enemyY.maxHp += 2;
            enemyY.speed += 0.2f;
        }
        foreach (var K in enemys)
        {
            var enemyY = K.GetComponent<Enemy>();
            if (Player.Instance.Scoreget > 10 && Player.Instance.Scoreget % 30 == 0)
            {
                enemyY.maxHp += 10;
                enemyY.damage += 1;
            }
            enemyY.maxHp += 2;
            enemyY.speed += 0.2f;
        }
        SpawnEnemy();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
