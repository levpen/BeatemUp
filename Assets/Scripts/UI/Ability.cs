using UnityEngine;

namespace Beatemup.UI
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Beatemup/UI/Ability", order = 1)]
    public class Ability : ScriptableObject
    {
        [SerializeField] private string description;
        [SerializeField] public int index;
    }
}