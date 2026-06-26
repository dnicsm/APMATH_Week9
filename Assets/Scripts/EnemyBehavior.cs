using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Vector3 enemyPosition; 
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = GetRandomColor();
        }

    }

    private void Update()
    {
        float denom = CameraSettings.focalLength + enemyPosition.z;
        if (denom <= 0.1f) denom = 0.1f;

        float perspective = CameraSettings.focalLength / denom;
        
        transform.localScale = Vector3.one * perspective;

        float xOffset = (enemyPosition.x - CameraSettings.vanishingPoint.x) * perspective;
        float yOffset = (enemyPosition.y - CameraSettings.vanishingPoint.y) * perspective;

   
        transform.position = new Vector3(
            CameraSettings.vanishingPoint.x + xOffset, 
            CameraSettings.vanishingPoint.y + yOffset, 
            0
        );
    }

       private Color GetRandomColor()
    {
        var rRand = Random.Range(0f, 1f);
        var gRand = Random.Range(0f, 1f);
        var bRand = Random.Range(0f, 1f);
        return new Color(rRand, gRand, bRand);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
    }

}