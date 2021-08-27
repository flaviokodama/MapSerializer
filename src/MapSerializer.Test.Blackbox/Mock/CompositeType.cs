namespace MapSerializer.Test.Blackbox.Mock
{
    public class CompositeType
    {
        public CompositeType Child { get; set; }
        public int Depth { get; set; }

        internal static CompositeType CreateDeepComposition(int depth, int currentDepth = 1)
        {
            if (currentDepth < depth)
                return new CompositeType() { Depth = currentDepth, Child = CreateDeepComposition(depth, currentDepth + 1) };
            else
                return new CompositeType() { Depth = currentDepth };
        }
    }
}
