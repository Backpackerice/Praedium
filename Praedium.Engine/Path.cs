using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine
{
    public class Path<Node> : IEnumerable<Node>
    {
        public Node LastStep
        {
            get;
            private set;
        }

        public Path<Node> PreviousSteps
        {
            get;
            private set;
        }

        public double TotalCost
        {
            get;
            private set;
        }

        public Path(Node start)
            :this(start, null, 0)
        { }

        public Path<Node> AddStep(Node step, double stepCost)
        {
            return new Path<Node>(step, this, TotalCost + stepCost);
        }

        private Path(Node lastStep, Path<Node> previousSteps, double totalCost)
        {
            LastStep = lastStep;
            PreviousSteps = previousSteps;
            TotalCost = totalCost;
        }

        public IEnumerator<Node> GetEnumerator()
        {
            for(Path<Node> p = this; p != null; p = p.PreviousSteps)
            {
                yield return p.LastStep;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static Path<Node> FindPath(
            Node start,
            Node destination,
            Func<Node, Node, double> distance,
            Func<Node, Node, double> estimate,
            Func<Node, IEnumerable<Node>> neighbours)
        {
            var closed = new HashSet<Node>();
            var queue = new PriorityQueue<double, Path<Node>>();
            queue.Enqueue(0, new Path<Node>(start));

            while(!queue.IsEmpty)
            {
                var path = queue.Dequeue();

                if(closed.Contains(path.LastStep))
                {
                    continue;
                }

                if(path.LastStep.Equals(destination))
                {
                    return path;
                }

                closed.Add(path.LastStep);

                foreach (Node n in neighbours(path.LastStep))
                {
                    double d = distance(path.LastStep, n);
                    var newPath = path.AddStep(n, d);
                    queue.Enqueue(newPath.TotalCost + estimate(n, destination), newPath);
                }
            }

            return null;
        }
    }
}
