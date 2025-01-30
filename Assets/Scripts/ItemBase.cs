using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] private ItemData _scriptedObjectData;
    public ItemData _itemData => _scriptedObjectData;
    private PoolManager _poolsManager;

    public static ItemBase instance;

    private void Awake()
    {
        instance = this;
    }

    internal void SetPoolManager(PoolManager poolsManager)
    {
        _poolsManager = poolsManager;
    }

    public void returnItem()
    {
        _poolsManager.RemoveItem(this);
    }
}
