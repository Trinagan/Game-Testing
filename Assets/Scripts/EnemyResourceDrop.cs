using UnityEngine;

public class EnemyResourceDrop : MonoBehaviour
{
    public float scrapDrop = 40;
    public float biomassDrop = 40;
    public float weaponPartsDrop = 40;
    public GameObject scrap;
    public GameObject biomass;
    public GameObject weaponParts;
    
    private EnemyController enemyController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyController = GetComponent<EnemyController>();

        switch (enemyController.enemyType)
        {
            case EnemyController.EnemyTypes.SoliderAI:
                scrapDrop *= 0.6f;
                biomassDrop *= 0.1f;
                weaponPartsDrop *= 0.3f;
                break;
            
            case EnemyController.EnemyTypes.ScientistAI:
                scrapDrop *= 0.3f;
                biomassDrop *= 0.6f;
                weaponPartsDrop *= 0.1f;
                break;

            case EnemyController.EnemyTypes.AlienAI:
                scrapDrop *= 0.1f;
                biomassDrop *= 0.9f;
                weaponPartsDrop *= 0;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyController.health <= 0)
        {
            GameObject scrapInstance = Instantiate(scrap, enemyController.transform.position + new Vector3(1, 1, 0), enemyController.transform.rotation);
            ResourceOnDrop scrapDrop = scrap.GetComponent<ResourceOnDrop>();
            GameObject biomassInstance = Instantiate(biomass, enemyController.transform.position + new Vector3(-1, 1, 0), enemyController.transform.rotation);
            ResourceOnDrop biomassDrop = biomass.GetComponent<ResourceOnDrop>();
            GameObject weaponPartsInstance = Instantiate(weaponParts, enemyController.transform.position + new Vector3(0, -1, 0), enemyController.transform.rotation);
            ResourceOnDrop weaponPartsDrop = weaponParts.GetComponent<ResourceOnDrop>();
            
            
            }
    }
}
