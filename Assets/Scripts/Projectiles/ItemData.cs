using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Create Scriptable Objects/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private ItemBase _itemPrefab;
    [SerializeField] private ItemType _typeOfItem;
    [SerializeField] private float _timeBeforeConsume;
    [SerializeField] private bool _isSingleUse;
    [SerializeField] private float _reuseCooldown;
    [SerializeField] private bool _isProjectile;
    [SerializeField] private float _speed;
    [SerializeField] private float _hpModifier;


    public ItemBase ItemPrefab => _itemPrefab;
    public float HPModifier
    {
        get => _hpModifier;
        set => _hpModifier = value;
    }
    public ItemType TypeOfItem
    {
        get => _typeOfItem;
        set => _typeOfItem = value;
    }
    public float TimeBeforeConsume
    {
        get => _timeBeforeConsume;
        set => _timeBeforeConsume = value;
    }
    public float ReuseCooldown
    {
        get => _reuseCooldown;
        set => _reuseCooldown = value;
    }
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    public bool IsProjectile
    {
        get => _isProjectile;
        set => _isProjectile = value;
    }
    public bool IsSingleUse
    {
        get => _isSingleUse;
        set => _isSingleUse = value;
    }

    public enum ItemType
    {
        None = 0,
        BasicProjectile = 1,
    }
}