using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdjacencyMatrix
{
    public class Item
    {
        public string Name { get; set; }
        public List<Arc> Arcs { get; set; } = new List<Arc>();

        public void AddArc(Item child, int weigth)
        {
            Arcs.Add(new Arc { Weigth = weigth, Parent = this, Child = child });                

            if (!child.Arcs.Exists(a => a.Parent == child && a.Child == this))
                child.AddArc(this, weigth);
        }
    }

    public class Arc
    {
        public int Weigth { get; set; }
        public Item Parent { get; set; }
        public Item Child { get; set; }
    }

    public class AdjacencMatrix
    {
        private IPerformer _performer;

        public AdjacencMatrix(IPerformer performer)
        {
            _performer = performer;
        }

        public void Create(List<Item> items)
        {
            var adjacentMatrix = new int?[items.Count, items.Count];

            for (var i = 0; i < items.Count; i++)
            {
                var parent = items[i];
                for (var j = 0; j < items.Count; j++)
                {
                    var child = items[j];
                    var arc = parent.Arcs.FirstOrDefault(a => a.Child == child);
                    if (arc != null)
                        adjacentMatrix[i, j] = arc.Weigth;
                }
            }

            _performer.Perform(adjacentMatrix);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = new Item { Name = "A" };
            var b = new Item { Name = "B" };
            var c = new Item { Name = "C" };
            var d = new Item { Name = "D" };
            var e = new Item { Name = "E" };
            var f = new Item { Name = "F" };
            var g = new Item { Name = "G" };
            var h = new Item { Name = "H" };

            a.AddArc(b, 1);
            a.AddArc(c, 1);
            b.AddArc(e, 1);
            b.AddArc(d, 3);
            c.AddArc(f, 1);
            c.AddArc(d, 3);
            d.AddArc(h, 8);
            e.AddArc(g, 1);
            e.AddArc(h, 3);
            f.AddArc(h, 3);

            new AdjacencMatrix(new Display()).Create(new List<Item> { a, b, c, d, e, f, g, h });

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public interface IPerformer
    {
        void Perform(int?[,] matrix);
    }

    public sealed class Display : IPerformer
    {
        private StringBuilder _sb = new StringBuilder();

        public void Perform(int?[,] matrix)
        {
            var length = matrix.GetLength(1);

            _sb.Append("    ");

            for (var i = 0; i < length; i++)
                _sb.Append($"{(char)('A' + i)}  ");

            _sb.AppendLine();

            for (var i = 0; i < length; i++)
            {
                _sb.Append($"{(char)('A' + i)} [");

                for (var j = 0; j < length; j++)
                {
                    if (i == j)
                        _sb.Append(" - ");
                    else if (matrix[i, j] == null)
                        _sb.Append(" 0 ");
                    else
                        _sb.Append($" {matrix[i, j]} ");
                }

                _sb.Append("]\r\n");
            }

            Console.WriteLine(_sb);
        }
    }
}