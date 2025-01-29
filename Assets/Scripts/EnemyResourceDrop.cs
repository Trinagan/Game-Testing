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
            Vector3 scrapDirection = new(1, 1, 0);
            RaycastHit2D hitScrap = Physics2D.Raycast(transform.position, scrapDirection.normalized);
            if (hitScrap.distance < scrapDirection.magnitude)
            {
                scrapDirection = hitScrap.point;
                GameObject scrapInstance = Instantiate(scrap, scrapDirection, transform.rotation);
                ResourceOnDrop scrapDropInstance = scrapInstance.GetComponent<ResourceOnDrop>();
                scrapDropInstance.scrapDrop = scrapDrop;
            } 
            else
            {
                GameObject scrapInstance = Instantiate(scrap, transform.position + scrapDirection, transform.rotation);
                ResourceOnDrop scrapDropInstance = scrapInstance.GetComponent<ResourceOnDrop>();
                scrapDropInstance.scrapDrop = scrapDrop;
            }

            Vector3 biomassDirection = new(-1, 1, 0);
            RaycastHit2D hitBiomass = Physics2D.Raycast(transform.position, biomassDirection.normalized);
            if (hitBiomass.distance < biomassDirection.magnitude)
            {
                biomassDirection = hitBiomass.point;
                GameObject biomassInstance = Instantiate(biomass, biomassDirection, transform.rotation);
                ResourceOnDrop biomassDropInstance = biomassInstance.GetComponent<ResourceOnDrop>();
                biomassDropInstance.biomassDrop = biomassDrop;
            }    
            else
            {
                GameObject biomassInstance = Instantiate(biomass, transform.position + biomassDirection, transform.rotation);
                ResourceOnDrop biomassDropInstance = biomassInstance.GetComponent<ResourceOnDrop>();
                biomassDropInstance.biomassDrop = biomassDrop;
            }   

            if (enemyController.enemyType != EnemyController.EnemyTypes.AlienAI)
            {
                Vector3 weaponPartsDirection = new (0, -1, 0);
                RaycastHit2D hitWeaponParts = Physics2D.Raycast(transform.position, weaponPartsDirection.normalized);
                if (hitWeaponParts.distance < weaponPartsDirection.magnitude)
                {
                    weaponPartsDirection = hitWeaponParts.point;
                    GameObject weaponPartsInstance = Instantiate(weaponParts, weaponPartsDirection, transform.rotation);
                    ResourceOnDrop weaponPartsDropInstance = weaponPartsInstance.GetComponent<ResourceOnDrop>();
                    weaponPartsDropInstance.weaponPartsDrop = weaponPartsDrop;
                }
                else
                {
                    GameObject weaponPartsInstance = Instantiate(weaponParts, transform.position + weaponPartsDirection, transform.rotation);
                    ResourceOnDrop weaponPartsDropInstance = weaponPartsInstance.GetComponent<ResourceOnDrop>();
                    weaponPartsDropInstance.weaponPartsDrop = weaponPartsDrop;
                }
            }
            
        }
    }
}
