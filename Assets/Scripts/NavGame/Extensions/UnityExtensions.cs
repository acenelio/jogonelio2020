// https://answers.unity.com/questions/50279/check-if-layer-is-in-layermask.html
namespace UnityEngine
{
    public static class UnityExtensions
    {
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}
