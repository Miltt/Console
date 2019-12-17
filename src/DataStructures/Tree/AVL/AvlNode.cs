namespace Cnsl.DataStructures
{
    public class AvlNode
    {
        public int Key { get; }
        public int Height { get; set; }
        public AvlNode Left { get; set; }
        public AvlNode Right { get; set; }

        public AvlNode(int key)
        {
            Key = key;
            Height = 1;
        }
    }
}