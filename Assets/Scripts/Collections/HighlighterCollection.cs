using UnityEngine;
using ScriptableObjectArchitecture;

namespace Collections
{
    [CreateAssetMenu(fileName = "HighlighterCollection.asset", menuName = "Collections/Highlighter")]
    public sealed class HighlighterCollection : Collection<Highlighter>
    {
    }
}