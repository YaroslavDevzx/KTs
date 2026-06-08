using UnityEngine;
using TMPro;

public class WalletUI : MonoBehaviour
{
    public static WalletUI Instance { get; private set; }

    [SerializeField] private TMP_Text balanceText;

    private int _balance;

    private void Awake()
    {
        Instance = this;
    }


    public void Add(int amount)
    {
        _balance += amount;
        balanceText.text = $"{_balance} $";
    }
}