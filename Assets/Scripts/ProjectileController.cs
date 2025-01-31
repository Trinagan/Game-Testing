using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 200;
    public int pierceAmount = 0;
    public int bounceCount = 0;
    public ProjectileController projectileController;
    public Vector3 startingPosition;
    public Vector3 targetPosition;

    void Start()
    {
        Vector2 aimVector = getAimVector(startingPosition, targetPosition);
        float rotationAngle = Vector3.Angle(startingPosition, transform.forward);
        Quaternion targetAngle = Quaternion.Euler(aimVector.x, aimVector.y, 0);
        transform.rotation = targetAngle;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed*Time.deltaTime, 0));
    }

    Vector2 getAimVector(Vector3 originPos, Vector3 mousePos) {
        Vector2 aimVec = originPos - mousePos;
        return aimVec.normalized;
    }
}
