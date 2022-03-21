using System;
using System.Collections.Generic;

namespace CSharpTestProject.Graph
{
    public class SearchingTheGraphSPOJ : IStartable
    {
        public void Start()
        {
            throw new NotImplementedException();
        }


    }

    public class GraphNode
    {
        private readonly List<GraphNode> adjacentnodes;

        public GraphNode[] GetNodes()
        {
            return adjacentnodes.ToArray();
        }

        public void AddNode(GraphNode node)
        {
            adjacentnodes.Add(node);
        }

        public bool RemoveNode(GraphNode node)
        {
            return adjacentnodes.Remove(node);
        }
    }
}
