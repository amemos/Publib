using UnityEngine;

public class CFactoryBehaviour<T> : MonoBehaviour where T : new()
{
    protected static T ProducedItem;

    protected virtual void Awake()
    {
        CreateNewItem();
    }

    private T CreateNewItem()
    {
        ProducedItem = new T();
        return ProducedItem;
    }
}
