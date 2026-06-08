using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5;
    [SerializeField] private float spacing = 1.05f;

    private void Awake()
    {
        for (int y = 0; y < height; y++)
        {
            float offset = (y % 2 == 0) ? 0f : spacing * 0.5f;
            for (int x = 0; x < width; x++)
            {
                Vector3 pos = transform.position + new Vector3(x * spacing + offset, y * spacing, 0f);
                Instantiate(cubePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}