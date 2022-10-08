public class EnhancerFactory
{
    public static ItemDecorator CreateItemDecorator(sEnhancer _enhancer)
    {
        if (_enhancer.enhancerType == sEnhancer.EnhancerType.GOLD)
        {
            return new GoldValueEnhancer(_enhancer.amount);
        }

        return null;
    }
}
