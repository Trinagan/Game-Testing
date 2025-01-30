using UnityEngine;

public class EnemyResourceDrop : MonoBehaviour
{
    public float scrapDrop = 40;
    public float biomassDrop = 40;
    public float weaponPartsDrop = 40;
    public GameObject scrap;
    public GameObject biomass;
    public GameObject weaponParts;
    public LayerMask wallLayer;
    private Transform lastPos;
    
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
                GameObject scrapInstance = Instantiate(scrap, transform.position, transform.rotation);
                ResourceOnDrop scrapDropInstance = scrapInstance.GetComponent<ResourceOnDrop>();
                scrapDropInstance.scrapDrop = scrapDrop;

                GameObject biomassInstance = Instantiate(biomass, transform.position, transform.rotation);
                ResourceOnDrop biomassDropInstance = biomassInstance.GetComponent<ResourceOnDrop>();
                biomassDropInstance.biomassDrop = biomassDrop;

            if (enemyController.enemyType != EnemyController.EnemyTypes.AlienAI)
            {
                    GameObject weaponPartsInstance = Instantiate(weaponParts, transform.position, transform.rotation);
                    ResourceOnDrop weaponPartsDropInstance = weaponPartsInstance.GetComponent<ResourceOnDrop>();
                    weaponPartsDropInstance.weaponPartsDrop = weaponPartsDrop;
            }
            
        }
    }
}
