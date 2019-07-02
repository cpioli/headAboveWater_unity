namespace cpioli.Events
{
    public interface IRootPrefab
    {
        /// <summary>
        /// Retrieves all instances of scripts inheriting ICommonGameEvents
        /// from the children of this prefab
        /// </summary>
        void RetrieveChildrenComponents();
    }
}
