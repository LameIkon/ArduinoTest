using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    /// <summary>
    /// MeshData for a flyweight design pattern.
    /// </summary>
    [CreateAssetMenu(menuName=("Data/Meshes"))]
    public class MeshData : ScriptableObject
    {
        [SerializeField] private Mesh[] _meshes; // The meshes that it can change into.
        
        /// <summary>
        /// Returns an array of Meshes that the <c>Object</c> should change into.
        /// </summary>
        public Mesh[] Meshes
        {
            get => _meshes;
        }

    }
}