using UnityEngine;

public interface IFactory
{
    GameObject Create(ScriptableObject objectData);
}
