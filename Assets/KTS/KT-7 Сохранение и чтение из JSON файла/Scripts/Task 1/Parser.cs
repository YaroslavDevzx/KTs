using UnityEngine;

public class Parser : MonoBehaviour
{
    private void Start()
    {
        Utils.ParseCSV("data");
    }
}