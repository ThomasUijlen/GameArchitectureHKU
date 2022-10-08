using UnityEngine;

[CreateAssetMenu(menuName = "Enhancers/FloatEnhancer")]
public class sEnhancer : ScriptableObject
{
    public enum EnhancerType { GOLD, OXYGONAMOUNT}
    public EnhancerType enhancerType;
    public int amount;
}
