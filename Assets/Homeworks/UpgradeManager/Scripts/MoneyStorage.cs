using Cysharp.Threading.Tasks;
using UniRx;

public sealed class MoneyStorage
{
    private int _money;

    public readonly IntReactiveProperty Money = new IntReactiveProperty();

    public MoneyStorage(int initAmount)
    {
        _money = initAmount;
    }

    public bool IsEnoughMoney(int needAmount)
    {
        return _money >= needAmount;
    }

    public void SpendMoney(int moneyToSpend)
    {
        _money -= moneyToSpend;
        Money.Value = _money;
    }
}